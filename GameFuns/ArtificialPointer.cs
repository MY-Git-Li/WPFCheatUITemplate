using CheatUITemplt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFCheatUITemplate.GameFuns
{
    class ArtificialPointer : GameFun
    {

        public override string ModuleName { get; set; }
        public override uint ModuleOffsetAddress { get; set; }
        public override uint[] IntPtrOffset { get; set; }
        public override System.Windows.Forms.Keys Vk { get; set; }

        public override bool IsSignatureCode { get; set; }
        public override string SignatureCode { get; set; }
        public override uint SignatureCodeOffset { get; set; }
        public override bool IsTrigger { get; set; }
        internal override HotKey.KeyModifiers FsModifiers { get; set; }
        internal override GameDataAddress GameDataAddress { get; set; }
        public override IntPtr Handle { get; set; }
        public override bool IsIntPtr { get; set; }
        public override string KeyDescription_TC { get; set; }
        public override string FunDescribe_TC { get; set; }
        public override bool IsAcceptValue { get; set; }
        public override string KeyDescription_SC { get; set; }
        public override string FunDescribe_SC { get; set; }
        public override string KeyDescription_EN { get; set; }
        public override string FunDescribe_EN { get; set; }
        public override double SliderMaxNum { get; set; }
        public override double SliderMinNum { get; set; }

        int artificialPoint;

        public ArtificialPointer()
        {
            ModuleName = "PlantsVsZombies.exe";
          
            Vk = System.Windows.Forms.Keys.NumPad9;
            FsModifiers = HotKey.KeyModifiers.None;

            KeyDescription_SC = "数字键9";
            FunDescribe_SC = "人造指针设置阳光";

            KeyDescription_TC = "數字鍵9";
            FunDescribe_TC = "人造指針設置陽光";

            KeyDescription_EN = "Number 9";
            FunDescribe_EN = "ArtificialPointer Sun number";

            IsTrigger = true;


            IsAcceptValue = true;
            SliderMinNum = 1;
            SliderMaxNum = 9999;

            GameFunManger.Instance.RegisterGameFun(this);
        }

        public override void Awake()
        {
            int pid = CheatTools.GetPidByHandle(Handle);
            int hwnd = ASM.OpenProcess(0x1F0FFF | 0x2 | 0x20, 0, pid);

            artificialPoint = ASM.VirtualAllocEx(hwnd,0,64, 4096, 0x4);
           
            ASM asm = new ASM();
            asm.Mov_EAX_DWORD_Ptr_EDI_Add(0x5578);
            asm.Pushad();
            asm.Mov_EAX_EDI();
            asm.Mov_DWORD_Ptr_EAX(artificialPoint);
            asm.Popad();
            asm.RunJmpHook(pid, (int)(this.ModuleAddress + 0x9f2e5), (int)(this.ModuleAddress + 0x9F2EB));

        }

        public override void DoFirstTime(double value)
        {
            int address = CheatTools.ReadMemoryValue(artificialPoint, Handle);


            CheatTools.WriteMemoryInt(address+0x5578, Handle, (int)value);
        }

        public override void DoRunAgain(double value)
        {
           
        }
    }
}
