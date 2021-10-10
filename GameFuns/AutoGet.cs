using CheatUITemplt;
using System.Windows.Forms;

namespace WPFCheatUITemplate.GameFuns
{
    class AutoGet:GameFun
    {
        public AutoGet()
        {
            this.gameFunDateStruct = new WPFCheatUITemplate.Other.GameFunDateStruct()
            {
                ModuleName = "PlantsVsZombies.exe",
                ModuleOffsetAddress = 0x3CC72,

                IsSignatureCode = false,
                IsIntPtr = false,

                Vk = Keys.W,
                FsModifiers = HotKey.KeyModifiers.Shift,

                KeyDescription_SC = "Shift+字母键W",
                FunDescribe_SC = "自动获取",

                KeyDescription_TC = "Shift+字母鍵W",
                FunDescribe_TC = "自動獲取",

                KeyDescription_EN = "Shift+Number W",
                FunDescribe_EN = "Automatic acquisition",

                IsTrigger = false,
            };
           




            AppGameFunManger.Instance.RegisterGameFun(this);
        }
        public override void Awake()
        {

        }

        public override void DoFirstTime(double value)
        {
            CheatTools.WriteMemoryByte(gameFunDateStruct.GameDataAddress.Address, gameFunDateStruct.Handle, new byte[] { 0xEB });
        }
    

        public override void DoRunAgain(double value)
        {
            CheatTools.WriteMemoryByte(gameFunDateStruct.GameDataAddress.Address, gameFunDateStruct.Handle, new byte[] { 0x75 });
        }
        public override void Ending()
        {

        }
    }
}
