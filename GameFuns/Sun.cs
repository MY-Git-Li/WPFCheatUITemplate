using System.Windows.Forms;
using WPFCheatUITemplate.Other.GameFuns;
namespace WPFCheatUITemplate.GameFuns
{
    class Sun : GameFun
    {
        public Sun()
        {
            gameFunDataAndUIStruct = GetButtonDateStruct("设置阳光", "Sun number", true);
          
        }

        public override void Awake()
        {

        }

        public override void DoFirstTime(double value)
        {
            WriteMemoryByID<int>("sun", (int)value);
        }

        public override void DoRunAgain(double value)
        {
           
        }

        public override void Ending()
        {
            
        }
    }
}
