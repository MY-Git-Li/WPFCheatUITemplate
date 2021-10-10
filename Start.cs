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
            new DrawWindow();
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
                    ModuleOffsetAddress = 0x6DC21,

                    IsSignatureCode = false,
                    IsIntPtr = false,

                    Vk = Keys.NumPad1,
                    FsModifiers = HotKey.KeyModifiers.Shift,

                    KeyDescription_SC = "Shift+数字键1",
                    FunDescribe_SC = "超级攻速",

                    KeyDescription_TC = "Shift+數字鍵1",
                    FunDescribe_TC = "超級攻速",

                    KeyDescription_EN = "Shift+Number 1",
                    FunDescribe_EN = "Super attack speed",

                    IsTrigger = false

                },

                awake = (i) => 
                {
                    i.gameDataAddresseList.Add(new GameDataAddress(i.gameFunDateStruct.Handle,i.gameFunDateStruct.ModuleAddress+ 0x72EE4));
                },

                doFirstTime = (i, v) => 
                {
                    CheatTools.WriteMemoryByte(i.gameFunDateStruct.GameDataAddress.Address, i.gameFunDateStruct.Handle, new byte[] { 0xB9, 0x22, 0x00, 0x00, 0x00 });
                    CheatTools.WriteMemoryByte(i.gameDataAddresseList[0].Address, i.gameFunDateStruct.Handle, new byte[] { 0x0F,0x84 });

                },
                doRunAgain = (i,v) => 
                {
                    CheatTools.WriteMemoryByte(i.gameFunDateStruct.GameDataAddress.Address, i.gameFunDateStruct.Handle, new byte[] { 0x8B, 0x4E, 0x5C, 0x2B, 0xC8 });
                    CheatTools.WriteMemoryByte(i.gameDataAddresseList[0].Address, i.gameFunDateStruct.Handle, new byte[] { 0x0F, 0x85 });
                },

                ending = (i)=>
                {
                    i.gameDataAddresseList.Clear();
                },

            }.Start();
        }
    }
}
