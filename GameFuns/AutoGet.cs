using CheatUITemplt;
using System.Windows.Forms;

namespace WPFCheatUITemplate.GameFuns
{
    class AutoGet:GameFun
    {
        public AutoGet()
        {
            gameFunDataAndUIStruct = Other.GameFuns.UIManager.GetCheckButtonDateStruct("自动获取", "Automatic acquisition", false);
        }
        public override void Awake()
        {

        }

        public override void DoFirstTime(double value)
        {
            memory.WriteMemoryByID<byte>("autoGet", new byte[] { 0xEB });
        }
    

        public override void DoRunAgain(double value)
        {
            memory.WriteMemoryByID<byte>("autoGet", new byte[] { 0x75 });
        }
        public override void Ending()
        {

        }
    }
}
