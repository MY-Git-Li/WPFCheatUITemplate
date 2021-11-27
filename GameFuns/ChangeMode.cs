﻿using CheatUITemplt;
using System.Windows.Forms;


namespace WPFCheatUITemplate.GameFuns
{
    class ChangeMode:GameFun
    {
      
        public ChangeMode()
        {
            gameFunDateStruct = new Other.GameFunDataStruct();
            gameFunDateStruct.uIData = new Other.UIData()
            {
                KeyDescription_SC = "数字键4",
                FunDescribe_SC = "改变模式",

                KeyDescription_TC = "數字鍵4",
                FunDescribe_TC = "改變模式",

                KeyDescription_EN = "Number 4",
                FunDescribe_EN = "Change mode",

                IsTrigger = true,


                IsAcceptValue = true,
                SliderMinNum = 1,
                SliderMaxNum = 70,

            };
            gameFunDateStruct.refHotKey = new Other.RefHotKey()
            {
                Vk = Keys.NumPad4,
                FsModifiers = HotKey.KeyModifiers.None,
            };
            gameFunDateStruct.AddGameDate(GameVersion.Version.Default, new Other.GameData()
            {
                ModuleName = "PlantsVsZombies.exe",
                ModuleOffsetAddress = 0x355E0C,

                IsSignatureCode = false,

                IntPtrOffset = new uint[] { 0x91c },
                IsIntPtr = true,
            });

        }

        public override void Awake()
        {

        }
        public override void DoFirstTime(double value)
        {
            memory.WriteMemory<int>(gameDataAddress.Address, (int)value);
        }

        public override void DoRunAgain(double value)
        {

        }
        public override void Ending()
        {

        }
    }
}
