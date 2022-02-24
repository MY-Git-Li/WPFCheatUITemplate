using CheatUITemplt;
using System;
using System.Windows.Forms;
using WPFCheatUITemplate.GameFuns;
using WPFCheatUITemplate.Other;
using WPFCheatUITemplate.Other.GameFuns;

namespace WPFCheatUITemplate
{
    class Start: ViewMenu
    {
       public static void Init()
        {
           SetCurentKeyModifiers(HotKey.KeyModifiers.None, Keys.NumPad1);
            new Sun();
            new Coin();
            new AdventureLevel();
            new ChangeMode();
            new Tree();
            //"直接胜利"
            new FastGameFun()
            {

                gameFunDataAndUIStruct = GetButtonDateStruct("直接胜利", "Win", false),

                doFirstTime = (i, v) =>
                {
                    var add = GetAddress("Secondary_Offset");
                    var ecx = i.memory.ReadMemory<int>(add);
                    ASM asm = new ASM();
                    asm.Mov_EAX(ecx);
                    asm.Mov_ECX_EAX();
                    asm.Mov_EAX(GetAddress("Win_Call").ToInt32());
                    asm.Call_EAX();
                    asm.Ret();
                    asm.RunAsm(GameMode.GameInformation.Pid);

                },

            };
            new DefaultPlantLayout();
            new NoCd();
            new DrawWindow();

            CreatSeparate("基本属性", "Basic properties");
            SetCurentKeyModifiers(HotKey.KeyModifiers.Shift, Keys.W);
            new AutoGet();

            SetCurentKeyModifiers(HotKey.KeyModifiers.Ctrl, Keys.NumPad1);
            new ArbitrarilyPlant();
            new AllowBackground();

            SetCurentKeyModifiers(HotKey.KeyModifiers.Alt, Keys.NumPad1);
            //"超级攻速"
            new FastGameFun()
            {
                gameFunDataAndUIStruct = GetCheckButtonDateStruct("超级攻速", "Super attack speed"),

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

            };
            //"超级传送带"
            new FastGameFun(GetCheckButtonDateStruct("超级传送带", "Super conveyor belt"), "fast_belt_1", "fast_belt_2");
           
        }
    }
}
