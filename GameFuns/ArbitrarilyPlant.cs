using System.Windows.Forms;
using WPFCheatUITemplate.Other.GameFuns;
namespace WPFCheatUITemplate.GameFuns
{
    class ArbitrarilyPlant : GameFun
    {
       
        public ArbitrarilyPlant()
        {
            gameFunDataAndUIStruct = GetCheckButtonDateStruct("随意种植", "Planting will", false);
        }
        public override void Awake()
        {

        }

        public override void DoFirstTime(double value)
        {
            WriteMemoryByID("arbitrarilyPlant");
        }

        public override void DoRunAgain(double value)
        {
            WriteMemoryByID("arbitrarilyPlant", true);
        }
        public override void Ending()
        {

        }
    }
}
