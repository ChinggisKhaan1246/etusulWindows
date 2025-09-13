using System;
using System.Configuration;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Newtonsoft.Json;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.LookAndFeel;
using DevExpress.XtraEditors;
using DevExpress.XtraPrinting;
using DevExpress.XtraPrintingLinks;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.Utils;
using System.Net;

namespace ST
{
    public partial class editdevice : Form
    {
        devices f;
        public editdevice(devices ff)
        {
            InitializeComponent();
            f = ff;
        }

        private void editdevice_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) // Enter товч дарагдсан эсэхийг шалгана
            {
                simpleButton1.PerformClick(); // simpleButton1_Click функцыг дуудаж байна
            }
        }

        private void simpleButtonFile_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            URL11.Text = openFileDialog1.SafeFileName;
        }
        BaseUrl Url = new BaseUrl();
        public string devtype;
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            try
            {
                dataSetFill dcd = new dataSetFill();
                var data = new NameValueCollection();
                data["deviceID"] = deviceID.Text.Trim();
                data["ner"] = ner.Text.Trim();
                data["mark"] = mark.Text.Trim();
                data["made"] = made.Text.Trim();
                data["too"] = too.Text.Trim();
                data["ready"] = ready.Text.Trim();
                data["ooriin"] = ooriin.Text.Trim();
                data["ulsdugaar"] = ulsdugaar.Text.Trim();
                data["producted"] = producted.Text.Trim();
                data["power"] = power.Text.Trim();
                data["devicetype"] = devicetype.Text.Trim();
                data["ognoo"] = ognoo.DateTime.ToString("yyyy-MM-dd");
                data["URL11"] = URL11.Text.Trim();

                MessageBox.Show(dcd.exec_command("editdevice", data));
                if (URL11.Text != "")
                {
                    ServicePointManager.Expect100Continue = true;
                    ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
                    WebClient Client = new System.Net.WebClient();
                    Client.Headers.Add("Content-Type", "binary/octet-stream");
                    string tusulid = "devices";
                    string deviceIDD = deviceID.Text.Trim();
                    byte[] result = Client.UploadFile(Url.GetUrl() + "api/fileupload.php?deviceID=" + deviceIDD + "&id=" + tusulid, "POST", openFileDialog1.FileName.ToString());
                    string s = System.Text.Encoding.UTF8.GetString(result, 0, result.Length);
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.ToString());
            }
            finally
            {
                this.Hide();
                f.fillgridDevices();
            }
        }

        private void devicetype_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (devicetype.SelectedIndex == 0)
            {
                devtype = "m";
            }
            if (devicetype.SelectedIndex == 1)
            {
                devtype = "b";
            }
            if (devicetype.SelectedIndex == 2)
            {
                devtype = "h";
            }
        }
    }
}
