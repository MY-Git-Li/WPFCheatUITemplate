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

            this.gameFunDateStruct = new WPFCheatUITemplate.Other.GameFunDateStruct()
            {
                ModuleName = "PlantsVsZombies.exe",
                ModuleOffsetAddress = 0x355E0C,

                IsSignatureCode = false,

                IntPtrOffset = new uint[] { 0x950, 0x50 },
                IsIntPtr = true,

                Vk = Keys.NumPad2,
                FsModifiers = HotKey.KeyModifiers.None,

                KeyDescription_SC = "数字键2",
                FunDescribe_SC = "设置硬币",

                KeyDescription_TC = "數字鍵2",
                FunDescribe_TC = "設置硬幣",

                KeyDescription_EN = "Number 2",
                FunDescribe_EN = "Coin number",

                IsTrigger = true,


                IsAcceptValue = true,
                SliderMinNum = 1,
                SliderMaxNum = 9999,
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
