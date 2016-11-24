using System;
using System.Windows.Forms;

namespace basbust
{
    public partial class Info : Form
    {
        public Info()
        {
            InitializeComponent();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/stanriders/bassboost");
        }
    }
}
