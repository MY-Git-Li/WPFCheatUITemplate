using CheatUITemplt;
using System.Windows.Forms;

namespace WPFCheatUITemplate.GameFuns
{
    class ArbitrarilyPlant : GameFun
    {
       
        public ArbitrarilyPlant()
        {
            gameFunDataAndUIStruct = Other.GameFuns.UIManager.GetCheckButtonDateStruct("随意种植", "Planting will", false);
        }
        public override void Awake()
        {

        }

        public override void DoFirstTime(double value)
        {
            memory.WriteMemoryByID("arbitrarilyPlant");
        }

        public override void DoRunAgain(double value)
        {
            memory.WriteMemoryByID("arbitrarilyPlant", true);
        }
        public override void Ending()
        {

        }
    }
}
