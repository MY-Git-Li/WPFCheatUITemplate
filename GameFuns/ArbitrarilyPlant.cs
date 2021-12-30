using CheatUITemplt;
using System.Windows.Forms;

namespace WPFCheatUITemplate.GameFuns
{
    class ArbitrarilyPlant : GameFun
    {
       
        public ArbitrarilyPlant()
        {
            gameFunDataAndUIStruct = Other.GameFuns.UIManager.GetCheckButtonDateStruct("随意种植", "Planting will", false);
        }
        public override void Awake()
        {

        }

        public override void DoFirstTime(double value)
        {
            if (GameMode.GameInformation.CurentVersion == GameVersion.Version.Default)
                memory.WriteMemory<byte>(Other.GameFuns.AddressDataManager.GetAddress("arbitrarilyPlant"), new byte[] { 0xE9, 0x47, 0x09, 0x00, 0x00, 0x90 });
            
            if (GameMode.GameInformation.CurentVersion == GameVersion.Version.V1_0_0_1051)
                memory.WriteMemory<byte>(Other.GameFuns.AddressDataManager.GetAddress("arbitrarilyPlant"), new byte[] { 0xE9, 0x20, 0x09, 0x00, 0x00, 0x90 });

        }

        public override void DoRunAgain(double value)
        {
            if (GameMode.GameInformation.CurentVersion == GameVersion.Version.Default)
                memory.WriteMemory<byte>(Other.GameFuns.AddressDataManager.GetAddress("arbitrarilyPlant"), new byte[] { 0x0f, 0x84, 0x46, 0x09, 0x00, 0x00 });

            if (GameMode.GameInformation.CurentVersion == GameVersion.Version.V1_0_0_1051)
                memory.WriteMemory<byte>(Other.GameFuns.AddressDataManager.GetAddress("arbitrarilyPlant"), new byte[] { 0x0f, 0x84, 0x1F, 0x09, 0x00, 0x00 });

        }
        public override void Ending()
        {

        }
    }
}
