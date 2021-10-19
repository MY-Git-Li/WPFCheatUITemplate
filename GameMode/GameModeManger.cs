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
                int[] { 0x755e0c, 0x868, 0xac });


            for (int i = -maxnum; i < maxnum; i++)
            {
                int address = CheatTools.ReadMemoryPoninter<int>(AppGameFunManger.Instance.Handle, new int[] { 0x755e0c, 0x868, 0xA8, 0xEC + 0x168 * i });

                if (address == 0)
                {
                    Zombie zombie = new Zombie();

                    zombie.BaseAddress = CheatTools.ReadMemoryPoninter<int>(AppGameFunManger.Instance.Handle, new int[] { 0x755e0c, 0x868, 0xA8 }) + 0x168 * i;

                    zombies.Add(zombie);
                }


            }
            return zombies;
        }

    }
}
