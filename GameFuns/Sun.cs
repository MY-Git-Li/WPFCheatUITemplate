using System.Windows.Forms;
using WPFCheatUITemplate.Other.GameFuns;
namespace WPFCheatUITemplate.GameFuns
{
    class Sun : GameFun
    {
        bool isDo;
        public Sun()
        {
            gameFunDataAndUIStruct = GetButtonDateStruct("设置阳光", "Sun number", 1, 99999);
            isDo = true;
        }

        public override void Start()
        {
            if (isDo)
            {
                WriteMemoryByID("unlock_sun_limit");
                isDo = false;
            }
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
            isDo = true;
            WriteMemoryByID("unlock_sun_limit",true);
        }
    }
}
