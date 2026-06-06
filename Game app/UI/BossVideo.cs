using System;
using System.Windows.Forms;

namespace Game_app.UI
{
    /// <summary>
    /// Plays the boss intro video then launches Level 3 (FinalBoss form).
    /// Sequence: Level1 -> Level2 -> BossVideo -> FinalBoss (Level 3)
    /// </summary>
    public partial class BossVideo : Form
    {
        public BossVideo()
        {
            InitializeComponent();

            // Hide media player controls — cinematic look
            axWindowsMediaPlayer1.uiMode = "none";

            // Play the intro video
            axWindowsMediaPlayer1.URL = Application.StartupPath + @"\Video.mp4";
            axWindowsMediaPlayer1.Ctlcontrols.play();

            // Listen for playback state changes
            axWindowsMediaPlayer1.PlayStateChange += AxWindowsMediaPlayer1_PlayStateChange;
        }

        private void AxWindowsMediaPlayer1_PlayStateChange(object sender, AxWMPLib._WMPOCXEvents_PlayStateChangeEvent e)
        {
            // State 8 = MediaEnded
            if (e.newState == 8)
            {
                // After the video, go to Level 3 (the FinalBoss level)
                FinalBoss level3 = new FinalBoss();
                level3.FormClosed += (s, args) => Application.Exit();
                level3.Show();

                this.Hide();
            }
        }
    }
}
