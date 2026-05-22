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
    public partial class EnterCode : Form
    {
        public EnterCode()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            string secretCode = txtEnterCode.Text.Trim(); 

            Form nextLevel = null;

            if (secretCode == "level1") nextLevel = new Level1();
            else if (secretCode == "level2") nextLevel = new Level2();
            else if (secretCode == "level3") nextLevel = new Level3();
            else if (secretCode == "finalboss") nextLevel = new FinalBoss();
            else
            {
                MessageBox.Show("Invalid Code! Please try again.");
                return; 
            }

            
            if (nextLevel != null)
            {
                Application.OpenForms["MainMenu"]?.Hide();
                nextLevel.FormClosed += (s, args) => Application.Exit();
                nextLevel.Show();
                this.Close();
            }
        }
    }
}
