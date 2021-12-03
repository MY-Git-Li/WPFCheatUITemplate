using CheatUITemplt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using WPFCheatUITemplate.GameMode;
using WPFCheatUITemplate.Other.Exceptions;

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

        static void GetAllGameDataAddress()
        {
            foreach (var item in data_Dic[GameVersion.Version.Default])
            {
                curentGameDataAddress.Add(item.Key, item.Value.GetDataAddress());
            }


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
                Dictionary<string, GameData> dic = data_Dic[version];

                ret = HandleGetAddress(version, dic,id);

            }
            else if (data_Dic.ContainsKey(GameVersion.Version.Default))
            {
                var dic = data_Dic[GameVersion.Version.Default];

                ret = HandleGetAddress(GameVersion.Version.Default, dic, id);

            }
                  
            return ret;
        }


        static int HandleGetAddress(GameVersion.Version v, Dictionary<string, GameData> dic,string id)
        {
            int ret = 0;
            if (dic.ContainsKey(id))
            {
                if (!curentGameDataAddress.ContainsKey(id))
                {
                    var dataAddress = dic[id].GetDataAddress();

                    if (dataAddress == null)
                    {
                        throw new ZeroAddressException("地址错误！", data_Dic[v][id]);
                    }

                    curentGameDataAddress.Add(id, dataAddress);
                    ret = dataAddress.Address;
                }
                else
                {
                    if (curentGameDataAddress[id] == null)
                    {
                        throw new ZeroAddressException("地址错误！", data_Dic[v][id]);
                    }

                    ret = curentGameDataAddress[id].Address;
                }
            }

            return ret;
        }
        
    }
}
