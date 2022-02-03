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
            UIManager.SetCurentKeyModifiers(HotKey.KeyModifiers.None, Keys.NumPad1);
            new Sun();
            new Coin();
            new AdventureLevel();
            new ChangeMode();
            new Tree();
            //"直接胜利"
            new FastGameFun()
            {

                gameFunDataAndUIStruct = UIManager.GetButtonDateStruct("直接胜利", "Win", false),

                doFirstTime = (i, v) =>
                {
                    var add = AddressDataManager.GetAddress("Win_Call_ECX");
                    var ecx = i.memory.ReadMemory<int>(add);
                    ASM asm = new ASM();
                    asm.Mov_EAX(ecx);
                    asm.Mov_ECX_EAX();
                    asm.Mov_EAX(AddressDataManager.GetAddress("Win_Call").ToInt32());
                    asm.Call_EAX();
                    asm.Ret();
                    asm.RunAsm(GameMode.GameInformation.Pid);

                },

            }.Go();
            new DefaultPlantLayout();
            new NoCd();
            new DrawWindow();

            UIManager.CreatSeparate("基本属性", "Basic properties");
            UIManager.SetCurentKeyModifiers(HotKey.KeyModifiers.Shift, Keys.W);
            new AutoGet();

            UIManager.SetCurentKeyModifiers(HotKey.KeyModifiers.Ctrl, Keys.NumPad1);
            new ArbitrarilyPlant();
            new AllowBackground();

            UIManager.SetCurentKeyModifiers(HotKey.KeyModifiers.Alt, Keys.NumPad1);
            //"超级攻速"
            new FastGameFun()
            {
                gameFunDataAndUIStruct = UIManager.GetCheckButtonDateStruct("超级攻速", "Super attack speed"),

                doFirstTime = (i, v) =>
                {
                    i.memory.WriteMemoryByID("supershoot");
                    i.memory.WriteMemoryByID("supershoot2");
                },
                doRunAgain = (i, v) =>
                {
                    i.memory.WriteMemoryByID("supershoot", true);
                    i.memory.WriteMemoryByID("supershoot2", true);
                },

            }.Go();
            //new Test();
        }
    }
}
