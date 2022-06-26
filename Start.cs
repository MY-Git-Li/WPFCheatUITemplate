using System.Windows.Forms;
using WPFCheatUITemplate.GameFuns;
using WPFCheatUITemplate.Core;
using WPFCheatUITemplate.Core.GameFuns;
using WPFCheatUITemplate.Core.Tools;
using WPFCheatUITemplate.Core.Tools.ASM;

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
            new FastGameFun(GetCheckButtonDateStruct("游戏速度", "Game speed", 0.1f, 10f))
            {
                doFirstTime = (v) =>
                {
                    WriteMemoryByID<int>("GameRunSpeed", (int)(10f / v ));
                },

                doRunAgain = (v) =>
                {
                    WriteMemoryByID<int>("GameRunSpeed", 10);
                },
            };
            new FastGameFun(GetButtonDateStruct("直接胜利", "Win"))
            {
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
            new ArbitrarilyPlant();SetToolTip("这个功能允许你可以随意的在草坪上种植，包括已经种上植物的土地", "This function allows you to plant on the lawn at will, including the land that has been planted with plants");
            new AllowBackground(); 

            SetCurentKeyModifiers(HotKey.KeyModifiers.Alt, Keys.NumPad1);
            new FastGameFun(GetCheckButtonDateStruct("超级攻速", "Super attack speed"), "supershoot", "supershoot2");
            new FastGameFun(GetCheckButtonDateStruct("超级传送带", "Super conveyor belt"), "fast_belt_1", "fast_belt_2");
            new FastGameFun(GetCheckButtonDateStruct("花瓶透视", "Vase Perspective"), "see_vase");
        }
    }
}
