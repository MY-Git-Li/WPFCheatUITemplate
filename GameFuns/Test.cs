using CheatUITemplt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFCheatUITemplate.Other.GameFuns;

namespace WPFCheatUITemplate.GameFuns
{
    class Test : GameFun
    {
        ASM asm;
        public Test()
        {
            gameFunDateStruct = GameFunDateStructManger.CheckButtonDateStruct("测试", "Test", false);
            asm = new ASM();
        }

        public override void Awake()
        {
            
        }

        public override void DoFirstTime(double value)
        {
            asm.HookAllRegister(gameFunDateStruct.Pid, 0x0061EBFC, 0x0061EC01);
        }

        public override void DoRunAgain(double value)
        {
            asm.RecoveryRegisterHook(gameFunDateStruct.Pid);
        }

        public override void Ending()
        {
           
        }
    }
}
