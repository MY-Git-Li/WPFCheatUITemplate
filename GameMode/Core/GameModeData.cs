using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using WPFCheatUITemplate.Core;
using WPFCheatUITemplate.Core.Extends;
using WPFCheatUITemplate.Core.GameFuns;
using WPFCheatUITemplate.Core.Interface;

namespace WPFCheatUITemplate.GameMode
{
    abstract class GameModeData:DataBase
    {
        GameVersion.Version version = GameInformation.CurentVersion;

        Dictionary<GameVersion.Version, Dictionary<string, int>> data_Offset = new Dictionary<GameVersion.Version, Dictionary<string, int>>();
        
        IntPtr baseAddress;

        public IntPtr BaseAddress { get => baseAddress;}

        public GameModeData(IntPtr BaseAddress)
        {
            this.baseAddress = BaseAddress;

            InitData();

        }

        virtual public void InitData() { }


        virtual public T GetValue<T>(string name) where T : struct
        {
           return CheatTools.ReadMemory<T>(GameInformation.Handle, (IntPtr)(baseAddress + GetOffSet(name)));
        }
        virtual public U SetValue<U>(string name, U Value) where U : struct
        {
            CheatTools.WriteMemory<U>(GameInformation.Handle, (IntPtr)(baseAddress + GetOffSet(name)), Value);
            return GetValue<U>(name);
        }


        public void AddData(string id,GameVersion.Version v,int offset)
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

        int GetOffSet(string id)
        {
            version = GameInformation.CurentVersion;

            int ret = 0;

            if (data_Offset.ContainsKey(version))
            {

                ret = HandleGetOffset(id, data_Offset[version]);

            }
            else if (data_Offset.ContainsKey(GameVersion.Version.Default))
            {

                ret = HandleGetOffset(id, data_Offset[GameVersion.Version.Default]);
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
    }

   
}

