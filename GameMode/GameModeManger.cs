using CheatUITemplt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFCheatUITemplate.Other;

namespace WPFCheatUITemplate.GameMode
{
    public struct OffsetBase
    {
        public int[] pointer;
        public int sizeOrOffset;
    };

    class GameModeManger
    {


        static OffsetBase zombieOffset;
        static OffsetBase zombieMaxNumOffset;
        static OffsetBase zombieIslive;

        public static void SetData(GameVersion.Version CurentVersion)
        {
            //根据版本设置数据地址,GameDate
            switch (CurentVersion)
            {
                case GameVersion.Version.Default:
                    DefaultVersion();
                    break;
                case GameVersion.Version.V1_0_0_1051:
                    V1_0_0_1051Version();
                    break;
                default:
                    break;
            }

        }
        static void V1_0_0_1051Version()
        {
            zombieOffset.pointer = new int[] { 0x6A9EC0, 0x768, 0x90 };
            zombieOffset.sizeOrOffset = 0x15c;

            zombieMaxNumOffset.pointer = new int[] { 0x6A9EC0, 0x768, 0x94 };
            zombieMaxNumOffset.sizeOrOffset = 0;

            zombieIslive.pointer = zombieOffset.pointer;
            zombieIslive.sizeOrOffset = 0xEC;
        }
        static void DefaultVersion()
        {
            zombieOffset.pointer = new int[] { 0x755e0c, 0x868, 0xA8 };
            zombieOffset.sizeOrOffset = 0x168;

            zombieMaxNumOffset.pointer = new int[] { 0x755e0c, 0x868, 0xac };
            zombieMaxNumOffset.sizeOrOffset = 0;

            zombieIslive.pointer = zombieOffset.pointer;
            zombieIslive.sizeOrOffset = 0xEC;
        }


        public static List<Zombie> GetZombies()
        {
            List<Zombie> zombies = new List<Zombie>();
            int maxnum = CheatTools.ReadMemory<int>(AppGameFunManger.Instance.Handle, zombieMaxNumOffset.pointer);


            for (int i = -maxnum; i < maxnum; i++)
            {
                int address = CheatTools.ReadMemory<int>(AppGameFunManger.Instance.Handle, 
                    new int[] { zombieIslive.pointer[0], zombieIslive.pointer[1], zombieIslive.pointer[2], zombieIslive.sizeOrOffset + zombieOffset.sizeOrOffset * i });

                if (address == 0)
                {
                    int BaseAddress = CheatTools.ReadMemory<int>(AppGameFunManger.Instance.Handle, zombieOffset.pointer) + zombieOffset.sizeOrOffset * i;
                    

                    Zombie zombie = new Zombie(BaseAddress, GameInformation.CurentVersion);
                    
                    zombies.Add(zombie);
                }


            }
            return zombies;
        }

    }
}
