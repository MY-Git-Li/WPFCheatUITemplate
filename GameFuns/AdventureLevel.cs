using CheatUITemplt;
using System;
using System.Windows.Forms;

namespace WPFCheatUITemplate.GameFuns
{
    class AdventureLevel:GameFun
    {
       
        public AdventureLevel()
        {
            this.gameFunDateStruct = new WPFCheatUITemplate.Other.GameFunDateStruct()
            {
                ModuleName = "PlantsVsZombies.exe",
                ModuleOffsetAddress = 0x355E0C,

                IsSignatureCode = false,

                IntPtrOffset = new uint[] { 0x950, 0x4c },
                IsIntPtr = true,

                Vk = Keys.NumPad3,
                FsModifiers = HotKey.KeyModifiers.None,

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
            

            AppGameFunManger.Instance.RegisterGameFun(this);
        }

        public override void Awake()
        {

        }

        public override void DoFirstTime(double value)
        {
            CheatTools.WriteMemoryInt(gameFunDateStruct.GameDataAddress.Address, gameFunDateStruct.Handle, (int)value);
        }

        public override void DoRunAgain(double value)
        {

        }
        public override void Ending()
        {

        }

    }
}
