using CheatUITemplt;
using System;
using System.Collections.Generic;

namespace WPFCheatUITemplate.GameMode
{

    public struct AttributeOffset<T> where T : struct
    {
        public int baseaddress;
        public int offset;

        public AttributeOffset(int basess,int offset=0)
        {
            baseaddress = basess;
            this.offset = offset;
        }

        public T Value
        {
            get
            {
                return CheatTools.ReadMemory<T>(GameInformation.Handle, (IntPtr)(baseaddress + offset));
            }

            set
            {
                CheatTools.WriteMemory<T>(GameInformation.Handle, (IntPtr)(baseaddress + offset), Value);
            }
        }
    }

    class Zombie
    {
        public AttributeOffset<float> X;

        public AttributeOffset<float> Y;

        public AttributeOffset<int> Row;

        public AttributeOffset<bool> IsLive;

        public AttributeOffset<int>  HatHp;

        public AttributeOffset<int> HatMaxHp;

        public AttributeOffset<int> AnnexHp;

        public AttributeOffset<int> AnnexMaxHp;

        public AttributeOffset<int> Hp;

        public AttributeOffset<int> HpMax;

        public Zombie(int BaseAddress, GameVersion.Version Version)
        {
            X = new AttributeOffset<float>(BaseAddress);

            Y = new AttributeOffset<float>(BaseAddress);

            Row = new AttributeOffset<int>(BaseAddress);

            IsLive = new AttributeOffset<bool>(BaseAddress);

            HatHp = new AttributeOffset<int>(BaseAddress);

            HatMaxHp = new AttributeOffset<int>(BaseAddress);

            AnnexMaxHp = new AttributeOffset<int>(BaseAddress);

            AnnexHp = new AttributeOffset<int>(BaseAddress);

            Hp = new AttributeOffset<int>(BaseAddress);

            HpMax = new AttributeOffset<int>(BaseAddress);

            SetZombieOffset(Version);
        }


        public void SetZombieOffset(GameVersion.Version Version)
        {
            //根据版本来更新数据
            switch (Version)
            {
                case GameVersion.Version.Default:
                    DefaultVersion();
                    break;
                case GameVersion.Version.V1_0_0_1051:
                    DefaultVersion();
                    break;
                default:
                    break;
            }

        }


        void DefaultVersion()
        {
            X.offset = 0x2C;

            Y.offset = 0x30;

            Row.offset = 0x1c;

            IsLive.offset = 0xeC;

            HatHp.offset = 0xD0;

            HatMaxHp.offset = 0xD4;

            AnnexHp.offset = 0xDC;

            AnnexMaxHp.offset = 0xE0;

            Hp.offset = 0xC8;

            HpMax.offset = 0xCC;

        }

    }
}
