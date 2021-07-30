using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CheatUITemplt.MyGameFuns
{
    class AutoGet:GameFun
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

        public AutoGet()
        {

            ModuleName = "PlantsVsZombies.exe";
            ModuleOffsetAddress = 0x3CC72;

            IsSignatureCode = false;
            IsIntPtr = false;

            Vk = Keys.W;
            FsModifiers = HotKey.KeyModifiers.Shift;

            KeyDescription_SC = "Shift+字母键W";
            FunDescribe_SC = "自动获取";

            KeyDescription_TC = "Shift+字母鍵W";
            FunDescribe_TC = "自動獲取";

            KeyDescription_EN = "Shift+Number W";
            FunDescribe_EN = "Automatic acquisition";

            IsTrigger = false;




            GameFunManger.Instance.RegisterGameFun(this);
        }
        public override void Awake()
        {

        }

        public override void DoFirstTime(double value)
        {
            CheatTools.WriteMemoryByte(GameDataAddress.Address, Handle, new byte[] { 0xEB });
        }
    

        public override void DoRunAgain(double value)
        {
            CheatTools.WriteMemoryByte(GameDataAddress.Address, Handle, new byte[] { 0x75 });
        }
    }
}
