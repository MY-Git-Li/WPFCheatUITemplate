using WPFCheatUITemplate.Core.Tools.Extensions;
using WPFCheatUITemplate.Core.GameFuns;
using WPFCheatUITemplate.Core.Tools.ASM;
using WPFCheatUITemplate.Core.Extends;

namespace WPFCheatUITemplate.GameFuns
{
    class Test : Extends
    {
        //ASM asm;
        public Test()
        {
            //asm = new ASM();
        }
        public override void StartAsync()
        {
            //#region KeybdTest

            //Keybd.KeyClick(System.Windows.Forms.Keys.LWin, System.Windows.Forms.Keys.R);
            //System.Threading.Thread.Sleep(100);
            //Keybd.KeyWrite("notepad");
            //Keybd.KeyClick(System.Windows.Forms.Keys.Space);
            //System.Threading.Thread.Sleep(100);
            //Keybd.KeyClick(System.Windows.Forms.Keys.Enter);
            //System.Threading.Thread.Sleep(200);

            //Keybd.KeyWrite("世界上最伟大的人，是谁呢？你肯定知道的", 100);
            //Keybd.KeyClick(KeyValueEnum.vbKeyReturn);
            //Keybd.keyClickFormStr("whoisyourdady");
            //Keybd.KeyClick(KeyValueEnum.vbKeyReturn);

            //#endregion

            //#region mouseTest

            //mouse.mouse_move(new System.Drawing.Point(10, 10), new System.Drawing.Point(100, 100), 5);

            //#endregion

            //var dd = "12".NumberToChinese();

            //AppGameFunManager.Instance.UILangerManger.AddString("gg", "你好", "hi");
            
            //System.Windows.Forms.MessageBox.Show(AppGameFunManager.Instance.UILangerManger.GetString("gg"));
        }
       
    }
}
