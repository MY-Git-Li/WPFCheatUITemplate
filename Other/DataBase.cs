using System;
using System.Collections.Concurrent;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using WPFCheatUITemplate.Other.GameFuns;

namespace WPFCheatUITemplate.Other
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


        static public void AddLockData<T>(IntPtr address, T value) where T : struct
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

        static public void AddLockDataById<T>(string id, T value) where T : struct
        {
            AddLockData(AppGameFunManager.Instance.AddressDataMg.GetAddress(id), value);
        }
        static public void DecLockDataById(string id)
        {
            DecLockData(AppGameFunManager.Instance.AddressDataMg.GetAddress(id));
        }

        static public void AddData(string id, GameVersion.Version v, int offset)
        {
            AppGameFunManager.Instance.AddressDataMg.AddData(id, v, offset);
        }

        static public void AddData(string id, GameVersion.Version v, GameData gameData, byte[] modifyData, byte[] orcData)
        {
            AppGameFunManager.Instance.AddressDataMg.AddData(id, v, gameData, modifyData, orcData);
        }


        static public void AddData(string id, GameVersion.Version v, GameData gameData)
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


        static public void WriteMemoryByID<T>(string id, object value) where T : struct
        {
            WriteMemory<T>(AppGameFunManager.Instance.AddressDataMg.GetAddress(id), value);
        }

        static public void WriteMemoryByID<T>(string id, byte[] value) where T : struct
        {
            WriteMemory<T>(AppGameFunManager.Instance.AddressDataMg.GetAddress(id), value);
        }

        static public void WriteMemoryByID(string id, bool isOrc = false)
        {
            if (isOrc)
            {
                WriteMemory<byte>(AppGameFunManager.Instance.AddressDataMg.GetAddress(id), AppGameFunManager.Instance.AddressDataMg.GetOrcData(id));
            }
            else
            {
                WriteMemory<byte>(AppGameFunManager.Instance.AddressDataMg.GetAddress(id), AppGameFunManager.Instance.AddressDataMg.GetModifyData(id));
            }

        }


        static public T ReadMemory<T>(IntPtr address) where T : struct
        {
            return CheatTools.ReadMemory<T>(GameMode.GameInformation.Handle, address);
        }

        static public T ReadMemoryByID<T>(string id) where T : struct
        {
            return ReadMemory<T>(AppGameFunManager.Instance.AddressDataMg.GetAddress(id));
        }

        static public void WriteMemory<T>(IntPtr address, object Value) where T : struct
        {
            byte[] buffer = StructureToByteArray(Value);
            CheatTools.WriteMemory<T>(GameMode.GameInformation.Handle, address, buffer);
            
        }

        static public void WriteMemory<T>(IntPtr address, byte[] Value) where T : struct
        {
            CheatTools.WriteMemory<T>(GameMode.GameInformation.Handle, address, Value);
        }

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
