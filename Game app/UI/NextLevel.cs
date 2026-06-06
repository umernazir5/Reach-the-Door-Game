using System;
using System.Windows.Forms;

namespace Game_app
{
    public partial class NextLevel : Form
    {
        private Func<Form> nextLevelFactory;

        public NextLevel(Func<Form> levelFactory)
        {
            InitializeComponent();
            this.nextLevelFactory = levelFactory;
        }

        private void btnNextLevel_Click(object sender, EventArgs e)
        {
            Form nextLevel = nextLevelFactory();
            nextLevel.Show();
            this.Close();
        }
    }
}