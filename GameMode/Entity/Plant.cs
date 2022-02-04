using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFCheatUITemplate.GameMode
{
    class Plant : GameModeData
    {
      
        public int X { get => GetValue<int>("X"); set => SetValue("X",value); }
        public int Y { get => GetValue<int>("Y"); set => SetValue("Y", value); }
        public int Row { get => GetValue<int>("Row"); set => SetValue("Row", value); }
        public int Column { get => GetValue<int>("Column"); set => SetValue("Column", value); }
        public byte Exist { get => GetValue<byte>("Exist"); set => SetValue("Exist", value); }
        public int Hp { get => GetValue<int>("Hp"); set => SetValue("Hp", value); }
        public int HpMax { get => GetValue<int>("HpMax"); set => SetValue("HpMax", value); }
        public bool NotSleeping { get => GetValue<bool>("NotSleeping"); set => SetValue("NotSleeping", value); }
        public int ShootOrProductCountdown { get => GetValue<int>("ShootOrProductCountdown"); set => SetValue("ShootOrProductCountdown", value); }
        public int ShootOrProductInterval { get => GetValue<int>("ShootOrProductInterval"); set => SetValue("ShootOrProductInterval", value); }
        public int ShootingCountdown { get => GetValue<int>("ShootingCountdown"); set => SetValue("ShootingCountdown", value); }
        public bool Visible { get => GetValue<bool>("Visible"); set => SetValue("Visible", value); }

        public Plant(IntPtr BaseAddress) : base(BaseAddress)
        {
            var Def = GameVersion.Version.Default;

            AddData("X", Def, 0x8);
            AddData("Y", Def, 0xC);
            AddData("Row", Def, 0x1C);
            AddData("Column", Def, 0x28);
            AddData("Exist", Def, 0x141);
            AddData("Hp", Def, 0x40);
            AddData("HpMax", Def, 0x44);
            AddData("NotSleeping", Def, 0x143);
            AddData("ShootOrProductCountdown", Def, 0x58);
            AddData("ShootOrProductInterval", Def, 0x5C);
            AddData("ShootingCountdown", Def, 0x90);
            AddData("Visible", Def, 0x18);
        }
    }
}
