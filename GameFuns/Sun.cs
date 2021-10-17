using CheatUITemplt;
using System.Windows.Forms;

namespace WPFCheatUITemplate.GameFuns
{
    class Sun : GameFun
    {
        public Sun()
        {
            gameFunDateStruct = new Other.GameFunDateStruct();
            gameFunDateStruct.uIData = new Other.UIData()
            {
                KeyDescription_SC = "数字键1",
                FunDescribe_SC = "设置阳光",

                KeyDescription_TC = "數字鍵1",
                FunDescribe_TC = "設置陽光",

                KeyDescription_EN = "Number 1",
                FunDescribe_EN = "Sun number",

                IsTrigger = true,


                IsAcceptValue = true,
                SliderMinNum = 1,
                SliderMaxNum = 9999

            };
            gameFunDateStruct.refHotKey = new Other.RefHotKey()
            {
                Vk = Keys.NumPad1,
                FsModifiers = HotKey.KeyModifiers.None,
            };
            gameFunDateStruct.AddGameDate(GameVersion.Version.Default, new Other.GameDate()
            {
                ModuleName = "PlantsVsZombies.exe",
                ModuleOffsetAddress = 0x355E0C,

                IsSignatureCode = false,

                IntPtrOffset = new uint[] { 0x868, 0x5578 },
                IsIntPtr = true,
            });

        }

        public override void Awake()
        {

        }

        public override void DoFirstTime(double value)
        {
            memory.WriteMemory<int>(gameDataAddress.Address, (int)value);
        }

        public override void DoRunAgain(double value)
        {
           
        }

        public override void Ending()
        {
            
        }
    }
}
