using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CheatUITemplt.MyGameFuns
{
    class AllowBackground:GameFun
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

        public AllowBackground()
        {
            ModuleName = "PlantsVsZombies.exe";
            ModuleOffsetAddress = 0x5D040;
            IsSignatureCode = false;
            IsIntPtr = false;

            Vk = Keys.NumPad6;
            FsModifiers = HotKey.KeyModifiers.Ctrl;

            KeyDescription_SC = "Ctrl+数字键6";
            FunDescribe_SC = "允许后台运行";

            KeyDescription_TC = "Ctrl+數字鍵6";
            FunDescribe_TC = "允許後臺運行";

            KeyDescription_EN = "Ctrl+Number 6";
            FunDescribe_EN = "Running in background";

            IsTrigger = false;


            GameFunManger.Instance.RegisterGameFun(this);
        }

        public override void DoFirstTime(double value)
        {
            CheatTools.WriteMemoryByte(GameDataAddress.Address, Handle, new byte[] { 0xC2, 0x04, 0x00 });
        }

        public override void DoRunAgain(double value)
        {
            CheatTools.WriteMemoryByte(GameDataAddress.Address, Handle, new byte[] { 0x55, 0x8B, 0xEC });
           
        }
    }
}

