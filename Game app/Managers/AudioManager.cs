using System.Windows.Forms;
using WMPLib;

namespace Game_app.Managers
{
    internal class AudioManager
    {
        private WindowsMediaPlayer musicPlayer;
        private WindowsMediaPlayer sfxPlayer; // <-- ADD THIS for sound effects!

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

        public void PlayDamageMusic()
        {
            // Use sfxPlayer here instead of musicPlayer!
            sfxPlayer = new WindowsMediaPlayer();
            sfxPlayer.URL = Application.StartupPath + @"\Damage.mp3";
            sfxPlayer.controls.play();
        }

        public void Stop()
        {
            if (musicPlayer != null)
                musicPlayer.controls.stop();

            // Optional: Stop the sound effect too if the game is over
            if (sfxPlayer != null)
                sfxPlayer.controls.stop();
        }
    }
}