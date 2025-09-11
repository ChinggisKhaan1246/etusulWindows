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
    public partial class editita : Form
    {
        ita i;
        public editita(ita ii)
        {
            InitializeComponent();
            i = ii;
        }
        
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            try
            {
                dataSetFill ds = new dataSetFill();
                var data = new NameValueCollection();
                data["id"] = itaID.Text.Trim();
                data["ovog"] = ovog.Text;
                data["ner"] = ner.Text;
                data["atushaal"] = atushaal.Text;
                data["proff"] = proff.Text;
                data["phone"] = phone.Text;
                data["email"] = email.Text;
                data["school"] = school.Text;
                data["zereg"] = zereg.Text;
                data["ajillsan"] = ajillsan.Text;
                data["niitAjillsan"] = niitAjilsan.Text.Trim();
                data["URL11"] = URL11.Text;
                
                MessageBox.Show(ds.exec_command("editita", data));

                
                
                if (URL11.Text != "")
                {
                    ServicePointManager.Expect100Continue = true;
                    ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
                    WebClient Client = new System.Net.WebClient();
                    Client.Headers.Add("Content-Type", "binary/octet-stream");
                    string tusulid = "itadoc";
                    if (doctype.Text.Trim() == "MA") tusulid = "itadocMA";
                    if (doctype.Text.Trim() == "OP") tusulid = "itadocOP";

                    byte[] result = Client.UploadFile(Url.GetUrl()+"api/fileupload.php?itaID="+itaID.Text.ToString().Trim()+"&id=" + tusulid, "POST", openFileDialog1.FileName.ToString());
                    string s = System.Text.Encoding.UTF8.GetString(result, 0, result.Length);
 
                }

            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.ToString());
            }
            finally
            {
                i.ita_Load(sender, e);
                this.Hide();
            }
        }
        BaseUrl Url = new BaseUrl();
        private void simpleButton2_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            URL11.Text = openFileDialog1.SafeFileName;
        }

        private void editita_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) // Enter товч дарагдсан эсэхийг шалгана
            {
                simpleButton1.PerformClick(); // simpleButton1_Click функцыг дуудаж байна
            }
        }
    }
}
