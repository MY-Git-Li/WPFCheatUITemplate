using WPFCheatUITemplate.Other;
using WPFCheatUITemplate.Other.GameFuns;
using WPFCheatUITemplate.Other.Interface;

namespace WPFCheatUITemplate
{
    class MyAddressDatas : IAddressDatas
    {
        public void Init()
        {
            AddressDataManager.AddData(GameVersion.Version.Default,"supershoot" ,new GameData()
            {
                ModuleName = "PlantsVsZombies.exe",
                ModuleOffsetAddress = 0x6DC21,

                IsSignatureCode = false,
                IsIntPtr = false,
            });

            AddressDataManager.AddData(GameVersion.Version.Default, "supershoot2", new GameData()
            {
                ModuleName = "PlantsVsZombies.exe",
                ModuleOffsetAddress = 0x72EE4,

                IsSignatureCode = false,
                IsIntPtr = false,
            });
        }
    }

}
