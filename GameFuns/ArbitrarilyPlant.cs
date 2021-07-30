using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CheatUITemplt.MyGameFuns
{
    class ArbitrarilyPlant : GameFun
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

        public ArbitrarilyPlant()
        {
            ModuleName = "PlantsVsZombies.exe";
            
            IsSignatureCode = true;
            SignatureCodeOffset = 0xe;
            SignatureCode = "8B 54 24 0C 53 52 57";

            IsIntPtr = false;

            Vk = Keys.NumPad1;
            FsModifiers = HotKey.KeyModifiers.Ctrl;

            KeyDescription_SC = "Ctrl+数字键1";
            FunDescribe_SC = "随意种植";

            KeyDescription_TC = "Ctrl+數字鍵1";
            FunDescribe_TC = "隨意種植";

            KeyDescription_EN = "Ctrl+Number 1";
            FunDescribe_EN = "Planting will";

            IsTrigger = false;


            GameFunManger.Instance.RegisterGameFun(this);
        }
        public override void Awake()
        {

        }

        public override void DoFirstTime(double value)
        {
            CheatTools.WriteMemoryByte(GameDataAddress.Address, Handle, new byte[] { 0xE9, 0x47, 0x09, 0x00, 0x00, 0x90 });

        }

        public override void DoRunAgain(double value)
        {
            CheatTools.WriteMemoryByte(GameDataAddress.Address, Handle, new byte[] { 0x0f, 0x84, 0x46, 0x09, 0x00, 0x00 });

        }
    }
}
