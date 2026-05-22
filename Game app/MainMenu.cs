using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Game_app
{
    public partial class MainMenu : Form
    {
        public MainMenu()
        {
            InitializeComponent();
        }

        private void playbutton_Click(object sender, EventArgs e)
        {
            Level1 level1 = new Level1();

            // Ensure the whole app closes when the player eventually closes the game window
            level1.FormClosed += (s, args) => Application.Exit();

            level1.Show();
            this.Hide(); // Hides the Main Menu
        }

        private void EnterYourCode_Click(object sender, EventArgs e)
        {
            EnterCode codeForm = new EnterCode();

            // ShowDialog opens the code form like a popup on top of the main menu
            codeForm.ShowDialog();
        }
    }
}
