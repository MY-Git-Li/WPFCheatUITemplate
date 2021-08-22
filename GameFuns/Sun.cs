using CheatUITemplt;
using System.Windows.Forms;

namespace WPFCheatUITemplate.GameFuns
{
    class Sun : GameFun
    {
        public Sun()
        {
            this.gameFunDateStruct = new WPFCheatUITemplate.Other.GameFunDateStruct()
            {
                ModuleName = "PlantsVsZombies.exe",
                ModuleOffsetAddress = 0x355E0C,

                IsSignatureCode = false,

                IntPtrOffset = new uint[] { 0x868, 0x5578 },
                IsIntPtr = true,

                Vk = Keys.NumPad1,
                FsModifiers = HotKey.KeyModifiers.None,

                KeyDescription_SC = "数字键1",
                FunDescribe_SC = "设置阳光",

                KeyDescription_TC = "數字鍵1",
                FunDescribe_TC = "設置陽光",

                KeyDescription_EN = "Number 1",
                FunDescribe_EN = "Sun number",

                IsTrigger = true,


                IsAcceptValue = true,
                SliderMinNum = 1,
                SliderMaxNum = 9999
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
