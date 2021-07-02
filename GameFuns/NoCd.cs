using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CheatUITemplt.MyGameFuns
{
    class NoCd:GameFun
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
        public override bool IsIntPtr { get ; set ; }
        public override string KeyDescription_TC { get ; set; }
        public override string FunDescribe_TC { get; set ; }
        public override bool IsAcceptValue { get ; set ; }
        public override string KeyDescription_SC { get; set ; }
        public override string FunDescribe_SC { get ; set ; }
        public override string KeyDescription_EN { get ; set; }
        public override string FunDescribe_EN { get ; set ; }
        public override double SliderMaxNum { get ; set ; }
        public override double SliderMinNum { get ; set ; }

        public NoCd()
        {
            ModuleName = "PlantsVsZombies.exe";
            ModuleOffsetAddress = 0x9ce02;
            IsSignatureCode = false;
            IsIntPtr = false;

            Vk = Keys.NumPad6;
            FsModifiers = HotKey.KeyModifiers.None;

            KeyDescription_SC = "数字键6";
            FunDescribe_SC = "无冷却时间";

            KeyDescription_TC = "數字鍵6";
            FunDescribe_TC = "無冷卻時間";

            KeyDescription_EN = "Number key 6";
            FunDescribe_EN = "No Cool Down Time";

            IsTrigger = false;
           

            GameFunManger.Instance.RegisterGameFun(this);
        }


        public override void DoFirstTime(double value)
        {
            CheatTools.WriteMemoryByte(GameDataAddress.Address, Handle, new byte[] { 0x74 });
        }

        public override void DoRunAgain(double value)
        {
            CheatTools.WriteMemoryByte(GameDataAddress.Address, Handle, new byte[] { 0x7e });
        }
    }
}
