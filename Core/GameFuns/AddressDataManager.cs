using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using WPFCheatUITemplate.GameMode;
using WPFCheatUITemplate.Core.Exceptions;

namespace WPFCheatUITemplate.Core.GameFuns
{
    class AddressDataManager
    {

        Dictionary<GameVersion.Version, Dictionary<string, GameData>> data_Dic = new Dictionary<GameVersion.Version, Dictionary<string, GameData>>();

        ConcurrentDictionary<string, GameDataAddress> curentGameDataAddress = new ConcurrentDictionary<string, GameDataAddress>();

        Dictionary<GameVersion.Version, Dictionary<string, int>> data_Offset = new Dictionary<GameVersion.Version, Dictionary<string, int>>();

        Dictionary<string,IntPtr> generalAddress = new Dictionary<string, IntPtr>();

        bool isAddDataInitComplete = false;

        struct ChangeData
        {
            public byte[] modifyData;
            public byte[] orcData;
        }

        static Dictionary<GameVersion.Version, Dictionary<string, ChangeData>> DataSet = new Dictionary<GameVersion.Version, Dictionary<string, ChangeData>>();


        public AddressDataManager()
        {
            Init();
        }

        void Init()
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

        public void DataClear()
        {
            curentGameDataAddress.Clear();

            generalAddress.Clear();
        }

        public void DataInit()
        {
            Task.Factory.StartNew(() => { while (!isAddDataInitComplete) ; GetAllGameDataAddress(); });
        }


        void GetAllGameDataAddress()
        {

            if (data_Dic.ContainsKey(GameVersion.Version.Default))
            {
                foreach (var item in data_Dic[GameVersion.Version.Default])
                {
                    if (!curentGameDataAddress.ContainsKey(item.Key))
                    {
                        curentGameDataAddress.TryAdd(item.Key, item.Value.GetDataAddress());
                    }

                }
            }

            if (data_Dic.ContainsKey(GameInformation.CurentVersion))
            {
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
        }

        public void AddData(string id,IntPtr address)
        {
            var version = GameInformation.CurentVersion;

            if (data_Dic[version].ContainsKey(id))
            {
                throw new Exception($"重复添加id:{id}");
            }


            if (!generalAddress.ContainsKey(id))
            {
                generalAddress.Add(id, address);
            }else
            {
                throw new Exception($"重复添加通用型地址id:{id}");
            }
                
        }

        public void AddData(string id, GameVersion.Version v, GameData gameData)
        {

            if (!data_Dic.ContainsKey(v))
            {
                var dic = new Dictionary<string, GameData>();
                dic[id] = gameData;
                data_Dic[v] = dic;
            }
            else if(!data_Dic[v].ContainsKey(id))
            {
                data_Dic[v][id] = gameData;
            }else
            {
                throw new Exception($"重复添加id:{id}，版本：{v}");
            }


        }

        public void AddData(string id, GameVersion.Version v, GameData gameData, byte[] modifyData, byte[] orcData)
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
            }
            else
            {
                ChangeData changeData;
                changeData.orcData = orcData;
                changeData.modifyData = modifyData;

                if(!DataSet[v].ContainsKey(id))
                {
                    DataSet[v][id] = changeData;
                }else
                {
                    throw new Exception($"重复添加id:{id}，版本：{v}");
                }

            }

        }

        public void AddOffsetData(string id, GameVersion.Version v, int offset)
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
            }else
            {
                throw new Exception($"重复添加id:{id}，版本：{v}");
            }
        }

        public byte[] GetModifyData(string id)
        {
            var version = GameInformation.CurentVersion;

            if (DataSet.ContainsKey(version))
            {
                var ret = HandleGetData(id, DataSet[version], false);
                if (ByteArrayToStructure<int>(ret).Equals(0))
                {
                    if (DataSet.ContainsKey(GameVersion.Version.Default))
                    {
                        var dicx = DataSet[GameVersion.Version.Default];

                        ret = HandleGetData(id, dicx, false);
                    }
                }

                return ret;
            }
            else if (DataSet.ContainsKey(GameVersion.Version.Default))
            {
                return HandleGetData(id, DataSet[GameVersion.Version.Default], false);
            }
            return new byte[] { 0 };

        }


        public byte[] GetOrcData(string id)
        {
            var version = GameInformation.CurentVersion;

            if (DataSet.ContainsKey(version))
            {

                var ret = HandleGetData(id, DataSet[version], true);
                if (ByteArrayToStructure<int>(ret).Equals(0))
                {
                    if (DataSet.ContainsKey(GameVersion.Version.Default))
                    {
                        var dicx = DataSet[GameVersion.Version.Default];

                        ret = HandleGetData(id, dicx, true);
                    }
                }

                return ret;

            }
            else if (DataSet.ContainsKey(GameVersion.Version.Default))
            {
                return HandleGetData(id, DataSet[GameVersion.Version.Default], true);
            }

            return new byte[] { 0 };
        }

        public int GetOffSet(string id)
        {
            var version = GameInformation.CurentVersion;

            int ret = 0;

            if (data_Offset.ContainsKey(version))
            {
                ret = HandleGetOffset(id, data_Offset[version]);

                if (ret == 0)
                {
                    if (data_Offset.ContainsKey(GameVersion.Version.Default))
                    {
                        ret = HandleGetOffset(id, data_Offset[GameVersion.Version.Default]);
                    }
                }

            }
            else if (data_Offset.ContainsKey(GameVersion.Version.Default))
            {

                ret = HandleGetOffset(id, data_Offset[GameVersion.Version.Default]);
            }

            return ret;
        }

        public IntPtr GetAddress(string id)
        {
            var version = GameInformation.CurentVersion;

            IntPtr ret = IntPtr.Zero;

            if (generalAddress.ContainsKey(id))
            {
                ret = generalAddress[id];

                return ret;
            }

            if (data_Dic.ContainsKey(version))
            {
                Dictionary<string, GameData> dic = data_Dic[version];

                ret = HandleGetAddress(version, dic, id);

                if (ret.Equals(IntPtr.Zero))
                {
                    if (data_Dic.ContainsKey(GameVersion.Version.Default))
                    {
                        var dicx = data_Dic[GameVersion.Version.Default];

                        ret = HandleGetAddress(GameVersion.Version.Default, dicx, id);
                    }
                }
                
            }
            else if (data_Dic.ContainsKey(GameVersion.Version.Default))
            {
                var dic = data_Dic[GameVersion.Version.Default];

                ret = HandleGetAddress(GameVersion.Version.Default, dic, id);

            }
           
            return ret;
        }

        byte[] HandleGetData(string id, Dictionary<string, ChangeData> dic, bool isOrc)
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
        IntPtr HandleGetAddress(GameVersion.Version v, Dictionary<string, GameData> dic, string id)
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

        int HandleGetOffset(string id, Dictionary<string, int> dic)
        {
            if (dic.ContainsKey(id))
            {
                return dic[id];
            }
            else if (data_Offset[GameVersion.Version.Default].ContainsKey(id))
            {
                return data_Offset[GameVersion.Version.Default][id];
            }

            return 0;
        }





        #region Conversion
        private static T ByteArrayToStructure<T>(byte[] bytes) where T : struct
        {
            var handle = GCHandle.Alloc(bytes, GCHandleType.Pinned);
            try
            {
                return (T)Marshal.PtrToStructure(handle.AddrOfPinnedObject(), typeof(T));
            }
            finally
            {
                handle.Free();
            }
        }

        private static byte[] StructureToByteArray(object obj)
        {
            int length = Marshal.SizeOf(obj);
            byte[] array = new byte[length];
            IntPtr pointer = Marshal.AllocHGlobal(length);
            Marshal.StructureToPtr(obj, pointer, true);
            Marshal.Copy(pointer, array, 0, length);
            Marshal.FreeHGlobal(pointer);
            return array;
        }

        private static float[] ConvertToFloatArray(byte[] bytes)
        {
            if (bytes.Length % 4 != 0)
            {
                throw new ArgumentException();
            }

            float[] floats = new float[bytes.Length / 4];
            for (int i = 0; i < floats.Length; i++)
            {
                floats[i] = BitConverter.ToSingle(bytes, i * 4);
            }
            return floats;
        }
        #endregion
    }
}
