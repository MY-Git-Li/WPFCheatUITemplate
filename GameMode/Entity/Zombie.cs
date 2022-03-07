using System;
using System.Collections.Generic;

namespace WPFCheatUITemplate.GameMode
{

    class Zombie:GameModeData
    {
       
        public float X { get => GetValue<float>("X"); set => SetValue("X", value); }
        public float Y { get => GetValue<float>("Y"); set => SetValue("Y", value); }
        public int Row { get => GetValue<int>("Row"); set => SetValue("Row", value); }
        public bool IsDie { get => GetValue<bool>("IsDie"); set => SetValue("IsDie", value); }
        public int HatHp { get => GetValue<int>("HatHp"); set => SetValue("HatHp", value); }
        public int HatMaxHp { get => GetValue<int>("HatMaxHp"); set => SetValue("HatMaxHp", value); }
        public int AnnexHp { get => GetValue<int>("AnnexHp"); set => SetValue("AnnexHp", value); }
        public int AnnexMaxHp { get => GetValue<int>("AnnexMaxHp"); set => SetValue("AnnexMaxHp", value); }
        public int Hp { get => GetValue<int>("Hp"); set => SetValue("Hp", value); }
        public int HpMax { get => GetValue<int>("HpMax"); set => SetValue("HpMax", value); }
        public float Speed { get => GetValue<float>("Speed"); set => SetValue("Speed", value); }
        public bool IsCharm { get => GetValue<bool>("IsCharm"); set => SetValue("IsCharm", value); }
        public int SlowdownCountdown { get => GetValue<int>("SlowdownCountdown"); set => SetValue("SlowdownCountdown", value); }
        public int FrozenCountdown { get => GetValue<int>("FrozenCountdown"); set => SetValue("FrozenCountdown", value); }
        public int ButterCountdown { get => GetValue<int>("ButterCountdown"); set => SetValue("ButterCountdown", value); }
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
            AddData("Speed", Def, 0x34);
            AddData("FrozenCountdown", Def, 0xB4);
            AddData("SlowdownCountdown", Def, 0xAC);
            AddData("IsCharm", Def, 0xB8);
            AddData("ButterCountdown", Def, 0xB0);
        }

    }
}
