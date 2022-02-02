using CheatUITemplt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFCheatUITemplate.Other.Interface;

namespace WPFCheatUITemplate.GameMode
{
    class GameMode: IAddressDatas
    {
        Dictionary<string, int> _data;

        public GameMode()
        {
            _data = new Dictionary<string, int>();
        }

        virtual public void Init() { }
       
        virtual public void InitData() { }

        public int GetAddress(string ID)
        {
            if (_data.ContainsKey(ID))
            {
                return _data[ID];
            }else
            {
               var add = Other.GameFuns.AddressDataManager.GetAddress(ID);
                _data[ID] = add;

                return add;
            }

        }

    }
}
