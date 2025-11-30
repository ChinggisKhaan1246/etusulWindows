using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Collections.Specialized;
using System.Windows.Forms;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.LookAndFeel;
using DevExpress.XtraEditors;
using DevExpress.XtraPrinting;
using DevExpress.XtraPrintingLinks;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.Utils;
using DevExpress.XtraReports.UI;
using Newtonsoft.Json;
using System.Security.Cryptography;
using System.Net.NetworkInformation;
using System.Management;
using System.Diagnostics;
using System.Web;
using System.IO;
using System.Net;
using Newtonsoft.Json.Linq; //Json messagebox дээр \u0431\ гэж гарахад decode хйихэд хэрэглэж байгаа шүү мартаад байдаг шүү

namespace ST
{
    public partial class freceipt : Form
    {
        public freceipt()
        {
            InitializeComponent();
            gridView1.CustomUnboundColumnData += (sender, e) =>
            {
                GridView view = sender as GridView;
                if (e.Column.FieldName == "dd" && e.IsGetData)
                    e.Value = view.GetRowHandle(e.ListSourceRowIndex) + 1;
            };
        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            try
            {
                addreceipt adr = new addreceipt();
                adr.projectID.Text = projectID.Text.Trim();
                adr.costID.Text = costID.Text.Trim();
                adr.ShowDialog();
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.ToString());
            }
            finally { }
        }

        private void урилгаХэвлэхToolStripMenuItem_Click(object sender, EventArgs e)
        {
            simpleButton4_Click(sender, e);
        }

        private void freceipt_Load(object sender, EventArgs e)
        {
            fillGridReceipt();
        }
        public void fillGridReceipt()
        {
            try
            {
                // Өгөгдлийг унших
                dataSetFill ds = new dataSetFill();
               // gridControl1.DataSource = ds.gridFill("getreceipts", "costID=" + costID.Text.Trim());
                //MessageBox.Show(costID.Text);
                gridView1.IndicatorWidth = 100;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void gridControl1_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                var encode = gridView1.GetFocusedRowCellValue("pic").ToString().Replace(" ", "%20");
                FileViewer flvv = new FileViewer(Url.GetUrl() + "dist/uploads/receipt/" + projectID.Text.Trim() + "/" + encode);
                // MessageBox.Show(encode);
            }
            catch (Exception)
            {
                MessageBox.Show("Chrome суулгачих");
            }
        }
        BaseUrl Url = new BaseUrl();
        private void gridView1_CustomDrawRowIndicator(object sender, RowIndicatorCustomDrawEventArgs e)
        {
            try
            {
                // Indicator-ийн дугаарыг өөрчлөх
                if (e.RowHandle < 0)
                    return;

                // Indicator-ийн текстийг тохируулна
                e.Info.DisplayText = "Файл харах"; // Дугаарын оронд "Зураг" гэж гаргана

                // Текстийн өнгө, фонтыг тохируулах
                e.Appearance.ForeColor = Color.Blue; // Текстийн өнгийг цэнхэр болгоно
                e.Appearance.BackColor = Color.LightGray; // Indicator-ийн арын өнгийг саарал болгоно

                // Текстийн байрлалыг өөрчлөх (заавал биш)
                e.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                e.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.ToString());
            }
        }
        
       private void gridView1_Click(object sender, EventArgs e)
        {
            try
            {
                // Сонгогдсон мөрөөс зураг эсвэл PDF-ийн замыг унших
                string fileName =  gridView1.GetFocusedRowCellValue("pic").ToString().Trim();
                if (!string.IsNullOrEmpty(fileName))
                {
                    BaseUrl Url = new BaseUrl();
                    string fileUrl = Url.GetUrl()+"dist/uploads/receipt/" + projectID.Text + "/" + fileName;
                    if (fileName.EndsWith(".jpg", StringComparison.OrdinalIgnoreCase) ||
                        fileName.EndsWith(".png", StringComparison.OrdinalIgnoreCase) ||
                        fileName.EndsWith(".jpeg", StringComparison.OrdinalIgnoreCase))
                    {
                        // Зураг харуулах
                        ShowImageInWebBrowser(fileUrl);
                    }
                    else if (fileName.EndsWith(".pdf", StringComparison.OrdinalIgnoreCase))
                    {
                        // PDF харуулах
                        ShowPdfInWebBrowser(fileUrl);
                    }
                    else
                    {
                        MessageBox.Show("Дэмжигдээгүй файл байна!", "Алдаа", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    MessageBox.Show("Файл сонгогдоогүй байна!", "Анхааруулга", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Ямар нэг баримт файл хадгалагдаагүй байна. Та [Баримт нэмэх] товчлуур дарж энэ зардалд хамаарах баримт оруулах боломжтой.", "Алдаа", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void GetImageDimensions(string imageURL, out int width, out int height)
        {
            width = 0;
            height = 0;

            try
            {
                // URL-ээс зураг унших хүсэлт үүсгэх
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(imageURL);
                request.Method = "GET";

                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                using (Stream stream = response.GetResponseStream())
                {
                    if (stream != null)
                    {
                        // Зургийг ачаалж хэмжээг авах
                        using (Image image = Image.FromStream(stream))
                        {
                            width = image.Width;
                            height = image.Height;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Алдаа: " + ex.Message);
            }
        }

        private void ShowImageInWebBrowser(string imageUrl)
        {
            // Зургийн URL хадгалах
            this.currentImageUrl = imageUrl;

            // WebBrowser-ийн одоогийн хэмжээг ашиглан зургийн HTML-ийг тохируулах
            UpdateImageInWebBrowser();
        }

        private void UpdateImageInWebBrowser()
        {
            if (string.IsNullOrEmpty(currentImageUrl))
                return;

            int imageWidth, imageHeight;

            // Зургийн бодит хэмжээг авах
            GetImageDimensions(currentImageUrl, out imageWidth, out imageHeight);

            // WebBrowser-ийн хэмжээнд нийцүүлэхийн тулд хэмжээг өөрчлөх
            int webBrowserWidth = webBrowser1.Width - 20; // Padding хасах
            int webBrowserHeight = webBrowser1.Height - 20;

            if (imageWidth > webBrowserWidth || imageHeight > webBrowserHeight)
            {
                // Пропорцыг хадгалж хэмжээг багасгах
                double widthRatio = (double)webBrowserWidth / imageWidth;
                double heightRatio = (double)webBrowserHeight / imageHeight;
                double scale = Math.Min(widthRatio, heightRatio);

                imageWidth = (int)(imageWidth * scale);
                imageHeight = (int)(imageHeight * scale);
            }

            // HTML агуулгыг тохируулах
            string htmlContent = string.Format(@"
        <html>
            <head>
                <style>
                    html, body {{
                        margin: 0;
                        padding: 0;
                        width: 100%;
                        height: 100%;
                        display: flex;
                        justify-content: center;
                        align-items: center;
                        background-color: #f0f0f0;
                    }}
                </style>
            </head>
            <body>
                <center><img src='{0}' alt='Зураг ачаалагдаж байна...' width='{1}' height='{2}' /></center>
            </body>
        </html>", currentImageUrl, imageWidth, imageHeight);

            webBrowser1.DocumentText = htmlContent;
        }

        private string currentImageUrl;


        private void ShowPdfInWebBrowser(string pdfUrl)
        {
            string htmlContent = string.Format(@"
    <html>
        <body style='margin:0; padding:0; overflow:hidden;'>
            <iframe src='{0}' style='width:100%; height:100%; border:none;'></iframe>
        </body>
    </html>", pdfUrl);

            webBrowser1.DocumentText = htmlContent;
        }

        private void устгахToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                dataSetFill dc = new dataSetFill();
                string id = gridView1.GetFocusedRowCellValue("id").ToString().Trim();
                DialogResult dr = MessageBox.Show("Зардалын баримт мэдээллийг утсгах уу?", "Анхаар", MessageBoxButtons.YesNo);
                if (dr == System.Windows.Forms.DialogResult.Yes)
                {
                    var data = new NameValueCollection();
                    data["deleteid"] = id;
                    data["ali_table"] = "receipts";
                    MessageBox.Show(dc.exec_command("deleteAll", data));
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.ToString());
            }
            finally
            {
                fillGridReceipt();
            }
        }

        private void keytext_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                gridView1.ActiveFilterString = "costname LIKE '%" + keytext.Text + "%'  or pic LIKE '%" + keytext.Text + "%'  or Rtailbar LIKE '%" + keytext.Text + "%'";
            }
            catch (Exception)
            {
                MessageBox.Show("Өгөгдөл байхгүй.");
            }

        }
        private bool IsWebBrowserContentEmpty()
        {
            // WebBrowser-ийн DocumentText эсвэл Body-г шалгах
            if (webBrowser1.Document != null && webBrowser1.Document.Body != null)
            {
                string content = webBrowser1.Document.Body.InnerHtml;
                return string.IsNullOrWhiteSpace(content); // Хоосон эсэхийг шалгах
            }
            return true; // Хэрэв Document байхгүй бол хоосон гэж үзэх
        }
        private void simpleButton6_Click(object sender, EventArgs e)
        {
            try
            {
                if (IsWebBrowserContentEmpty())
                {
                    MessageBox.Show("Хэвлэх контент олдсонгүй!", "Анхааруулга", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                
                webBrowser1.ShowPrintPreviewDialog();
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Хэвлэх тохиргооны цонх харуулахад алдаа гарлаа: " + ex.Message, "Алдаа", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void webBrowser1_Resize(object sender, EventArgs e)
        {
            UpdateImageInWebBrowser();
        }

        private void gridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                EditRtailbar();
                e.Handled = true;
            }
        }
        public static string DecodeUnicode(string unicodeString)
        {
            return System.Text.RegularExpressions.Regex.Unescape(unicodeString);
        }
        private void EditRtailbar()
        {
            try
            {
                gridView1.PostEditor();
                gridView1.UpdateCurrentRow();

                object idObj = gridView1.GetFocusedRowCellValue("id");
                object RtailbarObj = gridView1.GetFocusedRowCellValue("Rtailbar");

                if (idObj == null)
                {
                    MessageBox.Show("Мэдээлэл дутуу байна!", "Алдаа", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string id = idObj.ToString().Trim();
                string Rtailbar = RtailbarObj != null ? RtailbarObj.ToString().Trim() : ""; // 🔹 Null бол хоосон текст болгоно

                dataSetFill ds = new dataSetFill();
                var data = new NameValueCollection
        {
            { "id", id },
            { "Rtailbar", Rtailbar },
        };

                string result = ds.exec_command("editreceipt", data);

                if (!string.IsNullOrEmpty(result))
                {
                    if (result.Trim() == "success")
                    {
                        gridView1.SetFocusedRowCellValue("Rtailbar", Rtailbar); // 🔹 Null байсан ч хоосон текст болж хадгалагдана
                    }
                    else
                    {
                        string decodedMessage = DecodeUnicode(result);
                        MessageBox.Show("Серверийн хариу: " + decodedMessage, "Алдаа", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Серверээс хариу ирсэнгүй!", "Алдаа", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Алдаа: " + ex.Message, "Алдаа", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}
