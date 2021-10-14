using CheatUITemplt;
using System.Collections.Generic;
using System.Threading;

namespace WPFCheatUITemplate.GameFuns
{
    class DrawWindow : GameFun
    {
        //hook 所有的寄存器的值
        //Dictionary<ASM.RegisterType, int> ret;

        //启动绘图的线程
        Thread tt;
        //绘图的管理者，用于绘图
        DrawManager drawManager;

        public DrawWindow()
        {
            this.gameFunDateStruct = new WPFCheatUITemplate.Other.GameFunDateStruct()
            {
                ModuleName = "PlantsVsZombies.exe",

                Vk = System.Windows.Forms.Keys.NumPad9,
                FsModifiers = HotKey.KeyModifiers.None,

                KeyDescription_SC = "数字键9",
                FunDescribe_SC = "外部绘制",

                KeyDescription_TC = "數字鍵9",
                FunDescribe_TC = "外部繪製",

                KeyDescription_EN = "Number 9",
                FunDescribe_EN = "External draw",

                IsTrigger = false,

            };

            drawManager = new DrawManager();

        }

        public override void Awake()
        {
            //人造指针的步骤
            //int pid = CheatTools.GetPidByHandle(gameFunDateStruct.Handle);
           
            //ASM asm = new ASM();
            //asm.Mov_EAX_DWORD_Ptr_EDI_Add(0x5578);
         
            //ret = asm.HookAllRegister(pid, (int)(this.gameFunDateStruct.ModuleAddress + 0x9f2e5), (int)(this.gameFunDateStruct.ModuleAddress + 0x9F2EB));
            
        }
       
        public override void DoFirstTime(double value)
        {
            //使用人造指针
            //int address = CheatTools.ReadMemoryValue(ret[ASM.RegisterType.EDI], gameFunDateStruct.Handle);

            //CheatTools.WriteMemoryInt(address+0x5578, gameFunDateStruct.Handle, (int)value);

            tt = new Thread(Draw);
            tt.IsBackground = true;
            tt.Start();
          
        }

        void Draw()
        {
           
            drawManager.Init("PlantsVsZombies");
            drawManager.SetBrushes((g) => { drawManager._brushes["blue"] = g.CreateSolidBrush(30, 144, 255); });
            drawManager.SetFonts((g) => { drawManager._fonts["Microsoft YaHei"] = g.CreateFont("Microsoft YaHei", 12); });
            drawManager.DrawFun((g) =>
            {

                g.DrawText(drawManager._fonts["Microsoft YaHei"], 12.0f, 
                    drawManager._brushes["blue"],
                    10, drawManager._windowData.Height / 2,
                    "测试文字");

            });
            drawManager.Run();
        }

        public override void DoRunAgain(double value)
        {
            drawManager.Close();
            tt.Abort();
        }

        public override void Ending()
        {
            
        }
    }
}
