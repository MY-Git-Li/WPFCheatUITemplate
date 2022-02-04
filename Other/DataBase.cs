using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFCheatUITemplate.Other
{
    abstract class DataBase
    {
        static public void AddData(string id, WPFCheatUITemplate.GameVersion.Version v, int offset)
        {
            GameFuns.AddressDataManager.AddData(id, v, offset);
        }

        static public void AddData(string id, WPFCheatUITemplate.GameVersion.Version v, GameData gameData, byte[] modifyData, byte[] orcData)
        {
            GameFuns.AddressDataManager.AddData(id, v, gameData, modifyData, orcData);
        }


        static public void AddData(string id, WPFCheatUITemplate.GameVersion.Version v, GameData gameData)
        {
            GameFuns.AddressDataManager.AddData(id, v, gameData);
        }

        static public void GetModifyData(string id)
        {
            GameFuns.AddressDataManager.GetModifyData(id);
        }

        static public int GetOffSet(string id)
        {
          return GameFuns.AddressDataManager.GetOffSet(id);
        }

        static public void GetOrcData(string id)
        {
            GameFuns.AddressDataManager.GetOrcData(id);
        }

        static public IntPtr GetAddress(string id)
        {
          return GameFuns.AddressDataManager.GetAddress(id);
        }
    }
}
