using System.Threading;
using System.Windows.Forms;
using WPFCheatUITemplate.Core.GameFuns;
using WPFCheatUITemplate.Core.Tools.ASM;
using static Iced.Intel.AssemblerRegisters;
namespace WPFCheatUITemplate.GameFuns
{
    class DefaultPlantLayout:GameFun
    {
 
        public DefaultPlantLayout()
        {
            gameFunDataAndUIStruct = GetButtonDateStruct("默认植物种植", "Default planting", false);
        }

        public void Plant(int x, int y, int id)
        {
            //ASM asm = new ASM();
            //asm.Pushad();
            //asm.Push68(-1);
            //asm.Push68(id);
            //asm.Mov_EAX(x);
            //asm.Push68(y);
            //var offset = ReadMemory<int>(GetAddress("Secondary_Offset"));
            //asm.Push68(offset);
            //asm.Mov_EBX(GetAddress("Plant_Call").ToInt32());
            //asm.Call_EBX();
            //asm.Popad();
            //asm.Ret(); 
            //asm.RunAsm(pid);

            Assemble asm = new Assemble(32);
            asm.pushad();
            asm.push(-1);
            asm.push(id);
            asm.mov(eax, x);
            asm.push(y);
            asm.push(ReadMemory<int>(GetAddress("Secondary_Offset")));
            asm.mov(ebx, GetAddress("Plant_Call").ToInt32());
            asm.call(ebx);
            asm.popad();
            asm.ret();
            asm.RunAsm();
        }

        public override void DoFirstTime(double value)
        {
            for (int i = 0; i < 5; i++)
            {
                Plant(i, 0, 40);
                Thread.Sleep(10);
                Plant(i, 0, 30);
                Thread.Sleep(10);

                Plant(i, 1, 40);
                Thread.Sleep(10);
                Plant(i, 1, 30);
                Thread.Sleep(10);

                Plant(i, 2, 43);
                Thread.Sleep(10);
                Plant(i, 2, 30);
                Thread.Sleep(10);

                Plant(i, 3, 43);
                Thread.Sleep(10);
                Plant(i, 3, 30);
                Thread.Sleep(10);

                Plant(i, 4, 44);
                Thread.Sleep(10);
                Plant(i, 4, 30);
                Thread.Sleep(10);

                Plant(i, 5, 44);
                Thread.Sleep(10);
                Plant(i, 5, 30);
                Thread.Sleep(10);

                Plant(i, 6, 22);
                Thread.Sleep(10);
                Plant(i, 6, 30);
                Thread.Sleep(10);

                Plant(i, 7, 23);
                Thread.Sleep(10);
                Plant(i, 7, 30);
                Thread.Sleep(10);

                Plant(i, 8, 46);
                Thread.Sleep(10);
            }
        }

        public override void DoRunAgain(double value)
        {
           
        }
        public override void Ending()
        {

        }
    }
}
