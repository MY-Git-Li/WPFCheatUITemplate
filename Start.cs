using CheatUITemplt;
using System;
using System.Windows.Forms;
using WPFCheatUITemplate.GameFuns;
using WPFCheatUITemplate.Other;
using WPFCheatUITemplate.Other.GameFuns;

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
            new DefaultPlantLayout();
            new NoCd();
            new DrawWindow();
            AppGameFunManger.Instance.CreatSeparate("基本属性", "基本屬性", "Basic properties");
            new AutoGet();
            new ArbitrarilyPlant();
            new AllowBackground();
            new FastGameFun()
            {

                gameFunDateStruct = new GameFunDateStruct()
                {
                    uIData = new UIData()
                    {
                        KeyDescription_SC = "Alt+数字键1",
                        FunDescribe_SC = "超级攻速",

                        KeyDescription_TC = "Alt+數字鍵1",
                        FunDescribe_TC = "超級攻速",

                        KeyDescription_EN = "Alt+Number 1",
                        FunDescribe_EN = "Super attack speed",

                        IsTrigger = false
                    },
                    refHotKey = new RefHotKey()
                    {
                        Vk = Keys.NumPad1,
                        FsModifiers = HotKey.KeyModifiers.Alt,
                    },

                },

                setGameDate = (i) =>
                {
                    i.gameDates.Add(GameVersion.Version.Default, new GameDate()
                    {
                        ModuleName = "PlantsVsZombies.exe",
                        ModuleOffsetAddress = 0x6DC21,

                        IsSignatureCode = false,
                        IsIntPtr = false,
                    });
                },

                awake = (i) =>
                {
                    i.gameDataAddresseList.Add(new GameDataAddress((IntPtr)i.gameFunDateStruct.Pid, CheatTools.GetProcessModuleHandle((uint)i.gameFunDateStruct.Pid, "PlantsVsZombies.exe") + 0x72EE4));
                },

                doFirstTime = (i, v) =>
                {
                    i.memory.WriteMemory<byte>(i.gameDataAddress.Address, new byte[] { 0xB9, 0x22, 0x00, 0x00, 0x00 });
                    i.memory.WriteMemory<byte>(i.gameDataAddresseList[0].Address, new byte[] { 0x0F, 0x84 });
                },
                doRunAgain = (i, v) =>
                {
                    i.memory.WriteMemory<byte>(i.gameDataAddress.Address, new byte[] { 0x8B, 0x4E, 0x5C, 0x2B, 0xC8 });
                    i.memory.WriteMemory<byte>(i.gameDataAddresseList[0].Address, new byte[] { 0x0F, 0x85 });
                },

                ending = (i) =>
                {
                    i.gameDataAddresseList.Clear();
                },

            }.Go();

            GameFunDateStructManger.SetCurentKeyModifiers(Keys.NumPad9);
            new FastGameFun()
            {
                gameFunDateStruct = GameFunDateStructManger.CheckButtonDateStruct("测试1", "Test 1"),

            }.Go();
            new FastGameFun()
            {
                gameFunDateStruct = GameFunDateStructManger.CheckButtonDateStruct("测试2", "Test 2",true),

            }.Go();
        }
    }
}
