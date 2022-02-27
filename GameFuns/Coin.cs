using CheatUITemplt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WPFCheatUITemplate.GameFuns
{
    class Coin: GameFun
    {
        public Coin()
        {
            gameFunDataAndUIStruct = GetButtonDateStruct("设置硬币", "Coin number", true);
           
        }

        public override void Awake()
        {

        }

        public override void DoFirstTime(double value)
        {
            WriteMemoryByID<int>("coin", (int)value);
        }

        public override void DoRunAgain(double value)
        {

        }
        public override void Ending()
        {

        }
    }
}
