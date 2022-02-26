using CheatUITemplt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFCheatUITemplate.Other.GameFuns;
using WPFCheatUITemplate.Other.Tools;

namespace WPFCheatUITemplate.GameFuns
{
    class Test : GameFun
    {
        ASM asm;
        public Test()
        {
            gameFunDataAndUIStruct = GetCheckButtonDateStruct("测试", "Test", false);
            asm = new ASM();
        }

        public override void Awake()
        {
           
        }

        public override void DoFirstTime(double value)
        {
            #region KeybdTest

            Keybd.KeyClick(System.Windows.Forms.Keys.LWin, System.Windows.Forms.Keys.R);
            System.Threading.Thread.Sleep(100);
            Keybd.KeyWrite("notepad");
            Keybd.KeyClick(System.Windows.Forms.Keys.Space);
            System.Threading.Thread.Sleep(100);
            Keybd.KeyClick(System.Windows.Forms.Keys.Enter);
            System.Threading.Thread.Sleep(200);

            Keybd.KeyWrite("世界上最伟大的人，是谁呢？你肯定知道的",100);
            Keybd.KeyClick(KeyValueEnum.vbKeyReturn);
            Keybd.keyClickFormStr("whoisyourdady");
            Keybd.KeyClick(KeyValueEnum.vbKeyReturn);
            #endregion

            #region mouseTest

            mouse.mouse_move(new System.Drawing.Point(10, 10), new System.Drawing.Point(100, 100), 5);

            #endregion
            //var dd = AddressDataManager.GetAddress("sun");

            //asm.HookAllRegister(gameFunDateStruct.Pid, 0x0061EBFC, 0x0061EC01);
        }

        public override void DoRunAgain(double value)
        {
            //asm.RecoveryRegisterHook(gameFunDataAndUIStruct.Pid);
        }

        public override void Ending()
        {
           
        }
    }
}
