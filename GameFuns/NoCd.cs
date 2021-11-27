using CheatUITemplt;
using System.Windows.Forms;

namespace WPFCheatUITemplate.GameFuns
{
    class NoCd:GameFun
    {
       
        public NoCd()
        {
            gameFunDateStruct = new Other.GameFunDataStruct();
            gameFunDateStruct.uIData = new Other.UIData()
            {
                KeyDescription_SC = "数字键7",
                FunDescribe_SC = "无冷却时间",

                KeyDescription_TC = "數字鍵7",
                FunDescribe_TC = "無冷卻時間",

                KeyDescription_EN = "Number 7",
                FunDescribe_EN = "No cool down time",

                IsTrigger = false

            };
            gameFunDateStruct.refHotKey = new Other.RefHotKey()
            {
                Vk = Keys.NumPad7,
                FsModifiers = HotKey.KeyModifiers.None,
            };
            gameFunDateStruct.AddGameDate(GameVersion.Version.Default, new Other.GameData()
            {
                ModuleName = "PlantsVsZombies.exe",
                ModuleOffsetAddress = 0x9ce02,
                IsSignatureCode = false,
                IsIntPtr = false,
            });

        }

        public override void Awake()
        {

        }

        public override void DoFirstTime(double value)
        {
            memory.WriteMemory<byte>(gameDataAddress.Address, new byte[] { 0x74 });
        }

        public override void DoRunAgain(double value)
        {
            memory.WriteMemory<byte>(gameDataAddress.Address, new byte[] { 0x7e });
        }
        public override void Ending()
        {

        }
    }
}
