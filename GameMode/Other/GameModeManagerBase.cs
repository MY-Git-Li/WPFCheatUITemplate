using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFCheatUITemplate.Other;
using WPFCheatUITemplate.Other.GameFuns;
using WPFCheatUITemplate.Other.Interface;

namespace WPFCheatUITemplate.GameMode
{
    abstract class GameModeManagerBase : IAddressDatas
    {
        abstract public void Init();

        static public void AddData(string name, GameVersion.Version v, GameData offset)
        {
            AddressDataManager.AddData(name, v, offset);
        }
        static public void AddData(string name, GameVersion.Version v, int offset)
        {
            AddressDataManager.AddData(name, v, offset);
        }

        static public IntPtr GetAddress(string id)
        {
            return AddressDataManager.GetAddress(id);
        }

        static public int GetOffset(string id)
        {
            return AddressDataManager.GetOffSet(id);
        }

    }
}
