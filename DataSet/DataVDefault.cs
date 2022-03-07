using WPFCheatUITemplate.Other.GameFuns;
using WPFCheatUITemplate.Other.Extends;

namespace WPFCheatUITemplate.DataSet
{
    internal class DataVDefault : AddressDatas
    {
        public override void Init()
        {
            AddData("supershoot", GameVersion.Version.Default, new GameData()
            {
                ModuleName = "PlantsVsZombies.exe",

                IsSignatureCode = true,
                SignatureCodeOffset = 0,
                SignatureCode = "8b 4e 5c 2b c8 8b 46",

                IsIntPtr = false,
            },
            new byte[] { 0xB9, 0x22, 0x00, 0x00, 0x00 },
            new byte[] { 0x8B, 0x4E, 0x5C, 0x2B, 0xC8 });

            AddData("supershoot2", GameVersion.Version.Default, new GameData()
            {
                ModuleName = "PlantsVsZombies.exe",

                IsSignatureCode = true,
                SignatureCodeOffset = 0x7,
                SignatureCode = "83 bf 90 00 00 00 01 0f",

                IsIntPtr = false,
            },
            new byte[] { 0x0F, 0x84 },
            new byte[] { 0x0F, 0x85 });

        }
    }
}
