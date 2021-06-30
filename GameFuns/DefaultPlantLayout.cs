using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CheatUITemplt.MyGameFuns
{
    class DefaultPlantLayout:GameFun
    {
        public override string ModuleName { get; set; }
        public override uint ModuleOffsetAddress { get; set; }
        public override uint[] IntPtrOffset { get; set; }
        public override Keys Vk { get; set; }

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

        int pid;
        public DefaultPlantLayout()
        {
           

            Vk = Keys.NumPad8;
            FsModifiers = HotKey.KeyModifiers.Alt;

            KeyDescription_SC = "ALT+数字键8";
            FunDescribe_SC = "默认植物种植";

            KeyDescription_TC = "ALT+數字鍵8";
            FunDescribe_TC = "默認植物種植";

            KeyDescription_EN = "Alt+Number 8";
            FunDescribe_EN = "Default planting";

            IsTrigger = true;
           
            GameFunManger.Instance.RegisterGameFun(this);
        }

        public void Plant(int x, int y, int id)
        {
            ASM asm = new ASM();
            asm.Pushad();
            asm.Push68(-1);
            asm.Push68(id);
            asm.Mov_EAX(x);
            asm.Push68(y);
            asm.Mov_ECX_DWORD_Ptr(0x755E0C);
            asm.Mov_ECX_DWORD_Ptr_ECX_Add(0x868);
            asm.Push_ECX();
            asm.Mov_EBX(0x00418D70);
            asm.Call_EBX();
            asm.Popad();
            asm.Ret();
            asm.RunAsm(pid);
        }

        public override void DoFirstTime(double value)
        {
            pid = CheatTools.GetPidByHandle(Handle);
            for (int i = 0; i < 5; i++)
            {
                Plant(i, 0, 40);
                Thread.Sleep(10);
                Plant(i, 1, 40);
                Thread.Sleep(10);
                Plant(i, 2, 43);
                Thread.Sleep(10);
                Plant(i, 3, 43);
                Thread.Sleep(10);
                Plant(i, 4, 44);
                Thread.Sleep(10);
                Plant(i, 5, 44);
                Thread.Sleep(10);
                Plant(i, 6, 22);
                Thread.Sleep(10);
                Plant(i, 7, 23);
                Thread.Sleep(10);
                Plant(i, 8, 46);
                Thread.Sleep(10);
            }
        }

        public override void DoRunAgain(double value)
        {
           
        }
    }
}
