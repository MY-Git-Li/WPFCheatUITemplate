using CheatUITemplt;
using System;
using System.Collections.Generic;

namespace WPFCheatUITemplate.GameMode
{

    class Zombie:GameModeData
    {
       
        public float X { get => GetValue<float>("X"); set => SetValue<float>("X", value); }
        public float Y { get => GetValue<float>("Y"); set => SetValue<float>("Y", value); }
        public int Row { get => GetValue<int>("Row"); set => SetValue<int>("Row", value); }
        public bool IsDie { get => GetValue<bool>("IsDie"); set => SetValue<bool>("IsDie", value); }
        public int HatHp { get => GetValue<int>("HatHp"); set => SetValue<int>("HatHp", value); }
        public int HatMaxHp { get => GetValue<int>("HatMaxHp"); set => SetValue<int>("HatMaxHp", value); }
        public int AnnexHp { get => GetValue<int>("AnnexHp"); set => SetValue<int>("AnnexHp", value); }
        public int AnnexMaxHp { get => GetValue<int>("AnnexMaxHp"); set => SetValue<int>("AnnexMaxHp", value); }
        public int Hp { get => GetValue<int>("Hp"); set => SetValue<int>("Hp", value); }
        public int HpMax { get => GetValue<int>("HpMax"); set => SetValue<int>("HpMax", value); }

        public Zombie(IntPtr BaseAddress) : base(BaseAddress)
        {
            var Def = GameVersion.Version.Default;

            AddData("X", Def, 0x2C);

            AddData("Y", Def, 0x30);

            AddData("Row", Def, 0x1c);

            AddData("IsDie", Def, 0xeC);

            AddData("HatHp", Def, 0xD0);

            AddData("HatMaxHp", Def, 0xD4);

            AddData("AnnexHp", Def, 0xDC);

            AddData("AnnexMaxHp", Def, 0xE0);

            AddData("Hp", Def, 0xC8);

            AddData("HpMax", Def, 0xCC);
        }

    }
}
