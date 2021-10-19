using CheatUITemplt;

namespace WPFCheatUITemplate.GameMode
{
    class Zombie
    {

        public int BaseAddress;
        float x;
        float y;
        int row;
        bool isDath;

        int hatHp;
        int hatMaxHp;

        int annexHp;
        int annexMaxHp;

        int hp;
        int hpMax;

        public int Row
        {
            get
            {
                return CheatTools.ReadMemoryPoninter<int>(AppGameFunManger.Instance.Handle, new int[] { BaseAddress + 0x1c });
            }


        }
        public float Y
        {
            get
            {
                return CheatTools.ReadMemoryPoninter<float>(AppGameFunManger.Instance.Handle, new int[] { BaseAddress + 0x30 });
            }

        }

        public float X
        {
            get
            {
                return CheatTools.ReadMemoryPoninter<float>(AppGameFunManger.Instance.Handle, new int[] { BaseAddress + 0x2C });
            }

        }

        public bool IsDath
        {
            get
            {
                return CheatTools.ReadMemoryPoninter<float>(AppGameFunManger.Instance.Handle, new int[] { BaseAddress + 0xeC }) != 0 ? true : false;
            }


        }

        public int HatHp
        {
            get
            {
                return CheatTools.ReadMemoryPoninter<int>(AppGameFunManger.Instance.Handle, new int[] { BaseAddress + 0xD0 });
            }

        }

        public int HatMaxHp
        {
            get
            {
                return CheatTools.ReadMemoryPoninter<int>(AppGameFunManger.Instance.Handle, new int[] { BaseAddress + 0xD4 });
            }

        }

        public int AnnexHp
        {
            get
            {
                return CheatTools.ReadMemoryPoninter<int>(AppGameFunManger.Instance.Handle, new int[] { BaseAddress + 0xDC });
            }

        }

        public int AnnexMaxHp
        {
            get
            {
                return CheatTools.ReadMemoryPoninter<int>(AppGameFunManger.Instance.Handle, new int[] { BaseAddress + 0xE0 });
            }

        }

        public int Hp
        {
            get
            {
                return CheatTools.ReadMemoryPoninter<int>(AppGameFunManger.Instance.Handle, new int[] { BaseAddress + 0xC8 });
            }

        }

        public int HpMax
        {
            get
            {
                return CheatTools.ReadMemoryPoninter<int>(AppGameFunManger.Instance.Handle, new int[] { BaseAddress + 0xCC });
            }

        }
    }
}
