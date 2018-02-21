using System;
using System.Drawing;
using System.Net;
using System.Net.Sockets;
using System.Windows.Forms;

namespace Kontur.ImageTransformer.Demo
{
    public static class Utils
    {
        public static void ServerAvaible(string server, Button btn)
        {
            try
            {
                WebRequest.Create(server).GetResponse();
                btn.BackColor = Color.ForestGreen;
            }
            catch (Exception ex)
            {
                if (ex.InnerException is SocketException)
                {
                    btn.BackColor = Color.Crimson;
                }
                else
                {
                    btn.BackColor = Color.ForestGreen;
                }
            }
        }
    }
}