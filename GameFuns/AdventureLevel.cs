using CheatUITemplt;
using System;
using System.Windows.Forms;

namespace WPFCheatUITemplate.GameFuns
{
    class AdventureLevel:GameFun
    {
       
        public AdventureLevel()
        {
            gameFunDataAndUIStruct = GetButtonDateStruct("设置冒险关卡", "Adventure level", 1, 50); 
        }

        public override void Awake()
        {

        }

        public override void DoFirstTime(double value)
        {
            WriteMemoryByID<int>("adventureLevel", (int)value);
        }

        public override void DoRunAgain(double value)
        {

        }
        public override void Ending()
        {

        }

    }
}
