using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CheatUITemplt.MyGameFuns
{
    class NoCd:GameFun
    {
       
        public NoCd()
        {
            this.gameFunDateStruct = new WPFCheatUITemplate.Other.GameFunDateStruct()
            {
                ModuleName = "PlantsVsZombies.exe",
                ModuleOffsetAddress = 0x9ce02,
                IsSignatureCode = false,
                IsIntPtr = false,

                Vk = Keys.NumPad6,
                FsModifiers = HotKey.KeyModifiers.None,

                KeyDescription_SC = "数字键6",
                FunDescribe_SC = "无冷却时间",

                KeyDescription_TC = "數字鍵6",
                FunDescribe_TC = "無冷卻時間",

                KeyDescription_EN = "Number 6",
                FunDescribe_EN = "No cool down time",

                IsTrigger = false
            };
           
            GameFunManger.Instance.RegisterGameFun(this);
        }

        public override void Awake()
        {

        }

        public override void DoFirstTime(double value)
        {
            CheatTools.WriteMemoryByte(gameFunDateStruct.GameDataAddress.Address, gameFunDateStruct.Handle, new byte[] { 0x74 });
        }

        public override void DoRunAgain(double value)
        {
            CheatTools.WriteMemoryByte(gameFunDateStruct.GameDataAddress.Address, gameFunDateStruct.Handle, new byte[] { 0x7e });
        }
        public override void Ending()
        {

        }
    }
}
