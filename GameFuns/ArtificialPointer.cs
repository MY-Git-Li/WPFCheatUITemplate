using CheatUITemplt;
using System.Collections.Generic;
using System.Threading;

namespace WPFCheatUITemplate.GameFuns
{
    class ArtificialPointer : GameFun
    {

        Dictionary<ASM.RegisterType, int> ret;

        public ArtificialPointer()
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

                IsAcceptValue = false,
                SliderMinNum = 1,
                SliderMaxNum = 9999,
            };
            

            GameFunManger.Instance.RegisterGameFun(this);
        }

        public override void Awake()
        {
            int pid = CheatTools.GetPidByHandle(gameFunDateStruct.Handle);
           
            ASM asm = new ASM();
            asm.Mov_EAX_DWORD_Ptr_EDI_Add(0x5578);
         
            ret = asm.HookAllRegister(pid, (int)(this.gameFunDateStruct.ModuleAddress + 0x9f2e5), (int)(this.gameFunDateStruct.ModuleAddress + 0x9F2EB));
            
        }
        Thread tt;
        DrawManager drawManager;
        public override void DoFirstTime(double value)
        {
            //int address = CheatTools.ReadMemoryValue(ret[ASM.RegisterType.EDI], gameFunDateStruct.Handle);


            //CheatTools.WriteMemoryInt(address+0x5578, gameFunDateStruct.Handle, (int)value);

            tt = new Thread(Draw);
            tt.IsBackground = true;
            tt.Start();
          
        }

        void Draw()
        {
            drawManager = new DrawManager();
            drawManager.Init("PlantsVsZombies");
            drawManager.SetBrushes((g) => { drawManager._brushes["blue"] = g.CreateSolidBrush(30, 144, 255); });
            drawManager.SetFonts((g) => { drawManager._fonts["Microsoft YaHei"] = g.CreateFont("Microsoft YaHei", 12); });
            drawManager.DrawFun((g) =>
            {

                g.DrawText(drawManager._fonts["Microsoft YaHei"], 12.0f, 
                    drawManager._brushes["blue"], 
                    10, drawManager._windowData.Height / 2,
                    "这里是测试");

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
            //CheatTools.WriteMemoryByte((int)this.gameFunDateStruct.ModuleAddress + 0x9f2e5, gameFunDateStruct.Handle, new byte[] { 0x8B, 0x87, 0x78, 0x55, 0x00, 0x00 });
        }
    }
}
