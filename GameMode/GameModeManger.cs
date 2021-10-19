using CheatUITemplt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFCheatUITemplate.GameMode
{
    class GameModeManger
    {
        public static List<Zombie> GetZombies()
        {
            List<Zombie> zombies = new List<Zombie>();
            int maxnum = CheatTools.ReadMemoryPoninter<int>(AppGameFunManger.Instance.Handle, new
                int[] { 0x6A9EC0, 0x768, 0x94 });


            for (int i = 0; i < maxnum; i++)
            {
                int address = CheatTools.ReadMemoryPoninter<int>(AppGameFunManger.Instance.Handle, new int[] { 0x6A9EC0, 0x768, 0x90, 0xEC + 0x15C * i });

                if (address == 0)
                {
                    Zombie zombie = new Zombie();

                    zombie.BaseAddress = CheatTools.ReadMemoryPoninter<int>(AppGameFunManger.Instance.Handle, new int[] { 0x6A9EC0, 0x768, 0x90 }) + 0x15C * i;

                    zombies.Add(zombie);
                }


            }
            return zombies;
        }

    }
}
