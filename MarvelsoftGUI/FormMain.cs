using System;
using System.Windows.Forms;

namespace MarvelsoftGUI
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void BtnChild_Click(object sender, EventArgs e)
        {
            FormChild child = new FormChild();
            child.ShowDialog();
        }

        private void BrowseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            marvelsoftCSVReaderControl1.BrowseFile();
        }

        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void HowToToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Just select your CSV file which was previously generated using the console app and the processing will begin.\n\nWhen done, you'll see all your items in a list view and can iterate through pages by using fancy pagination.", "HowTos...", MessageBoxButtons.OK, MessageBoxIcon.Question);
        }

        private void AboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Development: omerowitz studios\n\nv0.1.1", "About", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
