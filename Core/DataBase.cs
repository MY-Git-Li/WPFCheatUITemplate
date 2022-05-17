using System;
using System.Collections.Concurrent;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using WPFCheatUITemplate.Core.GameFuns;

namespace WPFCheatUITemplate.Core
{
    abstract class DataBase
    {

        class LockData
        {
            public IntPtr address;
            public byte[] value;
        }

        static ConcurrentBag<LockData> lockDatas = new ConcurrentBag<LockData>();

        static DataBase()
        {
            var writeTask = new Task(() => 
            {
                while(true)
                {
                    foreach (var item in lockDatas)
                    {
                        WriteMemory<byte>(item.address, item.value);
                    }

                    Thread.Sleep(100);
                }
                
            });
            writeTask.Start();
        }

        /// <summary>
        /// 添加锁定地址
        /// </summary>
        /// <typeparam name="T">值类型</typeparam>
        /// <param name="address">地址</param>
        /// <param name="value">锁定的值</param>
        /// <exception cref="Exception">重复地址</exception>
        static public void AddLockData<T>(IntPtr address, object value) where T : struct
        {
            byte[] buffer = StructureToByteArray(value);

            foreach (var item in lockDatas)
            {
                if (item.address.Equals(address))
                {
                    throw new Exception($"重复添加锁定地址:{address.ToInt64():X}");
                }
            }

            lockDatas.Add(
                new LockData()
                {
                    address = address,
                    value = buffer,
                });
        }
        /// <summary>
        /// 解除锁定的地址
        /// </summary>
        /// <param name="address">地址</param>
        static public void DecLockData(IntPtr address)
        {
            foreach (var item in lockDatas)
            {
                if (item.address.Equals(address))
                {
                    var dd = item;
                    lockDatas.TryTake(out dd);
                }
            }
        }
        /// <summary>
        /// 通过ID来锁定地址
        /// </summary>
        /// <typeparam name="T">值类型</typeparam>
        /// <param name="id">唯一标识id</param>
        /// <param name="value">值</param>
        static public void AddLockDataById<T>(string id, object value) where T : struct
        {
            AddLockData<T>(GetAddress(id), value);
        }
        /// <summary>
        /// 通过ID来解除锁定
        /// </summary>
        /// <param name="id">唯一标识id</param>
        static public void DecLockDataById(string id)
        {
            DecLockData(GetAddress(id));
        }
        /// <summary>
        ///  通过ID来锁定地址，值为GetModifyData获得
        /// </summary>
        /// <typeparam name="T">字节型</typeparam>
        /// <param name="id">唯一标识id</param>
        static public void AddLockDataById<T>(string id) where T : struct
        {
            AddLockData<T>(GetAddress(id), GetModifyData(id));
        }

        /// <summary>
        /// 添加全局偏移地址
        /// </summary>
        /// <param name="id">唯一标识id</param>
        /// <param name="v">版本</param>
        /// <param name="offset">偏移</param>
        static public void AddOffsetData(string id, GameVersion.Version v, int offset)
        {
            AppGameFunManager.Instance.AddressDataMg.AddOffsetData(id, v, offset);
        }
        /// <summary>
        /// 添加全局地址
        /// </summary>
        /// <param name="id">唯一标识id</param>
        /// <param name="v">版本</param>
        /// <param name="gameData">游戏数据</param>
        /// <param name="modifyData">要修改的数据</param>
        /// <param name="orcData">原始的数据</param>
        static public void AddData(string id, GameVersion.Version v, GameData gameData, byte[] modifyData, byte[] orcData)
        {
            AppGameFunManager.Instance.AddressDataMg.AddData(id, v, gameData, modifyData, orcData);
        }
        /// <summary>
        /// 添加全局地址
        /// </summary>
        /// <param name="id">唯一标识id</param>
        /// <param name="address">地址</param>
        static public void AddData(string id,IntPtr address)
        {
            AppGameFunManager.Instance.AddressDataMg.AddData(id, address);
        }
        /// <summary>
        /// 添加全局地址
        /// </summary>
        /// <param name="id">唯一标识id</param>
        /// <param name="v">版本</param>
        /// <param name="gameData">游戏数据</param>
        static public void AddData(string id, GameVersion.Version v, GameData gameData)
        {
            AppGameFunManager.Instance.AddressDataMg.AddData(id, v, gameData);
        }
        /// <summary>
        /// 得到要修改的数据
        /// </summary>
        /// <param name="id">唯一标识id</param>
        /// <returns></returns>
        static public byte[] GetModifyData(string id)
        {
            return AppGameFunManager.Instance.AddressDataMg.GetModifyData(id);
        }
        /// <summary>
        /// 得到偏移
        /// </summary>
        /// <param name="id">唯一标识id</param>
        /// <returns></returns>
        static public int GetOffSet(string id)
        {
            return AppGameFunManager.Instance.AddressDataMg.GetOffSet(id);
        }
        /// <summary>
        /// 得到原始数据
        /// </summary>
        /// <param name="id">唯一标识id</param>
        /// <returns></returns>
        static public byte[] GetOrcData(string id)
        {
            return AppGameFunManager.Instance.AddressDataMg.GetOrcData(id);
        }
        /// <summary>
        /// 得到游戏最终地址
        /// </summary>
        /// <param name="id">唯一标识id</param>
        /// <returns></returns>
        static public IntPtr GetAddress(string id)
        {
            return AppGameFunManager.Instance.AddressDataMg.GetAddress(id);
        }

        /// <summary>
        /// 通过id写内存
        /// </summary>
        /// <typeparam name="T">值类型</typeparam>
        /// <param name="id">唯一标识id</param>
        /// <param name="value">值</param>
        static public void WriteMemoryByID<T>(string id, object value) where T : struct
        {
            WriteMemory<T>(GetAddress(id), value);
        }
        /// <summary>
        /// 通过id写内存
        /// </summary>
        /// <typeparam name="T">字节型</typeparam>
        /// <param name="id">唯一标识id</param>
        /// <param name="value"></param>
        static public void WriteMemoryByID<T>(string id, byte[] value) where T : struct
        {
            WriteMemory<T>(GetAddress(id), value);
        }
        /// <summary>
        /// 通过id写内存
        /// </summary>
        /// <param name="id">唯一标识id</param>
        /// <param name="isOrc">是否写原数据</param>
        static public void WriteMemoryByID(string id, bool isOrc = false)
        {
            if (isOrc)
            {
                WriteMemory<byte>(GetAddress(id), GetOrcData(id));
            }
            else
            {
                WriteMemory<byte>(GetAddress(id), GetModifyData(id));
            }

        }

        /// <summary>
        /// 读内存
        /// </summary>
        /// <typeparam name="T">读的类型</typeparam>
        /// <param name="address">地址</param>
        /// <returns></returns>
        static public T ReadMemory<T>(IntPtr address) where T : struct
        {
            return CheatTools.ReadMemory<T>(GameMode.GameInformation.Handle, address);
        }
        /// <summary>
        ///  通过id读内存
        /// </summary>
        /// <typeparam name="T">读的类型</typeparam>
        /// <param name="id">唯一标识id</param>
        /// <returns></returns>
        static public T ReadMemoryByID<T>(string id) where T : struct
        {
            return ReadMemory<T>(AppGameFunManager.Instance.AddressDataMg.GetAddress(id));
        }
        /// <summary>
        /// 通过id写内存
        /// </summary>
        /// <typeparam name="T">值类型</typeparam>
        /// <param name="address">地址</param>
        /// <param name="Value">值</param>
        static public void WriteMemory<T>(IntPtr address, object Value) where T : struct
        {
            byte[] buffer = StructureToByteArray(Value);
            CheatTools.WriteMemory<T>(GameMode.GameInformation.Handle, address, buffer);
            
        }
        /// <summary>
        /// 通过id写内存
        /// </summary>
        /// <typeparam name="T">字节类型</typeparam>
        /// <param name="address">地址</param>
        /// <param name="Value">值</param>
        static public void WriteMemory<T>(IntPtr address, byte[] Value) where T : struct
        {
            CheatTools.WriteMemory<T>(GameMode.GameInformation.Handle, address, Value);
        }
        /// <summary>
        /// 读矩阵
        /// </summary>
        /// <param name="address">矩阵地址</param>
        /// <param name="size">矩阵大小</param>
        /// <returns></returns>
        static public float[] ReadMatrix(IntPtr address,int size)
        {
           return CheatTools.ReadMatrix<float>(GameMode.GameInformation.Handle, address, size);
        }
        /// <summary>
        /// 读字符串转ASCII
        /// </summary>
        /// <param name="address">字符串地址</param>
        /// <param name="size">大小</param>
        /// <returns></returns>
        static public string ReadStringToASCII(IntPtr address, int size)
        {
            return CheatTools.ReadStringToASCII(GameMode.GameInformation.Handle, address, size);
        }
        /// <summary>
        /// 读字符串转Unicode
        /// </summary>
        /// <param name="address">字符串地址</param>
        /// <param name="size">大小</param>
        /// <returns></returns>
        static public string ReadStringToUnicode(IntPtr address, int size)
        {
            return CheatTools.ReadStringToUnicode(GameMode.GameInformation.Handle, address, size);
        }
        /// <summary>
        /// 读字符串转UTF8
        /// </summary>
        /// <param name="address">字符串地址</param>
        /// <param name="size">大小</param>
        /// <returns></returns>
        static public string ReadStringToUTF8(IntPtr address, int size)
        {
            return CheatTools.ReadStringToUTF8(GameMode.GameInformation.Handle, address, size);
        }
        /// <summary>
        /// 得到窗口大小
        /// </summary>
        /// <returns></returns>
        static public CheatTools.WindowData GetGameWindowData()
        {
            return CheatTools.GetGameWindowData(GameMode.GameInformation.WindowHandle);
        }
        /// <summary>
        /// 将游戏窗口至前
        /// </summary>
        /// <returns></returns>
        static public int SetGameWindowForegroundWindow()
        {
            return CheatTools.SetForegroundWindow(GameMode.GameInformation.WindowHandle);
        }
        /// <summary>
        /// 转换字节数组到指定结构
        /// </summary>
        /// <typeparam name="T">结构</typeparam>
        /// <param name="bytes">字节数组</param>
        /// <returns></returns>
        static public T ByteArrayToStructure<T>(byte[] bytes) where T : struct
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
        /// <summary>
        /// 转换结构到字节数组
        /// </summary>
        /// <param name="obj">结构</param>
        /// <returns></returns>
        static public byte[] StructureToByteArray(object obj)
        {
            int length = Marshal.SizeOf(obj);
            byte[] array = new byte[length];
            IntPtr pointer = Marshal.AllocHGlobal(length);
            Marshal.StructureToPtr(obj, pointer, true);
            Marshal.Copy(pointer, array, 0, length);
            Marshal.FreeHGlobal(pointer);
            return array;
        }
        /// <summary>
        /// 转换字节数组到浮点数
        /// </summary>
        /// <param name="bytes">字节数组</param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        static public float[] ConvertToFloatArray(byte[] bytes)
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
    }
}
