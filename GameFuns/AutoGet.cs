﻿using CheatUITemplt;
using System.Windows.Forms;

namespace WPFCheatUITemplate.GameFuns
{
    class AutoGet:GameFun
    {
        public AutoGet()
        {
            gameFunDataAndUIStruct = GetCheckButtonDateStruct("自动获取", "Automatic acquisition", false);
        }
        public override void Awake()
        {

        }

        public override void DoFirstTime(double value)
        {
            WriteMemoryByID<byte>("autoGet", new byte[] { 0xEB });
        }
    

        public override void DoRunAgain(double value)
        {
            WriteMemoryByID<byte>("autoGet", new byte[] { 0x75 });
        }
        public override void Ending()
        {

        }
    }
}
