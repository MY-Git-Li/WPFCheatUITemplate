using CheatUITemplt;
using System;
using System.Windows.Forms;

namespace WPFCheatUITemplate.GameFuns
{
    class Tree:GameFun
    {
        
        public Tree()
        {
            this.gameFunDateStruct = new WPFCheatUITemplate.Other.GameFunDateStruct()
            {
                ModuleName = "PlantsVsZombies.exe",
                ModuleOffsetAddress = 0x355E0C,

                IsSignatureCode = false,

                IntPtrOffset = new uint[] { 0x950, 0x11c },
                IsIntPtr = true,

                Vk = Keys.NumPad5,
                FsModifiers = HotKey.KeyModifiers.None,

                KeyDescription_SC = "数字键5",
                FunDescribe_SC = "设置智慧树高度",

                KeyDescription_TC = "數字鍵5",
                FunDescribe_TC = "設置智慧樹高度",

                KeyDescription_EN = "Number 5",
                FunDescribe_EN = "Tree height",

                IsTrigger = true,


                IsAcceptValue = true,
                SliderMinNum = 1,
                SliderMaxNum = 99999,
            };
           

            GameFunManger.Instance.RegisterGameFun(this);
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
