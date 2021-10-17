using CheatUITemplt;
using System;
using System.Windows.Forms;

namespace WPFCheatUITemplate.GameFuns
{
    class AdventureLevel:GameFun
    {
       
        public AdventureLevel()
        {
            gameFunDateStruct = new Other.GameFunDateStruct();
            gameFunDateStruct.uIData = new Other.UIData()
            {
                KeyDescription_SC = "数字键3",
                FunDescribe_SC = "设置冒险关卡",

                KeyDescription_TC = "數字鍵3",
                FunDescribe_TC = "設置冒險關卡",

                KeyDescription_EN = "Number 3",
                FunDescribe_EN = "Adventure level",

                IsTrigger = true,


                IsAcceptValue = true,
                SliderMinNum = 1,
                SliderMaxNum = 50,

            };
            gameFunDateStruct.refHotKey = new Other.RefHotKey()
            {
                Vk = Keys.NumPad3,
                FsModifiers = HotKey.KeyModifiers.None,
            };
            gameFunDateStruct.AddGameDate(GameVersion.Version.Null, new Other.GameDate()
            {
                ModuleName = "PlantsVsZombies.exe",
                ModuleOffsetAddress = 0x355E0C,

                IsSignatureCode = false,

                IntPtrOffset = new uint[] { 0x950, 0x4c },
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
