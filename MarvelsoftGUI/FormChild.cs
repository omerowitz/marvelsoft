using System;
using System.Windows.Forms;

namespace MarvelsoftGUI
{
    public partial class FormChild : Form
    {
        public FormChild()
        {
            InitializeComponent();
        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
