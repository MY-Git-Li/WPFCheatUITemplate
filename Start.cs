﻿using System.Windows.Forms;
using WPFCheatUITemplate.GameFuns;
using WPFCheatUITemplate.Other;
using WPFCheatUITemplate.Other.GameFuns;
using WPFCheatUITemplate.Other.Tools;
namespace WPFCheatUITemplate
{
    class Start : ViewMenu
    {
        public static void Init()
        {
            new Sun();
            new Coin();
            new AdventureLevel();
            new ChangeMode();
            new Tree();
            //"游戏速度"
            new FastGameFun()
            {
                gameFunDataAndUIStruct = GetCheckButtonDateStruct("游戏速度", "Game speed", 0.1f, 10f),

                doFirstTime = (v) =>
                {
                    WriteMemoryByID<int>("GameRunSpeed", (int)(10f / v ));
                },

                doRunAgain = (v) =>
                {
                    WriteMemoryByID<int>("GameRunSpeed", 10);
                },
            };
            //"直接胜利"
            new FastGameFun()
            {

                gameFunDataAndUIStruct = GetButtonDateStruct("直接胜利", "Win", false),

                doFirstTime = (v) =>
                {
                    var ecx = ReadMemoryByID<int>("Secondary_Offset");
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
            new FastGameFun(GetCheckButtonDateStruct("超级攻速", "Super attack speed"), "supershoot", "supershoot2");
            //"超级传送带"
            new FastGameFun(GetCheckButtonDateStruct("超级传送带", "Super conveyor belt"), "fast_belt_1", "fast_belt_2");
            //"花瓶透视"
            new FastGameFun(GetCheckButtonDateStruct("花瓶透视", "Vase Perspective"), "see_vase");
        }
    }
}
