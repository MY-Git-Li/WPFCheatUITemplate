using System;
using System.Media;
using System.Windows.Controls;
using System.Windows.Media;

namespace WPFCheatUITemplate.Core.Voice
{
    class SoundEffect
    {
        SoundPlayer player;

        bool isOpen;
        public SoundEffect()
        {
            player = new SoundPlayer();
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

            player.Stream = Properties.Resources.afpiz_if2hn;
            player.Play();
        }
        public void PlayTurnOffEffect()
        {
            if (!isOpen)
            {
                return;
            }

            player.Stream = Properties.Resources.ext09_vnxd7;
            player.Play();
        }
    }
}
