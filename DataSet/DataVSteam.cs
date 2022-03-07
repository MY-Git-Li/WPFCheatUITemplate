using WPFCheatUITemplate.Other.GameFuns;
using WPFCheatUITemplate.Other.Extends;

namespace WPFCheatUITemplate.DataSet
{
    class DataVSteam : AddressDatas
    {
        public override void Init()
        {
            AddData("supershoot", GameVersion.Version.Steam, new GameData()
            {
                ModuleName = "popcapgame1.exe",

                IsSignatureCode = true,
                SignatureCodeOffset = 0,
                SignatureCode = "8b 4e 5c 2b c8 8b 46",

                IsIntPtr = false,
            },
            new byte[] { 0xB9, 0x22, 0x00, 0x00, 0x00 },
            new byte[] { 0x8B, 0x4E, 0x5C, 0x2B, 0xC8 });

            AddData("supershoot2", GameVersion.Version.Steam, new GameData()
            {
                ModuleName = "popcapgame1.exe",

                IsSignatureCode = true,
                SignatureCodeOffset = 0x7,
                SignatureCode = "83 bf 90 00 00 00 01 0f",

                IsIntPtr = false,
            },
            new byte[] { 0x0F, 0x84 },
            new byte[] { 0x0F, 0x85 });

            AddData("noCd", GameVersion.Version.Steam, new GameData()
            {
                ModuleName = "popcapgame1.exe",
                ModuleOffsetAddress = 0x958C5,
                IsSignatureCode = false,
                IsIntPtr = false,
            });

            AddData("sun", GameVersion.Version.Steam, new GameData()
            {
                ModuleName = "popcapgame1.exe",
                ModuleOffsetAddress = 0x331C50,

                IsSignatureCode = false,

                IntPtrOffset = new uint[] { 0x868, 0x5578 },
                IsIntPtr = true,
            });

            AddData("coin", GameVersion.Version.Steam, new GameData()
            {
                ModuleName = "popcapgame1.exe",
                ModuleOffsetAddress = 0x331C50,

                IsSignatureCode = false,

                IntPtrOffset = new uint[] { 0x82C + 0x120, 0x28 + 0x28 + 4 },
                IsIntPtr = true,
            });

            AddData("adventureLevel", GameVersion.Version.Steam, new GameData()
            {
                ModuleName = "popcapgame1.exe",
                ModuleOffsetAddress = 0x331C50,

                IsSignatureCode = false,

                IntPtrOffset = new uint[] { 0x82C + 0x120, 0x24 + 0x28 + 4 },
                IsIntPtr = true,
            });

            AddData("changeMode", GameVersion.Version.Steam, new GameData()
            {
                ModuleName = "popcapgame1.exe",
                ModuleOffsetAddress = 0x331C50,

                IsSignatureCode = false,

                IntPtrOffset = new uint[] { 0x554C + 0x18 },
                IsIntPtr = true,
            });

            AddData("tree", GameVersion.Version.Steam, new GameData()
            {
                ModuleName = "popcapgame1.exe",
                ModuleOffsetAddress = 0x331C50,

                IsSignatureCode = false,

                IntPtrOffset = new uint[] { 0x82C + 0x120, 0xF4 + 0x28 + 4 },
                IsIntPtr = true,
            });

            AddData("autoGet", GameVersion.Version.Steam, new GameData()
            {
                //E8 34 F5 FF FF EB 16
                ModuleName = "popcapgame1.exe",
                ModuleOffsetAddress = 0x352F2,

                IsSignatureCode = false,
                IsIntPtr = false,
            });

            AddData("arbitrarilyPlant", GameVersion.Version.Steam, new GameData()
            {
                ModuleName = "popcapgame1.exe",
                ModuleOffsetAddress = 0x13350,

                IsSignatureCode = false,
                IsIntPtr = false,
            },
            new byte[] { 0x81},
            new byte[] { 0x84});

            AddData("allowBackground", GameVersion.Version.Steam, new GameData()
            {
                ModuleName = "popcapgame1.exe",
                ModuleOffsetAddress = 0x1D87C9,
                IsSignatureCode = false,
                IsIntPtr = false,
            },
            new byte[] { 0xEB,0x00 },
            new byte[] { 0x74,0x40 });

            AddData("Win_Call", GameVersion.Version.Steam, new GameData()
            {
                ModuleName = "popcapgame1.exe",
                ModuleOffsetAddress = 0xF860,

                IsSignatureCode = false,
                IsIntPtr = false,
            });

            AddData("Plant_Call", GameVersion.Version.Steam, new GameData()
            {
                ModuleName = "popcapgame1.exe",
                ModuleOffsetAddress = 0x105A0,

                IsSignatureCode = false,
                IsIntPtr = false,
            });

            AddData("fast_belt_1", GameVersion.Version.Steam, new GameData()
            {
                ModuleName = "popcapgame1.exe",
                ModuleOffsetAddress = 0x2684F,


                IsSignatureCode = false,
                IsIntPtr = false,
            },
            new byte[] { 0x80 },
            new byte[] { 0x8f });

            AddData("fast_belt_2", GameVersion.Version.Steam, new GameData()
            {
                ModuleName = "popcapgame1.exe",
                ModuleOffsetAddress = 0x9831E,


                IsSignatureCode = false,
                IsIntPtr = false,
            },
            new byte[] { 0x33 },
            new byte[] { 0x85 });

            AddData("Secondary_Offset", GameVersion.Version.Steam, new GameData()
            {
                ModuleName = "popcapgame1.exe",
                ModuleOffsetAddress = 0x331C50,

                IsSignatureCode = false,

                IntPtrOffset = new uint[] { 0x868 },
                IsIntPtr = true,
            });

            AddData("see_vase", GameVersion.Version.Steam, new GameData()
            {
                ModuleName = "popcapgame1.exe",
                ModuleOffsetAddress = 0x531CA,


                IsSignatureCode = false,
                IsIntPtr = false,
            },
            new byte[] { 0x66, 0xB8, 0x33, 0x00 },
            new byte[] { 0x85, 0xC0, 0x7E, 0x04 });

            AddData("GameRunSpeed", GameVersion.Version.Steam, new GameData()
            {
                ModuleName = "popcapgame1.exe",
                ModuleOffsetAddress = 0x331C50,

                IsSignatureCode = false,

                IntPtrOffset = new uint[] { 0x454 + 0x60 },
                IsIntPtr = true,
            });

            AddData("unlock_sun_limit", GameVersion.Version.Steam, new GameData()
            {
                ModuleName = "popcapgame1.exe",
                ModuleOffsetAddress = 0x1F4E5,

                IsSignatureCode = false,
                IsIntPtr = false,
            },
            new byte[] { 0xEB },
            new byte[] { 0x7E });
        }
    }
}
