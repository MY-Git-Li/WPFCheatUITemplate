using CheatUITemplt;
using System.Windows.Forms;

namespace WPFCheatUITemplate.GameFuns
{
    class NoCd:GameFun
    {
       
        public NoCd()
        {
            gameFunDataAndUIStruct = Other.GameFuns.UIManager.GetCheckButtonDateStruct("无冷却时间", "No cool down time", false);
        }

        public override void Awake()
        {

        }

        public override void DoFirstTime(double value)
        {
            memory.WriteMemory<byte>(Other.GameFuns.AddressDataManager.GetAddress("noCd"), new byte[] { 0x74 });
        }

        public override void DoRunAgain(double value)
        {
            memory.WriteMemory<byte>(Other.GameFuns.AddressDataManager.GetAddress("noCd"), new byte[] { 0x7e });
        }
        public override void Ending()
        {

        }
    }
}
