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
            UIManager.CreatSeparate("基本属性", "Basic properties");
            new AutoGet();
            new ArbitrarilyPlant();
            new AllowBackground();

            UIManager.SetCurentKeyModifiers(HotKey.KeyModifiers.Alt, Keys.NumPad1);
            new FastGameFun()
            {
                gameFunDataAndUIStruct =UIManager.GetCheckButtonDateStruct("超级攻速", "Super attack speed",false),

                doFirstTime = (i, v) =>
                {
                    i.memory.WriteMemory<byte>(AddressDataManager.GetAddress("supershoot"), new byte[] { 0xB9, 0x22, 0x00, 0x00, 0x00 });
                    i.memory.WriteMemory<byte>(AddressDataManager.GetAddress("supershoot2"), new byte[] { 0x0F, 0x84 });
                },
                doRunAgain = (i, v) =>
                {
                    i.memory.WriteMemory<byte>(AddressDataManager.GetAddress("supershoot"), new byte[] { 0x8B, 0x4E, 0x5C, 0x2B, 0xC8 });
                    i.memory.WriteMemory<byte>(AddressDataManager.GetAddress("supershoot2"), new byte[] { 0x0F, 0x85 });
                },

            }.Go();
            //new Test();
        }
    }
}
