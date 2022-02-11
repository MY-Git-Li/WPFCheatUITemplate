using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFCheatUITemplate.GameMode
{
    class Projectile : GameModeData
    {
        public Projectile(IntPtr BaseAddress) : base(BaseAddress)
        {
            var Def = GameVersion.Version.Default;

            AddData("Row", Def, 0x1c);
            AddData("X", Def, 0x30);
            AddData("Y", Def, 0x34);
            AddData("Height", Def, 0x38);
            AddData("XSpeed", Def, 0x3c);
            AddData("YSpeed", Def, 0x40);
            AddData("NotExist", Def, 0x50);
            AddData("Motion", Def, 0x58);
            AddData("RotationAngle1", Def, 0x68);
            AddData("RotationSpeed1", Def, 0x6c);
            AddData("DamageAbility1", Def, 0x74);
            AddData("TracktargetId1", Def, 0x88);
            AddData("Id", Def, 0x90);
        }

        public int Row { get => GetValue<int>("Row"); set => SetValue("Row", value); }
        public float X { get => GetValue<float>("X"); set => SetValue("X", value); }
        public float Y { get => GetValue<float>("Y"); set => SetValue("Y", value); }
        public float Height { get => GetValue<float>("Height"); set => SetValue("Height", value); }
        public float XSpeed { get => GetValue<float>("XSpeed"); set => SetValue("XSpeed", value); }
        public float YSpeed { get => GetValue<float>("YSpeed"); set => SetValue("YSpeed", value); }
        public bool NotExist { get => GetValue<bool>("NotExist"); set => SetValue("NotExist", value); }
        public int Motion { get => GetValue<int>("Motion"); set => SetValue("Motion", value); }
        public float RotationAngle { get => GetValue<float>("RotationAngle"); set => SetValue("RotationAngle", value); }
        public float RotationSpeed { get => GetValue<float>("RotationSpeed"); set => SetValue("RotationSpeed", value); }
        public int DamageAbility { get => GetValue<int>("DamageAbility"); set => SetValue("DamageAbility", value); }
        public int TracktargetId { get => GetValue<int>("TracktargetId"); set => SetValue("TracktargetId", value); }
        public int Id { get => GetValue<int>("Id"); set => SetValue("Id", value); }
    }
}
