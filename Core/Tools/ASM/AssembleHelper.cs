using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Iced.Intel;
using static Iced.Intel.AssemblerRegisters;

namespace WPFCheatUITemplate.Core.Tools.ASM
{
    public static class AssembleHelper
    {
        #region 引用

        [DllImport("kernel32.dll", EntryPoint = "CloseHandle")]

        public static extern int CloseHandle(int hObject);

        [DllImport("kernel32.dll")]
        public static extern bool ReadProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, byte[] buffer, int size, out IntPtr lpNumberOfBytesRead);


        [DllImport("kernel32.dll")]

        public static extern Int32 WriteProcessMemory(

        IntPtr hProcess,

        IntPtr lpBaseAddress,

        [In, Out] byte[] buffer,

        int size,

        out IntPtr lpNumberOfBytesWritten);

        [DllImport("kernel32.dll")]

        public static extern Int32 WriteProcessMemory(

            IntPtr hProcess,

            IntPtr lpBaseAddress,

            byte[] buffer,

            int size,

            int lpNumberOfBytesWritten);

        [DllImport("kernel32", EntryPoint = "CreateRemoteThread")]

        public static extern int CreateRemoteThread(

            int hProcess,

            int lpThreadAttributes,

            int dwStackSize,

            Int64 lpStartAddress,

            int lpParameter,

            int dwCreationFlags,

            ref int lpThreadId

            );

        [DllImport("Kernel32.dll")]

        public static extern System.Int32 VirtualAllocEx(

            System.IntPtr hProcess,

            System.Int32 lpAddress,

            System.Int32 dwSize,

            System.Int16 flAllocationType,

            System.Int16 flProtect

            );

        [DllImport("Kernel32.dll")]

        public static extern Int64 VirtualAllocEx(

            int hProcess,

            int lpAddress,

            int dwSize,

            int flAllocationType,

            int flProtect

            );

        [DllImport("Kernel32.dll")]

        public static extern System.Int32 VirtualFreeEx(

            int hProcess,

            Int64 lpAddress,

            int dwSize,

            int flAllocationType

            );

        [DllImport("kernel32.dll", EntryPoint = "OpenProcess")]

        public static extern int OpenProcess(

            int dwDesiredAccess,

            int bInheritHandle,

            int dwProcessId

            );

        private const int PAGE_EXECUTE_READWRITE = 0x4;

        private const int MEM_COMMIT = 4096;

        private const int MEM_RELEASE = 0x8000;

        private const int MEM_DECOMMIT = 0x4000;

        private const int PROCESS_ALL_ACCESS = 0x1F0FFF;

        private const int PROCESS_CREATE_THREAD = 0x2;

        private const int PROCESS_VM_OPERATION = 0x8;

        private const int PROCESS_VM_WRITE = 0x20;

        #endregion

        public static void RunAsm(this Assembler asm, int pid)
        {
            var stream = new MemoryStream();

            asm.Assemble(new StreamCodeWriter(stream), 0);
            var code = stream.ToArray();

            var hwnd = OpenProcess(PROCESS_ALL_ACCESS | PROCESS_CREATE_THREAD | PROCESS_VM_WRITE, 0, pid);

            if (hwnd != 0)
            {

               var addre = VirtualAllocEx(hwnd, 0, code.Length, MEM_COMMIT, PAGE_EXECUTE_READWRITE);

                WriteProcessMemory((IntPtr)hwnd, (IntPtr)addre, code, code.Length, 0);

                var  threadhwnd = CreateRemoteThread(hwnd, 0, 0, addre, 0, 0, ref pid);

                VirtualFreeEx(hwnd, addre, code.Length, MEM_RELEASE);

                CloseHandle(threadhwnd);

                CloseHandle(hwnd);

            }
        }

        public static void JmpHook(this Assembler asm, int pid,Int64 address)
        {
            var codeBytes = new byte[32];

            var hwnd = OpenProcess(PROCESS_ALL_ACCESS | PROCESS_CREATE_THREAD | PROCESS_VM_WRITE, 0, pid);
            if (hwnd != 0)
            {
                ReadProcessMemory((IntPtr)hwnd, (IntPtr)address, codeBytes, codeBytes.Length, out var readBytes);

            }

            int exampleCodeBitness = asm.Bitness;
            ulong exampleCodeRIP = (ulong)address;

            var codeReader = new ByteArrayCodeReader(codeBytes);
            var decoder = Iced.Intel.Decoder.Create(exampleCodeBitness, codeReader);
            decoder.IP = exampleCodeRIP;
            ulong endRip = decoder.IP + (uint)codeBytes.Length;

            var instructions = new List<Instruction>();
            while (decoder.IP < endRip)
                instructions.Add(decoder.Decode());

            int index  = 0;
            int codeSum = 0;
            int MaxJmpLeng = asm.Bitness == 32 ? 5 : 14;
            foreach (var item in instructions)
            {
                codeSum += item.Length;
                index++;
                if (codeSum >= MaxJmpLeng)
                {
                    break;
                }
               
            }

            var addre = VirtualAllocEx(hwnd, 0, 1024, MEM_COMMIT, PAGE_EXECUTE_READWRITE);

            //写入原始数据
            WriteProcessMemory((IntPtr)hwnd, (IntPtr)addre, codeBytes, codeSum, 0);

            //ret
            asm.jmp(instructions[index].IP);
            var stream = new MemoryStream();
            asm.Assemble(new StreamCodeWriter(stream), (ulong)(addre+ codeSum));
            var code = stream.ToArray();
            WriteProcessMemory((IntPtr)hwnd, (IntPtr)addre + codeSum, code, code.Length, 0);

            //jmp
            Assembler jmpAsm = new Assembler(asm.Bitness);
            jmpAsm.jmp((ulong)addre);
            for (int i = 0; i < codeSum - MaxJmpLeng; i++)
            {
                jmpAsm.nop();
            }          
            var streamjmp = new MemoryStream();
            jmpAsm.Assemble(new StreamCodeWriter(streamjmp), (ulong)(address));
            var jmpcode = streamjmp.ToArray();
            WriteProcessMemory((IntPtr)hwnd, (IntPtr)address, jmpcode, jmpcode.Length, 0);

            CloseHandle(hwnd);
        }

    }
}
