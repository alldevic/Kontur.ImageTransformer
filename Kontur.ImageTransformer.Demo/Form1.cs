using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Windows.Forms;

namespace Kontur.ImageTransformer.Demo
{
    public partial class frmMain : Form
    {
        private readonly HttpData _data = new HttpData();

        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            prpData.SelectedObject = _data;
            lblRoute.Text = _data.Route();
            Utils.ServerAvaible(txtServer.Text, btnCheck);
            imgRequest.Image = _data.RequestImage;
        }

        private void btnCheck_Click(object sender, EventArgs e)
        {
            Utils.ServerAvaible(txtServer.Text, btnCheck);
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            var restClient = new HttpClient();
            restClient.BaseAddress = new Uri(txtServer.Text);

            using (var stream = new FileStream("Resources/zebra.png", FileMode.Open))
            {
                using (var content = new StreamContent(stream))
                {
                    content.Headers.ContentType = new MediaTypeHeaderValue(_data.ContentType);
                    var response = restClient.PostAsync(_data.Route(), content).Result;
                    _data.StatusCode = response.StatusCode;
                    if (response.IsSuccessStatusCode)
                    {
                        _data.ResponseImage = new Bitmap(response.Content.ReadAsStreamAsync().Result);
                    }
                    else
                    {
                        _data.ResponseImage = new Bitmap(1, 1, PixelFormat.Format32bppArgb);
                    }

                    imgResponse.Image = _data.ResponseImage;
                    imgRequest.Refresh();
                    prpData.Refresh();
                }
            }
        }

        private void txtServer_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Utils.ServerAvaible(txtServer.Text, btnCheck);
            }
        }

        private void prpRequest_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            var item = e.ChangedItem.Label;
            var parent = e.ChangedItem.Parent.Label;

            if (item == "HTTP Method" || parent == "Coordinates" || item == "Coordinates" ||
                item == "Action")
            {
                lblRoute.Text = _data.Route();
            }
        }
    }
}