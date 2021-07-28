using System;
using System.Media;
using System.Windows.Controls;
using System.Windows.Media;

namespace WPFCheatUITemplate.Other
{
    class SoundEffect
    {
        SoundPlayer player;
        public SoundEffect()
        {
            player = new SoundPlayer();
        }

        public void PlayTurnOnEffect()
        {
            player.Stream = Properties.Resources.afpiz_if2hn;
            player.Play();
        }
        public void PlayTurnOffEffect()
        {
            player.Stream = Properties.Resources.ext09_vnxd7;
            player.Play();
        }
    }
}
