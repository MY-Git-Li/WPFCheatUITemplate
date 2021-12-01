using CheatUITemplt;
using GameOverlay.Drawing;
using System.Collections.Generic;
using System.Threading;
using WPFCheatUITemplate.GameMode;

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
            gameFunDataAndUIStruct = Other.GameFuns.UIManager.GetCheckButtonDateStruct("外部绘制", "External draw", false);

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
        List<GameMode.Zombie> zombies;
        void Draw()
        {
           
            drawManager.Init(GameInformation.ProcessName);
            drawManager.SetBrushes((g) => { drawManager._brushes["blue"] = g.CreateSolidBrush(30, 144, 255); });
            drawManager.SetFonts((g) => { drawManager._fonts["Microsoft YaHei"] = g.CreateFont("Microsoft YaHei", 12); });
            drawManager.DrawFun((g) =>
            {

                g.DrawText(drawManager._fonts["Microsoft YaHei"], 12.0f, 
                    drawManager._brushes["blue"],
                    10, drawManager._windowData.Height / 2,
                    "测试文字");


                //zombies = GameMode.GameModeManger.GetZombies();

                //foreach (var item in zombies)
                //{
                //    //g.DrawRectangle(drawManager._brushes["blue"],
                //    //item.X+20,item.Y,item.X+100,item.Y+115,1f);

                //    //g.DrawBox2D(drawManager._brushes["blue"], drawManager._brushes["red"], item.X + 20, item.Y, item.X + 100, item.Y + 115, 1f);
                //    if (!item.IsLive.Value)
                //    {
                //        if (item.X.Value != 0 && item.Y.Value!=0)
                //        {
                //            float HpMax = (float)(item.HatMaxHp.Value + item.AnnexMaxHp.Value + item.HpMax.Value);
                //            float curentHp = (float)(item.HatHp.Value + item.AnnexHp.Value + item.Hp.Value);
                //            float percenttage = curentHp / HpMax;
                //            percenttage = percenttage * 100;
                //            g.DrawVerticalProgressBar(drawManager._brushes["blue"], drawManager._brushes["red"],
                //                Rectangle.Create(item.X.Value + 20, item.Y.Value - 10, 80, 6), 1f, percenttage);
                //            g.DrawRectangle(drawManager._brushes["blue"],
                //            item.X.Value + 20, item.Y.Value, item.X.Value + 100, item.Y.Value + 115, 1f);
                //            g.DrawText(drawManager._fonts["Microsoft YaHei"], 12.0f,
                //                drawManager._brushes["blue"],
                //                item.X.Value + 20, item.Y.Value-26,
                //                curentHp.ToString()+"/"+ HpMax.ToString());
                //        }

                //    }
                //}





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
