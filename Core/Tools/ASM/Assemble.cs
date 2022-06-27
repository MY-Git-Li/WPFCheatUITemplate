using Iced.Intel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFCheatUITemplate.Core.Tools.ASM
{
    internal class Assemble : Assembler
    {

        const int HOOKALLOCSIZE = 1024;

        HookTtpe hookTtpe;
        struct HookTtpe
        {
            public Int64 O_address;
            public byte[] codeBytes;
            public int codeSum;
            public byte[] jmpBytes;
        }


        public Assemble(int bitness) : base(bitness)
        {
        }

        public void RunAsm()
        {
            int pid = GameMode.GameInformation.Pid;

            IntPtr hwnd = GameMode.GameInformation.Handle;

            var stream = new MemoryStream();

            this.Assemble(new StreamCodeWriter(stream), 0);
            var code = stream.ToArray();

            if (!hwnd.Equals(IntPtr.Zero))
            {

                var addre = WinAPI.VirtualAllocEx(hwnd, 0, code.Length, WinAPI.MEM_COMMIT, WinAPI.PAGE_EXECUTE_READWRITE);

                CheatTools.WriteProcessMemory(hwnd, (IntPtr)addre, code, code.Length,out var writeBytes);

                var threadhwnd = WinAPI.CreateRemoteThread(hwnd.ToInt32(), 0, 0, addre, 0, 0, ref pid);

                WinAPI.VirtualFreeEx(hwnd, addre, code.Length, WinAPI.MEM_RELEASE);

                WinAPI.CloseHandle((IntPtr)threadhwnd);

            }
        }

        public void Hook(Int64 address)
        {
            int pid = GameMode.GameInformation.Pid;

            IntPtr hwnd = GameMode.GameInformation.Handle;

            var codeBytes = new byte[32];

            if (!hwnd.Equals(IntPtr.Zero))
            {
                CheatTools.ReadProcessMemory(hwnd, (IntPtr)address, codeBytes, codeBytes.Length, out var readBytes);

            }

            int exampleCodeBitness = this.Bitness;
            ulong exampleCodeRIP = (ulong)address;

            var codeReader = new ByteArrayCodeReader(codeBytes);
            var decoder = Iced.Intel.Decoder.Create(exampleCodeBitness, codeReader);
            decoder.IP = exampleCodeRIP;
            ulong endRip = decoder.IP + (uint)codeBytes.Length;

            var instructions = new List<Instruction>();
            while (decoder.IP < endRip)
                instructions.Add(decoder.Decode());


            var addre = WinAPI.VirtualAllocEx(hwnd, 0, HOOKALLOCSIZE, WinAPI.MEM_COMMIT, WinAPI.PAGE_EXECUTE_READWRITE);

            int index = 0;
            int codeSum = 0;

            Assembler TextjmpAsmCode = new Assembler(this.Bitness);
            TextjmpAsmCode.jmp((ulong)addre);
            var Textstreamjmp = new MemoryStream();
            TextjmpAsmCode.Assemble(new StreamCodeWriter(Textstreamjmp), (ulong)(address));
            var Textjmpcode = Textstreamjmp.ToArray();

            int MaxJmpLeng = Textjmpcode.Length;
            foreach (var item in instructions)
            {
                codeSum += item.Length;
                index++;
                if (codeSum >= MaxJmpLeng)
                {
                    break;
                }

            }

            hookTtpe.codeBytes = codeBytes;
            hookTtpe.codeSum = codeSum;

            hookTtpe.O_address = address;
          
            //写入原始数据
            CheatTools.WriteProcessMemory(hwnd, (IntPtr)addre, codeBytes, codeSum, out var writeBytes);

            //ret
            this.jmp(instructions[index].IP);
            var stream = new MemoryStream();
            this.Assemble(new StreamCodeWriter(stream), (ulong)(addre + codeSum));
            var code = stream.ToArray();
            CheatTools.WriteProcessMemory(hwnd, (IntPtr)addre + codeSum, code, code.Length, out writeBytes);

            
           
            //jmp
            Assembler jmpAsm = new Assembler(this.Bitness);
            jmpAsm.jmp((ulong)addre);
            for (int i = 0; i < codeSum - MaxJmpLeng; i++)
            {
                jmpAsm.nop();
            }
            var streamjmp = new MemoryStream();
            jmpAsm.Assemble(new StreamCodeWriter(streamjmp), (ulong)(address));
            var jmpcode = streamjmp.ToArray();
            CheatTools.WriteProcessMemory(hwnd, (IntPtr)address, jmpcode, jmpcode.Length, out writeBytes);

            hookTtpe.jmpBytes = jmpcode;
        }


        public void CloseHook()
        {
            if (hookTtpe.O_address == 0)
            {
                return;
            }
            //写入原始数据
            CheatTools.WriteProcessMemory(GameMode.GameInformation.Handle, (IntPtr)hookTtpe.O_address, hookTtpe.codeBytes, hookTtpe.codeSum, out var writeBytes);

        }


        public void RestartHook()
        {
            if (hookTtpe.O_address == 0)
            {
                return;
            }
            CheatTools.WriteProcessMemory(GameMode.GameInformation.Handle, (IntPtr)hookTtpe.O_address, hookTtpe.jmpBytes, hookTtpe.jmpBytes.Length,out var writeBytes);
        }
    }
}
