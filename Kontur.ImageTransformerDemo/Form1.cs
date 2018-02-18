using System;
using System.Drawing;
using System.Net;
using System.Net.Sockets;
using System.Windows.Forms;

namespace Kontur.ImageTransformerDemo
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            Utils.ServerAvaible(txtServer.Text, btnCheck);
        }

        private void btnCheck_Click(object sender, EventArgs e)
        {
            Utils.ServerAvaible(txtServer.Text, btnCheck);
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
        }

        private void txtServer_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Utils.ServerAvaible(txtServer.Text, btnCheck);
            }
        }
    }
}