using CheatUITemplt;
using System.Windows.Forms;


namespace WPFCheatUITemplate.GameFuns
{
    class ChangeMode:GameFun
    {
      
        public ChangeMode()
        {
            gameFunDataAndUIStruct = Other.GameFuns.UIManager.GetButtonDateStruct("改变模式", "Change mode", true, 1, 70);
           
        }

        public override void Awake()
        {

        }
        public override void DoFirstTime(double value)
        {
            memory.WriteMemory<int>(Other.GameFuns.AddressDataManager.GetAddress("changeMode"), (int)value);
        }

        public override void DoRunAgain(double value)
        {

        }
        public override void Ending()
        {

        }
    }
}
