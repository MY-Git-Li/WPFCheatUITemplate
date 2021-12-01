using CheatUITemplt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using WPFCheatUITemplate.GameMode;

namespace WPFCheatUITemplate.Other.GameFuns
{
    static class AddressDataManager
    {

        static GameVersion.Version version= GameInformation.CurentVersion;

        static Dictionary<GameVersion.Version, Dictionary<string, GameData>> data_Dic = new Dictionary<GameVersion.Version, Dictionary<string, GameData>>();

        static Dictionary<string, GameDataAddress> curentGameDataAddress = new Dictionary<string, GameDataAddress>();

        public static void Init()
        {
            Task.Factory.StartNew(() => 
            {
                Type[] types = Assembly.GetExecutingAssembly().GetTypes();
                foreach (var type in types)
                {
                    if (type.IsInterface)
                        continue;
                    if (typeof(Interface.IAddressDatas).IsAssignableFrom(type))
                    {
                        if (!type.IsAbstract)
                        {
                            var obj = Activator.CreateInstance(type) as Interface.IAddressDatas;

                            obj.Init();
                        }
                    }

                }
                GetAllGameDataAddress();
            });
        }

        public static void GetAllGameDataAddress()
        {
            
            foreach (var item in data_Dic[version])
            {
                curentGameDataAddress.Add(item.Key,item.Value.GetDataAddress());
            }
        }

        public static void AddData(string id, GameVersion.Version v, GameData gameData)
        {

            if (!data_Dic.ContainsKey(v))
            {
                var dic = new Dictionary<string, GameData>();
                dic[id] = gameData;
                data_Dic[v] = dic;
            }else
            {
                data_Dic[v][id] = gameData;
            }


        }

        public static int GetAddress(string id)
        {
            int ret = 0;

            if (data_Dic.ContainsKey(version))
            {
                var dic = data_Dic[version];
                if (dic.ContainsKey(id))
                {
                    if (!curentGameDataAddress.ContainsKey(id))
                    {
                        var dataAddress = dic[id].GetDataAddress();
                        curentGameDataAddress.Add(id, dataAddress);
                        return dataAddress.Address;
                    }
                    ret = curentGameDataAddress[id].Address;
                    //ret = dic[id].GetDataAddress().Address;
                }
               
            }

            return ret;
        }


    }
}
