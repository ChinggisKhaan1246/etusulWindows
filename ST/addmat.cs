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
    public partial class addmat : Form
    {
        fmaterials f;
        public addmat(fmaterials ff)
        {
            InitializeComponent();
            f = ff;
        }

        private void addmat_Load(object sender, EventArgs e)
        {
            ognoo.DateTime = DateTime.Now;
        }

        private void addmat_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) // Enter товч дарагдсан эсэхийг шалгана
            {
                simpleButton1.PerformClick(); // simpleButton1_Click функцыг дуудаж байна
            }
        }

        private void too_EditValueChanged(object sender, EventArgs e)
        {
          if (!string.IsNullOrWhiteSpace(too.Text) && !string.IsNullOrWhiteSpace(une.Text))
            {
                long tooValue, uneValue;

                if (long.TryParse(too.Text, out tooValue) && long.TryParse(une.Text, out uneValue))
                {
                    niit.Text = (tooValue * uneValue).ToString();
                }
                else
                {
                    MessageBox.Show("Зөвхөн бүхэл тоон утга оруулна уу.");
                    niit.Text = "0";
                }
            }
            else
            {
                niit.Text = "0"; 
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            try
            {
                dataSetFill dcd = new dataSetFill();
                var data = new NameValueCollection();
                data["comID"] = "999";
                data["matname"] = matname.Text;
                data["negj"] = negj.Text;
                data["too"] = too.Text;
                data["une"] = une.Text;
                data["niit"] = niit.Text;
                data["status"] = status.Text;
                data["ognoo"] = DateTime.Now.ToString("yyyy-MM-dd");
                data["location"] = location.Text;
                data["URL11"] = URL11.Text;
                MessageBox.Show(dcd.exec_command("addmat", data));
                BaseUrl Url = new BaseUrl();
                if (URL11.Text != "")
                {
                    ServicePointManager.Expect100Continue = true;
                    ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
                    WebClient Client = new System.Net.WebClient();
                    Client.Headers.Add("Content-Type", "binary/octet-stream");
                    string tusulid = "serti";
                    byte[] result = Client.UploadFile(Url.GetUrl() + "api/fileupload.php?id=" + tusulid, "POST", openFileDialog1.FileName.ToString());
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
                f.fillGridMat();
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

        private void une_EditValueChanged(object sender, EventArgs e)
        {
          if (!string.IsNullOrWhiteSpace(too.Text) && !string.IsNullOrWhiteSpace(une.Text))
            {
                long tooValue, uneValue;

                if (long.TryParse(too.Text, out tooValue) && long.TryParse(une.Text, out uneValue))
                {
                    niit.Text = (tooValue * uneValue).ToString();
                }
                else
                {
                    MessageBox.Show("Зөвхөн бүхэл тоон утга оруулна уу.");
                    niit.Text = "0";
                }
            }
            else
            {
                niit.Text = "0"; 
            }
        }
    }
}
