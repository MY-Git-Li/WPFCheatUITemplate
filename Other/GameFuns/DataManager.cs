using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using WPFCheatUITemplate.GameMode;

namespace WPFCheatUITemplate.Other.GameFuns
{
    static class DataManager
    {

        static GameVersion.Version version;

        static Dictionary<GameVersion.Version, Dictionary<string ,GameData>> data_Dic = new Dictionary<GameVersion.Version, Dictionary<string, GameData>>();

        static void Init()
        {
            Type[] types = Assembly.GetExecutingAssembly().GetTypes();
            foreach (var type in types)
            {
                if (typeof(Datas).IsAssignableFrom(type))
                {
                    MethodInfo init = type.GetMethod("Init", BindingFlags.NonPublic | BindingFlags.Instance|BindingFlags.Public|BindingFlags.Static);

                    if (init !=null)
                    {
                        if (!type.IsAbstract)
                        {
                            var obj = Activator.CreateInstance(type) as Datas;

                            init.Invoke(obj, null);
                        }
                       
                    }
                }

            }
        }

        public static void GetVersion(GameVersion.Version v)
        {
            version = v;
        }

        public static void AddData(GameVersion.Version v, string id, GameData gameData)
        {
            data_Dic[v][id] = gameData;
        }

        public static int GetAddress(string id)
        {
            return data_Dic[version][id].GetDataAddress(GameInformation.Pid).Address;
        }


    }
}
