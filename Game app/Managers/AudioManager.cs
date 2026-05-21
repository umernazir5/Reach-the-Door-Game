using System.Windows.Forms;
using WMPLib;

namespace Game_app.Managers
{
    internal class AudioManager
    {
        private WindowsMediaPlayer musicPlayer;

        public void PlayBackgroundMusic()
        {
            musicPlayer = new WindowsMediaPlayer();
            musicPlayer.URL = Application.StartupPath + @"\Main_Theme.mp3";
            musicPlayer.settings.setMode("loop", true);
            musicPlayer.controls.play();
        }

        public void PlayGameOverMusic()
        {
            musicPlayer = new WindowsMediaPlayer();
            musicPlayer.URL = Application.StartupPath + @"\Fahh.mp3";
            musicPlayer.controls.play();
        }

        public void Stop()
        {
            if (musicPlayer != null)
                musicPlayer.controls.stop();
        }
    }
}

