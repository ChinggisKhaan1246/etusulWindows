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
    public partial class addreceipt : Form
    {
        //freceipt f;
        public addreceipt()
            
        {
            InitializeComponent();
            //f = ff;
        }
        BaseUrl Url = new BaseUrl();
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            try
            {
                if (URL11.Text != "")
                {

                    dataSetFill dcd = new dataSetFill();
                    var data = new NameValueCollection();
                    data["costID"] = costID.Text.Replace(",","");
                    data["Rtailbar"] = Rtailbar.Text;
                    data["URL11"] = URL11.Text;
                    data["projectID"] = projectID.Text;
                    data["userID"] = UserSession.LoggedUserID.ToString().Trim();
                    ServicePointManager.Expect100Continue = true;
                    ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
                    WebClient Client = new System.Net.WebClient();
                    Client.Headers.Add("Content-Type", "binary/octet-stream");
                    string tusulid = "receipt";
                    string url = Url.GetUrl() + "api/fileupload.php?id=" + tusulid + "&projectID=" + projectID.Text.Trim() + "&userID=" + UserSession.LoggedUserID.ToString().Trim();
                    byte[] result = Client.UploadFile(url, "POST", openFileDialog1.FileName.ToString());
                    string s = System.Text.Encoding.UTF8.GetString(result, 0, result.Length);
                    MessageBox.Show(dcd.exec_command("addreceipt", data));
                }
                else
                {
                    MessageBox.Show("Ямар нэг файл сонгогдоогүй байна.");
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.ToString());
            }
            finally 
            {
                //f.fillGridReceipt();
            }


        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            URL11.Text = openFileDialog1.SafeFileName;
        }

        private void addreceipt_Load(object sender, EventArgs e)
        {
            dateEdit1.DateTime = DateTime.Now;
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
        }

        private void addreceipt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) // Enter товч дарагдсан эсэхийг шалгана
            {
                simpleButton1.PerformClick(); // simpleButton1_Click функцыг дуудаж байна
            }
        }
    }
}
