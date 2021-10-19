using CheatUITemplt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFCheatUITemplate.GameMode
{
    class Zombie
    {

        public int BaseAddress;
        float x;
        float y;
        int row;

        public int Row
        {
            get
            {
                return CheatTools.ReadMemoryPoninter<int>(AppGameFunManger.Instance.Handle, new int[] { BaseAddress + 0x1c });
            }


        }
        public float Y
        {
            get
            {
                return CheatTools.ReadMemoryPoninter<float>(AppGameFunManger.Instance.Handle, new int[] { BaseAddress + 0x30 });
            }

        }

        public float X
        {
            get
            {
                return CheatTools.ReadMemoryPoninter<float>(AppGameFunManger.Instance.Handle, new int[] { BaseAddress + 0x2C });
            }

        }


    }
}
