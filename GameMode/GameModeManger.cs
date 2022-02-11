﻿using CheatUITemplt;
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
            ZomBie_V1_0_0();

            Plant_v1_0_0();

            Projectile_v1_0_0();
        }
        static void DefaultVersion()
        {
            Zombie_VDef();

            Plant_VDef();

            Projectile_vDef();
        }


        private static void Projectile_vDef()
        {
            AddData("projectileHead", GameVersion.Version.Default, new GameData()
            {
                ModuleName = "PlantsVsZombies.exe",
                ModuleOffsetAddress = 0x355E0C,

                IsSignatureCode = false,

                IntPtrOffset = new uint[] { 0x868, 0xE0 },
                IsIntPtr = true,
            });

            AddData("projectileSize", GameVersion.Version.Default, 0x94);

            AddData("projectileMaxNum", GameVersion.Version.Default, new GameData()
            {
                ModuleName = "PlantsVsZombies.exe",
                ModuleOffsetAddress = 0x355E0C,

                IsSignatureCode = false,

                IntPtrOffset = new uint[] { 0x868, 0xE4 },
                IsIntPtr = true,
            });
        }

        private static void Projectile_v1_0_0()
        {
            AddData("projectileHead", GameVersion.Version.V1_0_0_1051, new GameData()
            {
                ModuleName = "PlantsVsZombies.exe",
                ModuleOffsetAddress = 0x2A9EC0,

                IsSignatureCode = false,

                IntPtrOffset = new uint[] { 0x768, 0xC8 },
                IsIntPtr = true,
            });

            AddData("projectileSize", GameVersion.Version.V1_0_0_1051, 0x94);

            AddData("projectileMaxNum", GameVersion.Version.V1_0_0_1051, new GameData()
            {
                ModuleName = "PlantsVsZombies.exe",
                ModuleOffsetAddress = 0x2A9EC0,

                IsSignatureCode = false,

                IntPtrOffset = new uint[] { 0x768, 0xCC },
                IsIntPtr = true,
            });
        }

        private static void Plant_v1_0_0()
        {
            AddData("plantHead", GameVersion.Version.V1_0_0_1051, new GameData()
            {
                ModuleName = "PlantsVsZombies.exe",
                ModuleOffsetAddress = 0x2A9EC0,

                IsSignatureCode = false,

                IntPtrOffset = new uint[] { 0x768, 0xAC },
                IsIntPtr = true,
            });

            AddData("plantSize", GameVersion.Version.V1_0_0_1051, 0x14C);

            AddData("plantMaxNum", GameVersion.Version.V1_0_0_1051, new GameData()
            {
                ModuleName = "PlantsVsZombies.exe",
                ModuleOffsetAddress = 0x2A9EC0,

                IsSignatureCode = false,

                IntPtrOffset = new uint[] { 0x768, 0xB0 },
                IsIntPtr = true,
            });
        }

        private static void ZomBie_V1_0_0()
        {
            AddData("zombieHead", GameVersion.Version.V1_0_0_1051, new GameData()
            {
                ModuleName = "PlantsVsZombies.exe",
                ModuleOffsetAddress = 0x2A9EC0,

                IsSignatureCode = false,

                IntPtrOffset = new uint[] { 0x768, 0x90 },
                IsIntPtr = true,
            });

            AddData("zombieSize", GameVersion.Version.V1_0_0_1051, 0x15C);

            AddData("zombieMaxNum", GameVersion.Version.V1_0_0_1051, new GameData()
            {
                ModuleName = "PlantsVsZombies.exe",
                ModuleOffsetAddress = 0x2A9EC0,

                IsSignatureCode = false,

                IntPtrOffset = new uint[] { 0x768, 0x94 },
                IsIntPtr = true,
            });
        }

        private static void Plant_VDef()
        {
            AddData("plantHead", GameVersion.Version.Default, new GameData()
            {
                ModuleName = "PlantsVsZombies.exe",
                ModuleOffsetAddress = 0x355E0C,

                IsSignatureCode = false,

                IntPtrOffset = new uint[] { 0x868, 0xC4 },
                IsIntPtr = true,
            });

            AddData("plantSize", GameVersion.Version.Default, 0x14C);

            AddData("plantMaxNum", GameVersion.Version.Default, new GameData()
            {
                ModuleName = "PlantsVsZombies.exe",
                ModuleOffsetAddress = 0x355E0C,

                IsSignatureCode = false,

                IntPtrOffset = new uint[] { 0x868, 0xC8 },
                IsIntPtr = true,
            });
        }

        private static void Zombie_VDef()
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

        public static List<Plant> GetPlants()
        {
            List<Plant> plants = new List<Plant>();
            int maxnum = CheatTools.ReadMemory<int>(GameInformation.Handle, GetAddress("plantMaxNum"));

            var plantHead = GetAddress("plantHead");

            var plantSize = GetOffSet("plantSize");

            for (int i = -maxnum; i < maxnum; i++)
            {

                IntPtr BaseAddress = (IntPtr)(CheatTools.ReadMemory<IntPtr>(GameInformation.Handle, plantHead).ToInt64() + plantSize * i);

                Plant plant = new Plant(BaseAddress);
                if (plant.Hp > 0 && plant.Exist == 0)
                {
                    plants.Add(plant);
                }

            }

            return plants;
        }


        public static List<Projectile> GetProjectiles()
        {
            List<Projectile> projectiles = new List<Projectile>();
            int maxnum = CheatTools.ReadMemory<int>(GameInformation.Handle, GetAddress("projectileMaxNum"));

            var plantHead = GetAddress("projectileHead");

            var plantSize = GetOffSet("projectileSize");

            for (int i = -maxnum; i < maxnum; i++)
            {

                IntPtr BaseAddress = (IntPtr)(CheatTools.ReadMemory<IntPtr>(GameInformation.Handle, plantHead).ToInt64() + plantSize * i);

                Projectile pro = new Projectile(BaseAddress);

                if (!pro.NotExist)
                {
                    projectiles.Add(pro);
                }
            }

            return projectiles;
        }
    }
}
