using CheatUITemplt;
using System.Windows.Forms;

namespace WPFCheatUITemplate.GameFuns
{
    class AllowBackground:GameFun
    {
       
        public AllowBackground()
        {
            gameFunDataAndUIStruct = Other.GameFuns.UIManager.GetCheckButtonDateStruct("允许后台运行", "Running in background", false);
        }

        public override void Awake()
        {

        }

        public override void DoFirstTime(double value)
        {
            memory.WriteMemory<byte>(Other.GameFuns.AddressDataManager.GetAddress("allowBackground"), new byte[] { 0xC2, 0x04, 0x00 });
      
        }

        public override void DoRunAgain(double value)
        {
            memory.WriteMemory<byte>(Other.GameFuns.AddressDataManager.GetAddress("allowBackground"), new byte[] { 0x55, 0x8B, 0xEC });
        }
        public override void Ending()
        {

        }
    }
}

