using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections.Specialized;
using System.IO;
using System.Net;
using Newtonsoft.Json;
using DevExpress.XtraBars;
using DevExpress.XtraRichEdit;
using DevExpress.XtraRichEdit.UI;



namespace ST
{
    public partial class addact : Form
    {
        ildaldact li;
        public addact(ildaldact i)
        {
            InitializeComponent();
            li = i;
        }

        BaseUrl Url = new BaseUrl();
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (actnamefromuser.Text != "")
            {
                try
                {
                    string rtfText;
                    using (MemoryStream ms = new MemoryStream())
                    {
                        richEditControl1.SaveDocument(ms, DevExpress.XtraRichEdit.DocumentFormat.Rtf);
                        rtfText = Encoding.UTF8.GetString(ms.ToArray());  
                    }
                    var jsonData = new Dictionary<string, string>
            {
                { "rtfContent", rtfText }, // RTF өгөгдлийг JSON дотор хадгалах
                { "created_at", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") }
            };
                    string jsonString = JsonConvert.SerializeObject(jsonData);
                    using (WebClient client = new WebClient())
                    {
                        client.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";
                        NameValueCollection values = new NameValueCollection();
                        values["projectID"] = projectID.Text.Trim();
                        values["bookID"] = bookID.Text.Trim();
                        values["comID"] = "999";
                        values["userID"] = UserSession.LoggedUserID.ToString();
                        values["actnamefromuser"] = actnamefromuser.Text;
                        values["actdata"] = jsonString; // 📌 JSON өгөгдлийг `actdata` баганад хадгалах
                        byte[] response = client.UploadValues(Url.GetUrl() + "api/addactdata.php", "POST", values);
                        string responseText = Encoding.UTF8.GetString(response);
                        string decodedResponse = System.Text.RegularExpressions.Regex.Unescape(responseText);
                        MessageBox.Show(decodedResponse, "Амжилттай", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        li.grid2_refresh(); 
                    }
                }
                catch (Exception ee)
                {
                    MessageBox.Show("Алдаа:", ee.ToString());
                }
            }
            else
            {
                MessageBox.Show("Та энэ актыг өөрийнхөөрөө нэрлэнэ үү.");
            }
        }

        private void addact_Load(object sender, EventArgs e)
        {
          
        }

    }
}
