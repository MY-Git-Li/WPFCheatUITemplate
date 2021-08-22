using CheatUITemplt;
using System.Windows.Forms;
using WPFCheatUITemplate.GameFuns;
using WPFCheatUITemplate.Other;
namespace WPFCheatUITemplate
{
    class Start
    {
       public static void Init()
       {
            new Sun();
            new Coin();
            new AdventureLevel();
            new ChangeMode();
            new Tree();
            new ArtificialPointer();
            new NoCd();
            new AutoGet();
            new ArbitrarilyPlant();
            new AllowBackground();
            new DefaultPlantLayout();
            new FastGameFun()
            {
                gameFunDateStruct = new GameFunDateStruct()
                {
                    ModuleName = "PlantsVsZombies.exe",
                    ModuleOffsetAddress = 0x355E0C,

                    IsSignatureCode = false,

                    IntPtrOffset = new uint[] { 0x868, 0x5578 },
                    IsIntPtr = true,

                    Vk = Keys.NumPad1,
                    FsModifiers = HotKey.KeyModifiers.Shift,

                    KeyDescription_SC = "Shift+数字键1",
                    FunDescribe_SC = "设置阳光",

                    KeyDescription_TC = "Shift+數字鍵1",
                    FunDescribe_TC = "設置陽光",

                    KeyDescription_EN = "Shift+Number 1",
                    FunDescribe_EN = "Sun number",

                    IsTrigger = true,


                    IsAcceptValue = true,
                    SliderMinNum = 1,
                    SliderMaxNum = 9999
                },

                doFirstTime = (i, v) => {
                    CheatTools.WriteMemoryInt(i.gameFunDateStruct.GameDataAddress.Address, i.gameFunDateStruct.Handle, (int)v);
                }

            }.Start();
       }
    }
}
