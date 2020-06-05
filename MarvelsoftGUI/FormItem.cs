using MarvelsoftGUI.models;
using System;
using System.Windows.Forms;

namespace MarvelsoftGUI
{
    public partial class FormItem : Form
    {
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        public InfoProduct Product;

        public FormItem()
        {
            InitializeComponent();
        }

        private void FormItem_Load(object sender, EventArgs e)
        {
            TxtIndex.Text = Product.Index.ToString();
            TxtColor.Text = Product.Color.ToString();
            TxtFilename.Text = Product.Filename;
            TxtId.Text = Product.Id;
            TxtQuantity.Text = Product.Quantity;
            TxtPrice.Text = Product.Price;
        }

        private void FormItem_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
