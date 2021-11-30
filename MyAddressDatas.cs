using WPFCheatUITemplate.Other;
using WPFCheatUITemplate.Other.GameFuns;
using WPFCheatUITemplate.Other.Interface;

namespace WPFCheatUITemplate
{
    class MyAddressDatas : IAddressDatas
    {
        public void Init()
        {
            AddressDataManager.AddData("supershoot", GameVersion.Version.Default,new GameData()
            {
                ModuleName = "PlantsVsZombies.exe",
                ModuleOffsetAddress = 0x6DC21,

                IsSignatureCode = false,
                IsIntPtr = false,
            });

            AddressDataManager.AddData("supershoot2", GameVersion.Version.Default, new GameData()
            {
                ModuleName = "PlantsVsZombies.exe",
                ModuleOffsetAddress = 0x72EE4,

                IsSignatureCode = false,
                IsIntPtr = false,
            });
        }
    }

}
