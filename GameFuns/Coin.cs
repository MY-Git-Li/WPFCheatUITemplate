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
            gameFunDateStruct = new Other.GameFunDateStruct();
            gameFunDateStruct.uIData = new Other.UIData()
            {
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
            gameFunDateStruct.refHotKey = new Other.RefHotKey()
            {
                Vk = Keys.NumPad2,
                FsModifiers = HotKey.KeyModifiers.None,
            };
            gameFunDateStruct.AddGameDate(GameVersion.Version.Null, new Other.GameDate()
            {
                ModuleName = "PlantsVsZombies.exe",
                ModuleOffsetAddress = 0x355E0C,

                IsSignatureCode = false,

                IntPtrOffset = new uint[] { 0x950, 0x50 },
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
