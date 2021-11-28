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

        static void Init()
        {
            Type[] types = Assembly.GetExecutingAssembly().GetTypes();
            foreach (var type in types)
            {
                if (typeof(AddressDatas).IsAssignableFrom(type))
                {
                    MethodInfo init = type.GetMethod("Init", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public | BindingFlags.Static);

                    if (init != null)
                    {
                        if (!type.IsAbstract)
                        {
                            var obj = Activator.CreateInstance(type) as AddressDatas;

                            init.Invoke(obj, null);
                        }

                    }
                }

            }
        }

        public static void AddData(GameVersion.Version v, string id, GameData gameData)
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
                    ret = dic[id].GetDataAddress(GameInformation.Pid).Address;
                }
               
            }

            return ret;
        }


    }
}
