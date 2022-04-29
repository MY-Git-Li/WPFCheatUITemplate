using System.Windows.Forms;
using WPFCheatUITemplate.Other.GameFuns;
namespace WPFCheatUITemplate.GameFuns
{
    class Sun : GameFun
    {
        public Sun()
        {
            gameFunDataAndUIStruct = GetButtonDateStruct("设置阳光", "Sun number", 1, 99999,99999);
           
        }

        public override void Start()
        {
           WriteMemoryByID("unlock_sun_limit");
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
           WriteMemoryByID("unlock_sun_limit",true);
        }
    }
}
