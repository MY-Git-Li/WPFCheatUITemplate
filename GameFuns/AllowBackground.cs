using CheatUITemplt;
using System.Windows.Forms;

namespace WPFCheatUITemplate.GameFuns
{
    class AllowBackground:GameFun
    {
       
        public AllowBackground()
        {
            gameFunDataAndUIStruct = new Other.GameFunDataAndUIStruct();
            gameFunDataAndUIStruct.uIData = new Other.UIData()
            {
                KeyDescription_SC = "Ctrl+数字键2",
                FunDescribe_SC = "允许后台运行",

                KeyDescription_TC = "Ctrl+數字鍵2",
                FunDescribe_TC = "允許後臺運行",

                KeyDescription_EN = "Ctrl+Number 2",
                FunDescribe_EN = "Running in background",

                IsTrigger = false,

            };
            gameFunDataAndUIStruct.refHotKey = new Other.RefHotKey()
            {
                Vk = Keys.NumPad2,
                FsModifiers = HotKey.KeyModifiers.Ctrl,
            };
            gameFunDataAndUIStruct.AddData(GameVersion.Version.Default, new Other.GameData()
            {
                ModuleName = "PlantsVsZombies.exe",
                ModuleOffsetAddress = 0x5D040,
                IsSignatureCode = false,
                IsIntPtr = false,
            });
      
        }

        public override void Awake()
        {

        }

        public override void DoFirstTime(double value)
        {
            memory.WriteMemory<byte>(gameDataAddress.Address, new byte[] { 0xC2, 0x04, 0x00 });
      
        }

        public override void DoRunAgain(double value)
        {
            memory.WriteMemory<byte>(gameDataAddress.Address, new byte[] { 0x55, 0x8B, 0xEC });
        }
        public override void Ending()
        {

        }
    }
}

