using CheatUITemplt;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using WPFCheatUITemplate.GameMode;
using WPFCheatUITemplate.Other.Exceptions;

namespace WPFCheatUITemplate.Other.GameFuns
{
    static class AddressDataManager
    {
       
        static Dictionary<GameVersion.Version, Dictionary<string, GameData>> data_Dic = new Dictionary<GameVersion.Version, Dictionary<string, GameData>>();

        static ConcurrentDictionary<string, GameDataAddress> curentGameDataAddress = new ConcurrentDictionary<string, GameDataAddress>();

        static Dictionary<GameVersion.Version, Dictionary<string, int>> data_Offset = new Dictionary<GameVersion.Version, Dictionary<string, int>>();

        static bool isAddDataInitComplete = false;

        struct ChangeData
        {
            public byte[] modifyData;
            public byte[] orcData;
        }

        static Dictionary<GameVersion.Version, Dictionary<string, ChangeData>> DataSet = new Dictionary<GameVersion.Version, Dictionary<string, ChangeData>>();


        public static void Init()
        {
            isAddDataInitComplete = false;

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

                isAddDataInitComplete = true;
            });
        }

        public static void DataClear()
        {
            curentGameDataAddress.Clear();
        }

        public static void DataInit()
        {
            Task.Factory.StartNew(() => { while (!isAddDataInitComplete) ; GetAllGameDataAddress(); });
        }


        static void GetAllGameDataAddress()
        {
            foreach (var item in data_Dic[GameVersion.Version.Default])
            {
                if (!curentGameDataAddress.ContainsKey(item.Key))
                {
                    curentGameDataAddress.TryAdd(item.Key, item.Value.GetDataAddress());
                }

            }


            foreach (var item in data_Dic[GameInformation.CurentVersion])
            {
                if (curentGameDataAddress.ContainsKey(item.Key))
                {
                    curentGameDataAddress[item.Key] = item.Value.GetDataAddress();
                }
                else
                {
                    curentGameDataAddress.TryAdd(item.Key, item.Value.GetDataAddress());
                }

            }
        }

        public static void AddData(string id, GameVersion.Version v, GameData gameData)
        {

            if (!data_Dic.ContainsKey(v))
            {
                var dic = new Dictionary<string, GameData>();
                dic[id] = gameData;
                data_Dic[v] = dic;
            }
            else
            {
                data_Dic[v][id] = gameData;
            }


        }

        public static void AddData(string id, GameVersion.Version v, GameData gameData, byte[] modifyData, byte[] orcData)
        {
            AddData(id, v, gameData);

            if (!DataSet.ContainsKey(v))
            {
                var dicx = new Dictionary<string, ChangeData>();

                ChangeData changeData;
                changeData.orcData = orcData;
                changeData.modifyData = modifyData;
                dicx[id] = changeData;

                DataSet[v] = dicx;
            }else
            {
                ChangeData changeData;
                changeData.orcData = orcData;
                changeData.modifyData = modifyData;

                DataSet[v][id] = changeData;
            }

        }

        public static void AddData(string id,GameVersion.Version v,int offset)
        {
            if (!data_Offset.ContainsKey(v))
            {
                var dic = new Dictionary<string, int>();
                dic[id] = offset;
                data_Offset[v] = dic;
            }
            else if (!data_Offset[v].ContainsKey(id))
            {
                data_Offset[v][id] = offset;
            }
        }
            
        public static byte[] GetModifyData(string id)
        {
           var version = GameInformation.CurentVersion;

            if (DataSet.ContainsKey(version))
            {
                return HandleGetData(id, DataSet[version], false);
            }
            else if (DataSet.ContainsKey(GameVersion.Version.Default))
            {
                return HandleGetData(id, DataSet[GameVersion.Version.Default], false);
            }
            return new byte[] { 0 };

        }


        public static byte[] GetOrcData(string id)
        {
            var version = GameInformation.CurentVersion;

            if (DataSet.ContainsKey(version))
            {
                return HandleGetData(id, DataSet[version], true);
            }
            else if (DataSet.ContainsKey(GameVersion.Version.Default))
            {
                return HandleGetData(id, DataSet[GameVersion.Version.Default], true);
            }

            return new byte[] { 0 };
        }

        public static int GetOffSet(string id)
        {
            var version = GameInformation.CurentVersion;

            int ret = 0;

            if (data_Offset.ContainsKey(version))
            {

                ret = HandleGetOffset(id,data_Offset[version]);

            }
            else if (data_Offset.ContainsKey(GameVersion.Version.Default))
            {

                ret = HandleGetOffset(id, data_Offset[GameVersion.Version.Default]);
            }

            return ret;
        }

        public static IntPtr GetAddress(string id)
        {
            var version = GameInformation.CurentVersion;

            IntPtr ret = IntPtr.Zero;

            if (data_Dic.ContainsKey(version))
            {
                Dictionary<string, GameData> dic = data_Dic[version];

                ret = HandleGetAddress(version, dic, id);

            }
            else if (data_Dic.ContainsKey(GameVersion.Version.Default))
            {
                var dic = data_Dic[GameVersion.Version.Default];

                ret = HandleGetAddress(GameVersion.Version.Default, dic, id);

            }

            return ret;
        }

        static byte[] HandleGetData(string id, Dictionary<string, ChangeData> dic, bool isOrc)
        {
            if (dic.ContainsKey(id))
            {
                if (isOrc)
                {
                    return dic[id].orcData;
                }
                else
                {
                    return dic[id].modifyData;
                }
            }

            return new byte[] { 0 };
        }
        static IntPtr HandleGetAddress(GameVersion.Version v, Dictionary<string, GameData> dic, string id)
        {
            IntPtr ret = IntPtr.Zero;
            if (dic.ContainsKey(id))
            {
                if (!curentGameDataAddress.ContainsKey(id))
                {
                    var dataAddress = dic[id].GetDataAddress();

                    if (dataAddress == null)
                    {
                        throw new ZeroAddressException("地址错误！", data_Dic[v][id]);
                    }

                    curentGameDataAddress.TryAdd(id, dataAddress);
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

        static int HandleGetOffset(string id, Dictionary<string, int> dic)
        {
            if (dic.ContainsKey(id))
            {
                return dic[id];
            }
            else if(data_Offset[GameVersion.Version.Default].ContainsKey(id))
            {
                return data_Offset[GameVersion.Version.Default][id];
            }
            
            return 0;
        }
    }
}
