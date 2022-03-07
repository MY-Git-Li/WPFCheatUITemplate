using System.Windows.Forms;
using WPFCheatUITemplate.Other.GameFuns;
namespace WPFCheatUITemplate.GameFuns
{
    class Tree : GameFun
    {

        public Tree()
        {
            gameFunDataAndUIStruct = GetButtonDateStruct("设置智慧树高度", "Tree height", true);

        }

        public override void Awake()
        {

        }

        public override void DoFirstTime(double value)
        {
            WriteMemoryByID<int>("tree", (int)value);
        }

        public override void DoRunAgain(double value)
        {

        }
        public override void Ending()
        {

        }
    }
}
