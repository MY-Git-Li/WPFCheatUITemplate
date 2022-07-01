using System;
using System.Media;
using System.Windows.Controls;
using System.Windows.Media;

namespace WPFCheatUITemplate.Core.Voice
{
    class SoundEffect
    {
        SoundPlayer player;

        System.IO.Stream afpiz_if2hn = Properties.Resources.afpiz_if2hn;

        System.IO.Stream ext09_vnxd7 = Properties.Resources.ext09_vnxd7;

        bool isOpen;
        public SoundEffect()
        {
            player = new SoundPlayer();
            isOpen = true;
        }


        public void OPen()
        {
            isOpen = true;
        }
        public void Close()
        {
            isOpen = false;
        }

        public void PlayTurnOnEffect()
        {
            if (!isOpen)
            {
                return;
            }

            player.Stream = afpiz_if2hn;
            player.Play();
        }
        public void PlayTurnOffEffect()
        {
            if (!isOpen)
            {
                return;
            }

            player.Stream = ext09_vnxd7;
            player.Play();
        }
    }
}
