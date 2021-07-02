using CheatUITemplt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WPFCheatUITemplate.GameFuns
{
    class Coin: GameFun
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
      

        public Coin()
        {
            ModuleName = "PlantsVsZombies.exe";
            ModuleOffsetAddress = 0x355E0C;

            IsSignatureCode = false;

            IntPtrOffset = new uint[] { 0x950, 0x50 };
            IsIntPtr = true;

            Vk = Keys.NumPad2;
            FsModifiers = HotKey.KeyModifiers.None;

            KeyDescription_SC = "数字键2";
            FunDescribe_SC = "设置硬币";

            KeyDescription_TC = "數字鍵2";
            FunDescribe_TC = "設置硬幣";

            KeyDescription_EN = "Number 2";
            FunDescribe_EN = "Coin number";

            IsTrigger = true;


            IsAcceptValue = true;
            SliderMinNum = 1;
            SliderMaxNum = 9999;

            GameFunManger.Instance.RegisterGameFun(this);
        }

        public override void DoFirstTime(double value)
        {
            CheatTools.WriteMemoryInt(GameDataAddress.Address, Handle, (int)value);
        }

        public override void DoRunAgain(double value)
        {

        }
    }
}
