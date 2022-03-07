using WPFCheatUITemplate.Other.Extends;
using WPFCheatUITemplate.Other.GameFuns;
namespace WPFCheatUITemplate.DataSet
{
    class DataV1_1_0_1056 : AddressDatas
    {
        public override void Init()
        {
            AddData("sun", GameVersion.Version.V1_1_0_1056, new GameData()
            {
                ModuleName = "PlantsVsZombies.exe",
                ModuleOffsetAddress = 0x355E0C,

                IsSignatureCode = false,

                IntPtrOffset = new uint[] { 0x868, 0x5578 },
                IsIntPtr = true,
            });

            AddData("coin", GameVersion.Version.V1_1_0_1056, new GameData()
            {
                ModuleName = "PlantsVsZombies.exe",
                ModuleOffsetAddress = 0x355E0C,

                IsSignatureCode = false,

                IntPtrOffset = new uint[] { 0x950, 0x50 },
                IsIntPtr = true,
            });

            AddData("adventureLevel", GameVersion.Version.V1_1_0_1056, new GameData()
            {
                ModuleName = "PlantsVsZombies.exe",
                ModuleOffsetAddress = 0x355E0C,

                IsSignatureCode = false,

                IntPtrOffset = new uint[] { 0x950, 0x4c },
                IsIntPtr = true,
            });

            AddData("changeMode", GameVersion.Version.V1_1_0_1056, new GameData()
            {
                ModuleName = "PlantsVsZombies.exe",
                ModuleOffsetAddress = 0x355E0C,

                IsSignatureCode = false,

                IntPtrOffset = new uint[] { 0x91c },
                IsIntPtr = true,
            });

            AddData("tree", GameVersion.Version.V1_1_0_1056, new GameData()
            {
                ModuleName = "PlantsVsZombies.exe",
                ModuleOffsetAddress = 0x355E0C,

                IsSignatureCode = false,

                IntPtrOffset = new uint[] { 0x950, 0x11c },
                IsIntPtr = true,
            });

            AddData("noCd", GameVersion.Version.V1_1_0_1056, new GameData()
            {
                //FF 46 ?? 8B 46 ?? 3B 46 ?? 7E 16
                ModuleName = "PlantsVsZombies.exe",
                ModuleOffsetAddress = 0x9ce02,
                IsSignatureCode = false,
                IsIntPtr = false,
            });

            AddData("autoGet", GameVersion.Version.V1_1_0_1056, new GameData()
            {
                //E8 34 F5 FF FF EB 16
                ModuleName = "PlantsVsZombies.exe",
                ModuleOffsetAddress = 0x3CC72,

                IsSignatureCode = false,
                IsIntPtr = false,
            });

            AddData("arbitrarilyPlant", GameVersion.Version.V1_1_0_1056, new GameData()
            {
                ModuleName = "PlantsVsZombies.exe",

                IsSignatureCode = true,
                SignatureCodeOffset = 0xe,
                SignatureCode = "8B 54 24 0C 53 52 57",

                IsIntPtr = false,
            }, 
            new byte[] { 0xE9, 0x47, 0x09, 0x00, 0x00, 0x90 }, 
            new byte[] { 0x0f, 0x84, 0x46, 0x09, 0x00, 0x00 });

            AddData("allowBackground", GameVersion.Version.V1_1_0_1056, new GameData()
            {
                ModuleName = "PlantsVsZombies.exe",
                ModuleOffsetAddress = 0x5D040,
                IsSignatureCode = false,
                IsIntPtr = false,
            }, 
            new byte[] { 0xC2, 0x04, 0x00 },
            new byte[] { 0x55, 0x8B, 0xEC });

            AddData("Win_Call", GameVersion.Version.V1_1_0_1056, new GameData()
            {
                ModuleName = "PlantsVsZombies.exe",
                ModuleOffsetAddress = 0x18140,

                IsSignatureCode = false,
                IsIntPtr = false,
            });

            AddData("Plant_Call", GameVersion.Version.V1_1_0_1056, new GameData()
            {
                ModuleName = "PlantsVsZombies.exe",
                ModuleOffsetAddress = 0x18D70,

                IsSignatureCode = false,
                IsIntPtr = false,
            });

            AddData("fast_belt_1", GameVersion.Version.V1_1_0_1056, new GameData()
            {
                ModuleName = "PlantsVsZombies.exe",
                ModuleOffsetAddress = 0x2E58F,


                IsSignatureCode = false,
                IsIntPtr = false,
            },
           new byte[] { 0x80 },
           new byte[] { 0x8f });

            AddData("fast_belt_2", GameVersion.Version.V1_1_0_1056, new GameData()
            {
                ModuleName = "popcapgame1.exe",
                ModuleOffsetAddress = 0x9F6FE,


                IsSignatureCode = false,
                IsIntPtr = false,
            },
            new byte[] { 0x33 },
            new byte[] { 0x85 });


            AddData("Secondary_Offset", GameVersion.Version.V1_1_0_1056, new GameData()
            {
                ModuleName = "PlantsVsZombies.exe",
                ModuleOffsetAddress = 0x355E0C,

                IsSignatureCode = false,

                IntPtrOffset = new uint[] { 0x868 },
                IsIntPtr = true,
            });

            AddData("see_vase", GameVersion.Version.V1_1_0_1056, new GameData()
            {
                ModuleName = "PlantsVsZombies.exe",
                ModuleOffsetAddress = 0x59C1A,


                IsSignatureCode = false,
                IsIntPtr = false,
            },
            new byte[] { 0x66, 0xB8, 0x33, 0x00 },
            new byte[] { 0x85, 0xC0, 0x7E, 0x04 });

            AddData("GameRunSpeed", GameVersion.Version.V1_1_0_1056, new GameData()
            {
                ModuleName = "PlantsVsZombies.exe",
                ModuleOffsetAddress = 0x355E0C,

                IsSignatureCode = false,

                IntPtrOffset = new uint[] { 0x454 + 0x60 },
                IsIntPtr = true,
            });

            AddData("unlock_sun_limit", GameVersion.Version.V1_1_0_1056, new GameData()
            {
                ModuleName = "PlantsVsZombies.exe",
                ModuleOffsetAddress = 0x3C0C1,

                IsSignatureCode = false,
                IsIntPtr = false,
            },
            new byte[] { 0xEB },
            new byte[] { 0x7E });
        }
    }
    
}