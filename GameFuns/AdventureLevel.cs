using CheatUITemplt;
using System;
using System.Windows.Forms;

namespace WPFCheatUITemplate.GameFuns
{
    class AdventureLevel:GameFun
    {
       
        public AdventureLevel()
        {
            gameFunDataAndUIStruct = Other.GameFuns.UIManager.GetButtonDateStruct("设置冒险关卡", "Adventure level", true, 1, 50); 
        }

        public override void Awake()
        {

        }

        public override void DoFirstTime(double value)
        {
            memory.WriteMemory<int>(Other.GameFuns.AddressDataManager.GetAddress("adventureLevel"), (int)value);
        }

        public override void DoRunAgain(double value)
        {

        }
        public override void Ending()
        {

        }

    }
}
