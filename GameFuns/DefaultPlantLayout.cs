using CheatUITemplt;
using System.Threading;
using System.Windows.Forms;

namespace WPFCheatUITemplate.GameFuns
{
    class DefaultPlantLayout:GameFun
    {
        
        int pid;
        public DefaultPlantLayout()
        {

            this.gameFunDateStruct = new WPFCheatUITemplate.Other.GameFunDateStruct()
            {
                Vk = Keys.NumPad8,
                FsModifiers = HotKey.KeyModifiers.Alt,

                KeyDescription_SC = "Alt+数字键8",
                FunDescribe_SC = "默认植物种植",

                KeyDescription_TC = "Alt+數字鍵8",
                FunDescribe_TC = "默認植物種植",

                KeyDescription_EN = "Alt+Number 8",
                FunDescribe_EN = "Default planting",

                IsTrigger = true,
            };
           
           
            GameFunManger.Instance.RegisterGameFun(this);
        }

        public void Plant(int x, int y, int id)
        {
            ASM asm = new ASM();
            asm.Pushad();
            asm.Push68(-1);
            asm.Push68(id);
            asm.Mov_EAX(x);
            asm.Push68(y);
            asm.Mov_ECX_DWORD_Ptr(0x755E0C);
            asm.Mov_ECX_DWORD_Ptr_ECX_Add(0x868);
            asm.Push_ECX();
            asm.Mov_EBX(0x00418D70);
            asm.Call_EBX();
            asm.Popad();
            asm.Ret();
            asm.RunAsm(pid);
        }

        public override void Awake()
        {

        }

        public override void DoFirstTime(double value)
        {
            pid = CheatTools.GetPidByHandle(gameFunDateStruct.Handle);
            for (int i = 0; i < 5; i++)
            {
                Plant(i, 0, 40);
                Thread.Sleep(50);
                Plant(i, 1, 40);
                Thread.Sleep(50);
                Plant(i, 2, 43);
                Thread.Sleep(50);
                Plant(i, 3, 43);
                Thread.Sleep(50);
                Plant(i, 4, 44);
                Thread.Sleep(50);
                Plant(i, 5, 44);
                Thread.Sleep(50);
                Plant(i, 6, 22);
                Thread.Sleep(50);
                Plant(i, 7, 23);
                Thread.Sleep(50);
                Plant(i, 8, 46);
                Thread.Sleep(50);
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
