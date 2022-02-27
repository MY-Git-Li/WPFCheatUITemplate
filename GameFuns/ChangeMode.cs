using CheatUITemplt;
using System.Windows.Forms;


namespace WPFCheatUITemplate.GameFuns
{
    class ChangeMode:GameFun
    {
      
        public ChangeMode()
        {
            gameFunDataAndUIStruct = GetButtonDateStruct("改变模式", "Change mode",1, 70);
           
        }

        public override void Awake()
        {

        }
        public override void DoFirstTime(double value)
        {
            memory.WriteMemoryByID<int>("changeMode", (int)value);
        }

        public override void DoRunAgain(double value)
        {

        }
        public override void Ending()
        {

        }
    }
}
