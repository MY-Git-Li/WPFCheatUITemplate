using CheatUITemplt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFCheatUITemplate.Other;
using WPFCheatUITemplate.Other.Interface;

namespace WPFCheatUITemplate.GameMode
{
    class GameModeManger: GameModeManagerBase
    {

        public override void Init()
        {

            DefaultVersion();

            V1_0_0_1051Version();

        }

        static void V1_0_0_1051Version()
        {

            AddData("zombieHead", GameVersion.Version.V1_0_0_1051, new GameData()
            {
                ModuleName = "PlantsVsZombies.exe",
                ModuleOffsetAddress = 0x2A9EC0,

                IsSignatureCode = false,

                IntPtrOffset = new uint[] { 0x768, 0x90 },
                IsIntPtr = true,
            });

            AddData("zombieSize", GameVersion.Version.V1_0_0_1051, 0x15c);

            AddData("zombieMaxNum", GameVersion.Version.V1_0_0_1051, new GameData()
            {
                ModuleName = "PlantsVsZombies.exe",
                ModuleOffsetAddress = 0x2A9EC0,

                IsSignatureCode = false,

                IntPtrOffset = new uint[] { 0x768, 0x94 },
                IsIntPtr = true,
            });

        }
        static void DefaultVersion()
        {
            AddData("zombieHead", GameVersion.Version.Default, new GameData()
            {
                ModuleName = "PlantsVsZombies.exe",
                ModuleOffsetAddress = 0x355E0C,

                IsSignatureCode = false,

                IntPtrOffset = new uint[] { 0x868, 0xA8 },
                IsIntPtr = true,
            });

            AddData("zombieSize", GameVersion.Version.Default, 0x168);

            AddData("zombieMaxNum", GameVersion.Version.Default, new GameData()
            {
                ModuleName = "PlantsVsZombies.exe",
                ModuleOffsetAddress = 0x355E0C,

                IsSignatureCode = false,

                IntPtrOffset = new uint[] { 0x868, 0xAC },
                IsIntPtr = true,
            });
        }


        public static List<Zombie> GetZombies()
        {
            List<Zombie> zombies = new List<Zombie>();
            int maxnum = CheatTools.ReadMemory<int>(GameInformation.Handle,GetAddress("zombieMaxNum"));

            var zombieHead = GetAddress("zombieHead");

            var zombieSize = GetOffSet("zombieSize");

            for (int i = -maxnum; i < maxnum; i++)
            {


                IntPtr BaseAddress = (IntPtr)(CheatTools.ReadMemory<IntPtr>(GameInformation.Handle, zombieHead).ToInt64() + zombieSize * i);

                Zombie zombie = new Zombie(BaseAddress);

                if (!zombie.IsDie && zombie.Hp > 0 && zombie.Hp <= 100000 && zombie.Row >= 0 && zombie.Row <= 5)
                {
                    zombies.Add(zombie);
                }

            }

            return zombies;
        }

        
    }
}
