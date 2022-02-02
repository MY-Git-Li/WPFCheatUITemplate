using CheatUITemplt;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using WPFCheatUITemplate.Other.Extends;
using WPFCheatUITemplate.Other.GameFuns;
using WPFCheatUITemplate.Other.Interface;

namespace WPFCheatUITemplate.GameMode
{
    abstract class GameModeData
    {

        IntPtr BaseAddress;

        public GameModeData(IntPtr BaseAddress)
        {
            this.BaseAddress = BaseAddress;

            InitData();

        }

        abstract public void InitData();


        virtual public T GetValue<T>(string name) where T : struct
        {
           return CheatTools.ReadMemory<T>(GameInformation.Handle, (IntPtr)(BaseAddress + AddressDataManager.GetOffSet(name)));
        }
        virtual public U SetValue<U>(string name, U Value) where U : struct
        {
            CheatTools.WriteMemory<U>(GameInformation.Handle, (IntPtr)(BaseAddress + AddressDataManager.GetOffSet(name)), Value);
            return GetValue<U>(name);
        }


        virtual public void AddData(string name,GameVersion.Version v,int offset)
        {
            AddressDataManager.AddData(name,v,offset);
        }

    }
}

