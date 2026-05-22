using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Game_app.UI
{
    public partial class BossVideo : Form
    {
        public BossVideo()
        {
            InitializeComponent();

            // 1. Hide the play/pause/volume controls so it looks like a real movie
            axWindowsMediaPlayer1.uiMode = "none";

            // 2. Play the video!
            axWindowsMediaPlayer1.URL = Application.StartupPath + @"\BossVideo.mp4";
            axWindowsMediaPlayer1.Ctlcontrols.play();

            // 3. Listen for when the video changes state (like when it finishes)
            axWindowsMediaPlayer1.PlayStateChange += AxWindowsMediaPlayer1_PlayStateChange;
        }

        private void AxWindowsMediaPlayer1_PlayStateChange(object sender, AxWMPLib._WMPOCXEvents_PlayStateChangeEvent e)
        {
            // State 8 means the video finished playing
            if (e.newState == 8)
            {
                // Show a placeholder message since the Boss Level isn't ready
                MessageBox.Show("Cinematic finished! Boss Level coming soon...");

                // Send the player back to the Main Menu for now
                MainMenu menu = new MainMenu();
                menu.FormClosed += (s, args) => Application.Exit();
                menu.Show();

                this.Hide();
            }
        }

    }
}
