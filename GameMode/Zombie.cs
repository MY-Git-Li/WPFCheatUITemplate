using CheatUITemplt;
using System;
using System.Collections.Generic;

namespace WPFCheatUITemplate.GameMode
{

    class Zombie:GameModeData
    {
        private float x;

        private float y;

        private int row;

        private bool islive;

        private int hathp;

        private int hattmax;

        private int annexhp;

        private int annexmaxhp;

        private int hp;

        private int hpmax;


        public float X { get => GetValue<float>("X"); set => x = SetValue<float>("X", value); }
        public float Y { get => GetValue<float>("Y"); set => y = SetValue<float>("Y", value); }
        public int Row { get => GetValue<int>("Row"); set => row = SetValue<int>("Row", value); }
        public bool IsLive { get => GetValue<bool>("IsLive"); set => islive = SetValue<bool>("IsLive", value); }
        public int HatHp { get => GetValue<int>("HatHp"); set => hathp = SetValue<int>("HatHp", value); }
        public int HatMaxHp { get => GetValue<int>("HatMaxHp"); set => hattmax = SetValue<int>("HatMaxHp", value); }
        public int AnnexHp { get => GetValue<int>("AnnexHp"); set => annexhp = SetValue<int>("AnnexHp", value); }
        public int AnnexMaxHp { get => GetValue<int>("AnnexMaxHp"); set => annexmaxhp = SetValue<int>("AnnexMaxHp", value); }
        public int Hp { get => GetValue<int>("Hp"); set => hp = SetValue<int>("Hp", value); }
        public int HpMax { get => GetValue<int>("HpMax"); set => hpmax = SetValue<int>("HpMax", value); }

        public Zombie(IntPtr BaseAddress) : base(BaseAddress)
        {
            
        }

        public override void InitData()
        {
            var Def = GameVersion.Version.Default;

            AddData("X", Def, 0x2C);
            AddData("Y", Def, 0x30);
            AddData("Row", Def, 0x1c);
            AddData("IsLive", Def, 0xeC);
            AddData("HatHp", Def, 0xD0);
            AddData("HatMaxHp", Def, 0xD4);
            AddData("AnnexHp", Def, 0xDC);
            AddData("AnnexMaxHp", Def, 0xE0);
            AddData("Hp", Def, 0xC8);
            AddData("HpMax", Def, 0xCC);
        }

    }
}
