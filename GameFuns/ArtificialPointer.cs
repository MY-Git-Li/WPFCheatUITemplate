using CheatUITemplt;
using System.Collections.Generic;


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
                FunDescribe_SC = "人造指针设置阳光",

                KeyDescription_TC = "數字鍵9",
                FunDescribe_TC = "人造指針設置陽光",

                KeyDescription_EN = "Number 9",
                FunDescribe_EN = "ArtificialPointer Sun number",

                IsTrigger = true,


                IsAcceptValue = true,
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

        public override void DoFirstTime(double value)
        {
            int address = CheatTools.ReadMemoryValue(ret[ASM.RegisterType.EDI], gameFunDateStruct.Handle);


            CheatTools.WriteMemoryInt(address+0x5578, gameFunDateStruct.Handle, (int)value);
        }

        public override void DoRunAgain(double value)
        {
           
        }
        public override void Ending()
        {

        }
    }
}
