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
            AppGameFunManager.Instance.CreatSeparate("基本属性", "Basic properties");
            new AutoGet();
            new ArbitrarilyPlant();
            new AllowBackground();

            GameFunDataStructManager.SetCurentKeyModifiers(HotKey.KeyModifiers.Alt, Keys.NumPad1);
            new FastGameFun()
            {
                gameFunDateStruct =GameFunDataStructManager.CheckButtonDateStruct("超级攻速", "Super attack speed",false),

                setGameDate = (i) =>
                {
                    i.gameDates.Add(GameVersion.Version.Default, new GameData()
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
            //new Test();
        }
    }
}
