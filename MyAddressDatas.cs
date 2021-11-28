using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFCheatUITemplate.Other.GameFuns;

namespace WPFCheatUITemplate
{
    class MyAddressDatas : AddressDatas
    {
        public override void Init()
        {
            AddressDataManager.AddData(GameVersion.Version.Default, "sun",new Other.GameData()
            {
                ModuleName = "PlantsVsZombies.exe",
                ModuleOffsetAddress = 0x355E0C,

            });
        }
    }
}
