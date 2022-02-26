using CheatUITemplt;
using System.Windows.Forms;

namespace WPFCheatUITemplate.GameFuns
{
    class AllowBackground:GameFun
    {
       
        public AllowBackground()
        {
            gameFunDataAndUIStruct = GetCheckButtonDateStruct("允许后台运行", "Running in background", false);
        }

        public override void Awake()
        {

        }

        public override void DoFirstTime(double value)
        {
            memory.WriteMemoryByID("allowBackground");
      
        }

        public override void DoRunAgain(double value)
        {
            memory.WriteMemoryByID("allowBackground",true);
        }
        public override void Ending()
        {

        }
    }
}

