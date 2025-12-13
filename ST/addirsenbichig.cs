using System;
using System.IO;
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
using WIA;  // COM Reference: Microsoft Windows Image Acquisition Library v2.0
using iTextSharp.text;
using iTextSharp.text.pdf;
using PdfDocument = iTextSharp.text.Document;
using PdfImage = iTextSharp.text.Image;
using PdfWriter = iTextSharp.text.pdf.PdfWriter;
using PdfPageSize = iTextSharp.text.PageSize;

namespace ST
{
    public partial class addirsenbichig : Form
    {
        alban f;

        public addirsenbichig(alban ff)
        {
            InitializeComponent();
            f = ff;
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            try
            {
                dataSetFill dcd = new dataSetFill();
                var data = new NameValueCollection();
                data["projectID"] = "0";
                data["haanaas"] = haanaas.Text;
                data["Bnumber"] = Bnumber.Text;
                data["utga"] = utga.Text;
                data["ognooDoc"] = ognooDoc.DateTime.ToString("yyyy-MM-dd");
                data["ognoo"] = ognoo.DateTime.ToString("yyyy-MM-dd");
                data["URL11"] = URL11.Text;
                data["albantype"] = "irsen";
                MessageBox.Show(dcd.exec_command("addbichig", data));
                if (URL11.Text != "")
                {
                    ServicePointManager.Expect100Continue = true;
                    ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
                    WebClient Client = new System.Net.WebClient();
                    Client.Headers.Add("Content-Type", "binary/octet-stream");
                    string tusulid = "irsen";
                    byte[] result = Client.UploadFile(Url.GetUrl()+"api/fileupload.php?id=" + tusulid, "POST", openFileDialog1.FileName.ToString());
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
                f.FillGridIrsen();
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

        private void addirsenbichig_Load(object sender, EventArgs e)
        {
            ognoo.DateTime = DateTime.Now;
            ognooDoc.DateTime = DateTime.Now;
        }
        private void addirsenbichig_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) // Enter товч дарагдсан эсэхийг шалгана
            {
                simpleButton1.PerformClick(); // simpleButton1_Click функцыг дуудаж байна
            }
        }
        private void simpleButton3_Click(object sender, EventArgs e)
        {
            try
            {
                // 1. WIA CommonDialog үүсгэх
                WIA.CommonDialog wiaDialog = new WIA.CommonDialog();

                // 2. Scanner сонгох
                WIA.Device scanner = wiaDialog.ShowSelectDevice(
                    WIA.WiaDeviceType.ScannerDeviceType,
                    true,  // AlwaysSelectDevice
                    false  // CancelError
                );

                if (scanner == null)
                {
                    MessageBox.Show("Scanner сонгогдсонгүй эсвэл цуцлагдлаа.",
                        "Анхааруулга",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return;
                }

                // 3. Scan хийх
                WIA.Item scannerItem = scanner.Items[1];

                // Scanner тохиргоо (300 DPI)
                SetScannerProperties(scannerItem);

                // Scan хийж зураг авах
                const string WIA_JPEG_FORMAT = "{B96B3CAE-0728-11D3-9D7B-0000F81EF32E}";

                WIA.ImageFile scannedImage = (WIA.ImageFile)wiaDialog.ShowTransfer(
                    scannerItem,
                    WIA_JPEG_FORMAT,
                    false
                );

                if (scannedImage == null)
                {
                    MessageBox.Show("Scan амжилтгүй боллоо.",
                        "Алдаа",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    return;
                }

                // 4. PDF хадгалах зам сонгох
                SaveFileDialog saveDialog = new SaveFileDialog();
                saveDialog.Filter = "PDF файл|*.pdf";
                saveDialog.Title = "PDF хадгалах";
                saveDialog.FileName = "Scan_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".pdf";
                saveDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

                if (saveDialog.ShowDialog() != DialogResult.OK)
                    return;

                // 5. Зургийг PDF болгох
                ConvertScannedImageToPDF(scannedImage, saveDialog.FileName);

                // 6. Амжилттай мессеж
                MessageBox.Show("Scan амжилттай хийгдэж PDF үүслээ!\n\n" + saveDialog.FileName,
                    "Амжилттай",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                // 7. PDF нээх эсэхийг асуух
                DialogResult result = MessageBox.Show("PDF файлыг нээх үү?",
                    "Нээх",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    System.Diagnostics.Process.Start(saveDialog.FileName);
                }
            }
            catch (System.Runtime.InteropServices.COMException comEx)
            {
                MessageBox.Show("Scanner алдаа:\n\n" + comEx.Message +
                    "\n\nScanner асаалттай эсэхийг шалгана уу.",
                    "Scanner алдаа",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Алдаа гарлаа:\n\n" + ex.Message,
                    "Алдаа",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void SetScannerProperties(WIA.Item scannerItem)
        {
            try
            {
                const int WIA_HORIZONTAL_SCAN_RESOLUTION_DPI = 6147;
                const int WIA_VERTICAL_SCAN_RESOLUTION_DPI = 6148;
                const int WIA_BRIGHTNESS = 6154;
                const int WIA_CONTRAST = 6155;

                // 300 DPI тохируулах
                SetWiaProperty(scannerItem.Properties, WIA_HORIZONTAL_SCAN_RESOLUTION_DPI, 300);
                SetWiaProperty(scannerItem.Properties, WIA_VERTICAL_SCAN_RESOLUTION_DPI, 300);

                // Brightness, Contrast
                SetWiaProperty(scannerItem.Properties, WIA_BRIGHTNESS, 0);
                SetWiaProperty(scannerItem.Properties, WIA_CONTRAST, 0);
            }
            catch
            {
                // Scanner тохиргоог дэмжихгүй бол үл хайх
            }
        }

        private void SetWiaProperty(WIA.IProperties properties, int propertyId, object value)
        {
            foreach (WIA.Property p in properties)
            {
                if (p.PropertyID == propertyId)
                {
                    try
                    {
                        p.set_Value(value);
                    }
                    catch
                    {
                        // supported боловч readonly байж болно
                    }
                    return;
                }
            }
            // Property огт байхгүй бол зүгээр алгасна
        }


        // Scan хийсэн зургийг PDF болгох
        private void ConvertScannedImageToPDF(WIA.ImageFile wiaImage, string pdfPath)
        {
            string tempImagePath = Path.GetTempFileName() + ".jpg";

            try
            {
                // 1. WIA зургийг temp файл болгох
                wiaImage.SaveFile(tempImagePath);

                // 2. PDF document үүсгэх
                PdfDocument pdfDoc = new PdfDocument(PdfPageSize.A4, 0, 0, 0, 0);

                using (FileStream fs = new FileStream(pdfPath, FileMode.Create))
                {
                    PdfWriter.GetInstance(pdfDoc, fs);
                    pdfDoc.Open();

                    // 3. Зургийг PDF-д оруулах
                    PdfImage pdfImage = PdfImage.GetInstance(tempImagePath);
                    pdfImage.ScaleToFit(pdfDoc.PageSize.Width, pdfDoc.PageSize.Height);
                    pdfImage.Alignment = PdfImage.ALIGN_CENTER;

                    pdfDoc.Add(pdfImage);
                    pdfDoc.Close();
                }
            }
            finally
            {
                // Temp файл устгах
                if (File.Exists(tempImagePath))
                {
                    File.Delete(tempImagePath);
                }
            }
        }
    }
}
