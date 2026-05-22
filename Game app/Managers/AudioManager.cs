using System.Windows.Forms;
using WMPLib;

namespace Game_app.Managers
{
    internal class AudioManager
    {
       
        private static WindowsMediaPlayer musicPlayer;
        private static WindowsMediaPlayer sfxPlayer;

        public void PlayBackgroundMusic()
        {
           
            if (musicPlayer == null)
            {
                musicPlayer = new WindowsMediaPlayer();
                musicPlayer.URL = Application.StartupPath + @"\Main_Theme.mp3";
                musicPlayer.settings.setMode("loop", true);
                musicPlayer.controls.play();
            }
        }

        public void PlayGameOverMusic()
        {
     
            if (musicPlayer != null) musicPlayer.controls.stop();

            musicPlayer = new WindowsMediaPlayer();
            musicPlayer.URL = Application.StartupPath + @"\Fahh.mp3";
            musicPlayer.controls.play();
        }

        public void PlayDamageMusic()
        {
            sfxPlayer = new WindowsMediaPlayer();
            sfxPlayer.URL = Application.StartupPath + @"\Damage.mp3";
            sfxPlayer.controls.play();
        }

        public void Stop()
        {
            if (musicPlayer != null)
                musicPlayer.controls.stop();

            if (sfxPlayer != null)
                sfxPlayer.controls.stop();
        }
    }
}