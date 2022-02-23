using CheatUITemplt;
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
            AppGameFunManager.Instance.AddressDataMg.AddData(id, v, offset);
        }

        static public void AddData(string id, WPFCheatUITemplate.GameVersion.Version v, GameData gameData, byte[] modifyData, byte[] orcData)
        {
            AppGameFunManager.Instance.AddressDataMg.AddData(id, v, gameData, modifyData, orcData);
        }


        static public void AddData(string id, WPFCheatUITemplate.GameVersion.Version v, GameData gameData)
        {
            AppGameFunManager.Instance.AddressDataMg.AddData(id, v, gameData);
        }

        static public void GetModifyData(string id)
        {
            AppGameFunManager.Instance.AddressDataMg.GetModifyData(id);
        }

        static public int GetOffSet(string id)
        {
          return AppGameFunManager.Instance.AddressDataMg.GetOffSet(id);
        }

        static public void GetOrcData(string id)
        {
            AppGameFunManager.Instance.AddressDataMg.GetOrcData(id);
        }

        static public IntPtr GetAddress(string id)
        {
          return AppGameFunManager.Instance.AddressDataMg.GetAddress(id);
        }
    }
}
