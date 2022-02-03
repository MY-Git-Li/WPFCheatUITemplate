using WPFCheatUITemplate.Other;
using WPFCheatUITemplate.Other.GameFuns;
using WPFCheatUITemplate.Other.Interface;

namespace WPFCheatUITemplate
{
    class MyAddressDatas : IAddressDatas
    {
        public void Init()
        {
            AddressDataManager.AddData("supershoot", GameVersion.Version.Default, new GameData()
            {
                ModuleName = "PlantsVsZombies.exe",
                ModuleOffsetAddress = 0x6DC21,

                IsSignatureCode = false,
                IsIntPtr = false,
            },
            new byte[] { 0xB9, 0x22, 0x00, 0x00, 0x00 },
            new byte[] { 0x8B, 0x4E, 0x5C, 0x2B, 0xC8 });

            AddressDataManager.AddData("supershoot2", GameVersion.Version.Default, new GameData()
            {
                ModuleName = "PlantsVsZombies.exe",
                ModuleOffsetAddress = 0x72EE4,

                IsSignatureCode = false,
                IsIntPtr = false,
            },
            new byte[] { 0x0F, 0x84 },
            new byte[] { 0x0F, 0x85 });

            AddressDataManager.AddData("sun", GameVersion.Version.Default, new Other.GameData()
            {
                ModuleName = "PlantsVsZombies.exe",
                ModuleOffsetAddress = 0x355E0C,

                IsSignatureCode = false,

                IntPtrOffset = new uint[] { 0x868, 0x5578 },
                IsIntPtr = true,
            });

            AddressDataManager.AddData("coin", GameVersion.Version.Default, new Other.GameData()
            {
                ModuleName = "PlantsVsZombies.exe",
                ModuleOffsetAddress = 0x355E0C,

                IsSignatureCode = false,

                IntPtrOffset = new uint[] { 0x950, 0x50 },
                IsIntPtr = true,
            });

            AddressDataManager.AddData("adventureLevel", GameVersion.Version.Default, new Other.GameData()
            {
                ModuleName = "PlantsVsZombies.exe",
                ModuleOffsetAddress = 0x355E0C,

                IsSignatureCode = false,

                IntPtrOffset = new uint[] { 0x950, 0x4c },
                IsIntPtr = true,
            });

            AddressDataManager.AddData("changeMode", GameVersion.Version.Default, new Other.GameData()
            {
                ModuleName = "PlantsVsZombies.exe",
                ModuleOffsetAddress = 0x355E0C,

                IsSignatureCode = false,

                IntPtrOffset = new uint[] { 0x91c },
                IsIntPtr = true,
            });

            AddressDataManager.AddData("tree", GameVersion.Version.Default, new Other.GameData()
            {
                ModuleName = "PlantsVsZombies.exe",
                ModuleOffsetAddress = 0x355E0C,

                IsSignatureCode = false,

                IntPtrOffset = new uint[] { 0x950, 0x11c },
                IsIntPtr = true,
            });

            AddressDataManager.AddData("noCd", GameVersion.Version.Default, new Other.GameData()
            {
                //FF 46 ?? 8B 46 ?? 3B 46 ?? 7E 16
                ModuleName = "PlantsVsZombies.exe",
                ModuleOffsetAddress = 0x9ce02,
                IsSignatureCode = false,
                IsIntPtr = false,
            });

            AddressDataManager.AddData("autoGet", GameVersion.Version.Default, new Other.GameData()
            {
                //E8 34 F5 FF FF EB 16
                ModuleName = "PlantsVsZombies.exe",
                ModuleOffsetAddress = 0x3CC72,

                IsSignatureCode = false,
                IsIntPtr = false,
            });

            AddressDataManager.AddData("arbitrarilyPlant", GameVersion.Version.Default, new Other.GameData()
            {
                ModuleName = "PlantsVsZombies.exe",

                IsSignatureCode = true,
                SignatureCodeOffset = 0xe,
                SignatureCode = "8B 54 24 0C 53 52 57",

                IsIntPtr = false,
            });

            AddressDataManager.AddData("allowBackground", GameVersion.Version.Default, new Other.GameData()
            {
                ModuleName = "PlantsVsZombies.exe",
                ModuleOffsetAddress = 0x5D040,
                IsSignatureCode = false,
                IsIntPtr = false,
            }, 
            new byte[] { 0xC2, 0x04, 0x00 },
            new byte[] { 0x55, 0x8B, 0xEC });

            AddressDataManager.AddData("Win_Call", GameVersion.Version.Default, new GameData()
            {
                ModuleName = "PlantsVsZombies.exe",
                ModuleOffsetAddress = 0x18140,

                IsSignatureCode = false,
                IsIntPtr = false,
            });

            AddressDataManager.AddData("Win_Call_ECX", GameVersion.Version.Default, new GameData()
            {
                ModuleName = "PlantsVsZombies.exe",
                ModuleOffsetAddress = 0x355E0C,

                IsSignatureCode = false,

                IntPtrOffset = new uint[] { 0x868 },
                IsIntPtr = true,
            });
        }
    }
    class MyAddressDatas_1_0_0 : IAddressDatas
    {
        public void Init()
        {
            AddressDataManager.AddData("noCd", GameVersion.Version.V1_0_0_1051, new Other.GameData()
            {
                ModuleName = "PlantsVsZombies.exe",
                ModuleOffsetAddress = 0x87296,
                IsSignatureCode = false,
                IsIntPtr = false,
            });

            AddressDataManager.AddData("sun", GameVersion.Version.V1_0_0_1051, new Other.GameData()
            {
                ModuleName = "PlantsVsZombies.exe",
                ModuleOffsetAddress = 0x2A9EC0,

                IsSignatureCode = false,

                IntPtrOffset = new uint[] { 0x768, 0x5560 },
                IsIntPtr = true,
            });

            AddressDataManager.AddData("coin", GameVersion.Version.V1_0_0_1051, new Other.GameData()
            {
                ModuleName = "PlantsVsZombies.exe",
                ModuleOffsetAddress = 0x2A9EC0,

                IsSignatureCode = false,

                IntPtrOffset = new uint[] { 0x82C, 0x28 },
                IsIntPtr = true,
            });

            AddressDataManager.AddData("adventureLevel", GameVersion.Version.V1_0_0_1051, new Other.GameData()
            {
                ModuleName = "PlantsVsZombies.exe",
                ModuleOffsetAddress = 0x2A9EC0,

                IsSignatureCode = false,

                IntPtrOffset = new uint[] { 0x82C, 0x24 },
                IsIntPtr = true,
            });

            AddressDataManager.AddData("changeMode", GameVersion.Version.V1_0_0_1051, new Other.GameData()
            {
                ModuleName = "PlantsVsZombies.exe",
                ModuleOffsetAddress = 0x2A9EC0,

                IsSignatureCode = false,

                IntPtrOffset = new uint[] { 0x554C },
                IsIntPtr = true,
            });

            AddressDataManager.AddData("tree", GameVersion.Version.V1_0_0_1051, new Other.GameData()
            {
                ModuleName = "PlantsVsZombies.exe",
                ModuleOffsetAddress = 0x2A9EC0,

                IsSignatureCode = false,

                IntPtrOffset = new uint[] { 0x824, 0xF4 },
                IsIntPtr = true,
            });

            AddressDataManager.AddData("autoGet", GameVersion.Version.V1_0_0_1051, new Other.GameData()
            {
                //E8 34 F5 FF FF EB 16
                ModuleName = "PlantsVsZombies.exe",
                ModuleOffsetAddress = 0x3158F,

                IsSignatureCode = false,
                IsIntPtr = false,
            });

            AddressDataManager.AddData("arbitrarilyPlant", GameVersion.Version.V1_0_0_1051, new Other.GameData()
            {
                ModuleName = "PlantsVsZombies.exe",
                ModuleOffsetAddress = 0xFE2F,

                IsSignatureCode = false,
                IsIntPtr = false,
            });

            AddressDataManager.AddData("allowBackground", GameVersion.Version.V1_0_0_1051, new Other.GameData()
            {
                ModuleName = "PlantsVsZombies.exe",
                ModuleOffsetAddress = 0x14EBA8,
                IsSignatureCode = false,
                IsIntPtr = false,
            },
            new byte[] {112},
            new byte[] {116});

            AddressDataManager.AddData("Win_Call", GameVersion.Version.V1_0_0_1051, new GameData()
            {
                ModuleName = "PlantsVsZombies.exe",
                ModuleOffsetAddress = 0xC3E0,

                IsSignatureCode = false,
                IsIntPtr = false,
            });

            AddressDataManager.AddData("Win_Call_ECX", GameVersion.Version.V1_0_0_1051, new GameData()
            {
                ModuleName = "PlantsVsZombies.exe",
                ModuleOffsetAddress = 0x2A9EC0,

                IsSignatureCode = false,

                IntPtrOffset = new uint[] { 0x768 },
                IsIntPtr = true,
            });

        }
    }

}