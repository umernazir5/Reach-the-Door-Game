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
    public partial class NextLevel : Form
    {
        private Form nextLevelToLoad;
        public NextLevel(Form targetLevel)
        {
            InitializeComponent();
            this.nextLevelToLoad = targetLevel;
        }

        private void btnNextLevel_Click(object sender, EventArgs e)
        {
            
            nextLevelToLoad.FormClosed += (s, args) => Application.Exit();
            nextLevelToLoad.Show();
            this.Hide();
        }
    }
}
