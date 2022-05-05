using WPFCheatUITemplate.Other.GameFuns;
using WPFCheatUITemplate.Other.Extends;

namespace WPFCheatUITemplate.DataSet
{
    class DataV1_0_0_1051 : AddressDatas
    {
        public override void Init()
        {
            AddData("noCd", GameVersion.Version.V1_0_0_1051, new GameData()
            {
                ModuleName = "PlantsVsZombies.exe",
                ModuleOffsetAddress = 0x87296,
                IsSignatureCode = false,
                IsIntPtr = false,
            });

            AddData("sun", GameVersion.Version.V1_0_0_1051, new GameData()
            {
                ModuleName = "PlantsVsZombies.exe",
                ModuleOffsetAddress = 0x2A9EC0,

                IsSignatureCode = false,

                IntPtrOffset = new uint[] { 0x768, 0x5560 },
                IsIntPtr = true,
            });

            AddData("coin", GameVersion.Version.V1_0_0_1051, new GameData()
            {
                ModuleName = "PlantsVsZombies.exe",
                ModuleOffsetAddress = 0x2A9EC0,

                IsSignatureCode = false,

                IntPtrOffset = new uint[] { 0x82C, 0x28 },
                IsIntPtr = true,
            });

            AddData("adventureLevel", GameVersion.Version.V1_0_0_1051, new GameData()
            {
                ModuleName = "PlantsVsZombies.exe",
                ModuleOffsetAddress = 0x2A9EC0,

                IsSignatureCode = false,

                IntPtrOffset = new uint[] { 0x82C, 0x24 },
                IsIntPtr = true,
            });

            AddData("changeMode", GameVersion.Version.V1_0_0_1051, new GameData()
            {
                ModuleName = "PlantsVsZombies.exe",
                ModuleOffsetAddress = 0x2A9EC0,

                IsSignatureCode = false,

                IntPtrOffset = new uint[] { 0x554C },
                IsIntPtr = true,
            });

            AddData("tree", GameVersion.Version.V1_0_0_1051, new GameData()
            {
                ModuleName = "PlantsVsZombies.exe",
                ModuleOffsetAddress = 0x2A9EC0,

                IsSignatureCode = false,

                IntPtrOffset = new uint[] { 0x82C, 0xF4 },
                IsIntPtr = true,
            });

            AddData("autoGet", GameVersion.Version.V1_0_0_1051, new GameData()
            {
                //E8 34 F5 FF FF EB 16
                ModuleName = "PlantsVsZombies.exe",
                ModuleOffsetAddress = 0x3158F,

                IsSignatureCode = false,
                IsIntPtr = false,
            });

            AddData("arbitrarilyPlant", GameVersion.Version.V1_0_0_1051, new GameData()
            {
                ModuleName = "PlantsVsZombies.exe",
                ModuleOffsetAddress = 0xFE2F,

                IsSignatureCode = false,
                IsIntPtr = false,
            },
            new byte[] { 0xE9, 0x20, 0x09, 0x00, 0x00, 0x90 },
            new byte[] { 0x0f, 0x84, 0x1F, 0x09, 0x00, 0x00 });

            AddData("allowBackground", GameVersion.Version.V1_0_0_1051, new GameData()
            {
                ModuleName = "PlantsVsZombies.exe",
                ModuleOffsetAddress = 0x14EBA8,
                IsSignatureCode = false,
                IsIntPtr = false,
            },
            new byte[] { 112 },
            new byte[] { 116 });

            AddData("Win_Call", GameVersion.Version.V1_0_0_1051, new GameData()
            {
                ModuleName = "PlantsVsZombies.exe",
                ModuleOffsetAddress = 0xC3E0,

                IsSignatureCode = false,
                IsIntPtr = false,
            });

            AddData("Plant_Call", GameVersion.Version.V1_0_0_1051, new GameData()
            {
                ModuleName = "PlantsVsZombies.exe",
                ModuleOffsetAddress = 0xD120,

                IsSignatureCode = false,
                IsIntPtr = false,
            });


            AddData("Secondary_Offset", GameVersion.Version.V1_0_0_1051, new GameData()
            {
                ModuleName = "PlantsVsZombies.exe",
                ModuleOffsetAddress = 0x2A9EC0,

                IsSignatureCode = false,

                IntPtrOffset = new uint[] { 0x768 },
                IsIntPtr = true,
            });

            AddData("fast_belt_1", GameVersion.Version.V1_0_0_1051, new GameData()
            {
                ModuleName = "PlantsVsZombies.exe",
                ModuleOffsetAddress = 0x22D20,


                IsSignatureCode = false,
                IsIntPtr = false,
            },
            new byte[] { 0x80 },
            new byte[] { 0x8f });

            AddData("fast_belt_2", GameVersion.Version.V1_0_0_1051, new GameData()
            {
                ModuleName = "PlantsVsZombies.exe",
                ModuleOffsetAddress = 0x89CA1,


                IsSignatureCode = false,
                IsIntPtr = false,
            },
            new byte[] { 0x33 },
            new byte[] { 0x85 });

            AddData("see_vase", GameVersion.Version.V1_0_0_1051, new GameData()
            {
                ModuleName = "PlantsVsZombies.exe",
                ModuleOffsetAddress = 0x4E5CC,


                IsSignatureCode = false,
                IsIntPtr = false,
            },
            new byte[] { 0x66, 0xB8, 0x33, 0x00 },
            new byte[] { 0x85, 0xC0, 0x7E, 0x06 });

            AddData("GameRunSpeed", GameVersion.Version.V1_0_0_1051, new GameData()
            {
                ModuleName = "PlantsVsZombies.exe",
                ModuleOffsetAddress = 0x2A9EC0,

                IsSignatureCode = false,

                IntPtrOffset = new uint[] { 0x454 },
                IsIntPtr = true,
            });

            AddData("unlock_sun_limit", GameVersion.Version.V1_0_0_1051, new GameData()
            {
                ModuleName = "PlantsVsZombies.exe",
                ModuleOffsetAddress = 0x30A23,

                IsSignatureCode = false,
                IsIntPtr = false,
            },
            new byte[]{ 0xEB}, 
            new byte[] { 0x7E});
        }
    }
}
