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
using Newtonsoft.Json;
using System.Security.Cryptography;
using System.Net.NetworkInformation;
using System.Management;
using System.Diagnostics;
using System.Web;
using System.Net;                    // + WebClient
using System.IO;                     // + IO
using System.Web.Script.Serialization; // + JavaScriptSerializer

namespace ST
{
    public partial class devices : Form
    {
        dataSetFill ds = new dataSetFill();
        baseinfo userInfo = new baseinfo(UserSession.LoggedUserID);
        BaseUrl Url = new BaseUrl();

        public devices()
        {
            InitializeComponent();

            gridView2.CustomUnboundColumnData += (sender, e) =>
            {
                GridView view = sender as GridView;
                if (e.Column.FieldName == "dd" && e.IsGetData)
                    e.Value = view.GetRowHandle(e.ListSourceRowIndex) + 1;
            };
        }

        public void too_Load(object sender, EventArgs e)
        {
            try { fillgridDevices(); }
            catch { }
            finally { }
        }

        public void fillgridDevices()
        {
            try { gridControl2.DataSource = ds.gridFill("getdevices"); }
            catch (Exception ee) { MessageBox.Show(ee.ToString()); }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            gridView2.ActiveFilterString = "";
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            PrintGridview.Print(
                gridView2,
                20, 15, 15, 10,
                gridView2.GroupPanelText,
                "",
                userInfo.comName,
                "Хэвлэсэн:" + userInfo.userName,
                DateTime.Now.ToString("yyyy-MM-dd"),
                true
            );
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            try
            {
                adddevice adddev = new adddevice(this);
                adddev.ognoo.DateTime = DateTime.Now;
                adddev.ShowDialog();
            }
            catch (Exception ee) { MessageBox.Show(ee.ToString()); }
        }

        private void устгахToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult dsr = MessageBox.Show("Сонгогдсон борлуулалтын буцаалт хийхдээ итгэлтэй байна уу.", "Анхаар", MessageBoxButtons.YesNo);
                if (dsr == DialogResult.Yes)
                {
                    // Устгах команд
                    // ...
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.ToString(), ""); }
        }

        private void gridControl2_Click(object sender, EventArgs e)
        {
            try { }
            catch { }
            finally { }
        }

        private void textEdit1_EditValueChanged_1(object sender, EventArgs e)
        {
            gridView2.ActiveFilterString =
                "ner LIKE '%" + textEdit1.Text + "%'  or mark LIKE '%" + textEdit1.Text + "%'  or ULSdugaar LIKE '%" + textEdit1.Text + "%'";
        }

        private void comboBoxEdit1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string searchtext = "";
            if (comboBoxEdit1.SelectedIndex == 0) searchtext = "";
            if (comboBoxEdit1.SelectedIndex == 1) searchtext = "машин механизм";
            if (comboBoxEdit1.SelectedIndex == 2) searchtext = "багаж, тоног төхөөрөмж";
            if (comboBoxEdit1.SelectedIndex == 3) searchtext = "ХАБЭА хэрэгсэл";

            gridView2.ActiveFilterString = "devicetype LIKE '%" + searchtext + "%'";
        }

        private void comboBoxEdit1_TextChanged(object sender, EventArgs e)
        {
            gridView2.ActiveFilterString =
                "ner LIKE '%" + textEdit1.Text + "%'  or mark LIKE '%" + textEdit1.Text + "%'  or ULSdugaar LIKE '%" + textEdit1.Text + "%'";
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            try
            {
                dataSetFill dc = new dataSetFill();
                string id = gridView2.GetFocusedRowCellValue("id").ToString().Trim();
                DialogResult dr = MessageBox.Show("Тоног төхөөрөмжийн мэдээллийг утсгах уу?", "Анхаар", MessageBoxButtons.YesNo);
                if (dr == DialogResult.Yes)
                {
                    var data = new NameValueCollection();
                    data["deleteid"] = id;
                    data["ali_table"] = "devices";
                    MessageBox.Show(dc.exec_command("deleteAll", data));
                }
            }
            catch (Exception ee) { MessageBox.Show(ee.ToString()); }
            finally { fillgridDevices(); }
        }

        private void засахToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                editdevice edd = new editdevice(this);
                edd.deviceID.Text = gridView2.GetFocusedRowCellValue("id").ToString().Trim();
                edd.ner.Text = gridView2.GetFocusedRowCellValue("ner").ToString().Trim();
                edd.mark.Text = gridView2.GetFocusedRowCellValue("mark").ToString().Trim();
                edd.ooriin.Text = gridView2.GetFocusedRowCellValue("ooriin").ToString().Trim();
                edd.too.Text = gridView2.GetFocusedRowCellValue("too").ToString().Trim();
                edd.ready.Text = gridView2.GetFocusedRowCellValue("ready").ToString().Trim();
                edd.made.Text = gridView2.GetFocusedRowCellValue("made").ToString().Trim();
                edd.devicetype.Text = gridView2.GetFocusedRowCellValue("devicetype").ToString().Trim();
                edd.power.Text = gridView2.GetFocusedRowCellValue("power").ToString().Trim();
                edd.producted.Text = gridView2.GetFocusedRowCellValue("producted").ToString().Trim();
                edd.ulsdugaar.Text = gridView2.GetFocusedRowCellValue("ULSdugaar").ToString().Trim();
                var ogVal = gridView2.GetFocusedRowCellValue("ognoo");
                DateTime dt;
                if (ogVal != null && DateTime.TryParse(ogVal.ToString(), out dt))
                {
                    edd.ognoo.DateTime = dt;
                }
                edd.ShowDialog();
            }
            catch (Exception ee) { MessageBox.Show(ee.ToString()); }
        }

        // ===== API models (devices) =====
        private class ApiFileEntry
        {
            public string name = string.Empty;
            public string url = string.Empty;
            public string type = string.Empty;
            public string ext = string.Empty;
            public long size = 0;
            public long bytes = 0;
            public string modified = string.Empty;

            public long SizeBytes { get { return (size > 0 ? size : bytes); } }
        }

        private class ApiFileList
        {
            public int success = 0;
            public string error = string.Empty;
            public List<ApiFileEntry> files = new List<ApiFileEntry>();
        }

        private class ApiFileList2
        {
            public bool ok = false;
            public string error = string.Empty;
            public List<ApiFileEntry> files = new List<ApiFileEntry>();
        }

        // ===== devices/{id}/ фолдероос файлын жагсаалт авах =====
        private List<ApiFileEntry> FetchFileList(string folderUrl, string ext)
        {
            try
            {
                if (!folderUrl.EndsWith("/")) folderUrl += "/";

                string api = Url.GetUrl() + "api/getfilelist.php"
                           + "?url=" + Uri.EscapeDataString(folderUrl)
                           + "&ext=" + Uri.EscapeDataString(string.IsNullOrEmpty(ext) ? "*" : ext);

                using (var wc = new WebClient())
                {
                    ServicePointManager.Expect100Continue = true;
                    ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072; // TLS 1.2

                    wc.Encoding = Encoding.UTF8;
                    string json = wc.DownloadString(api);
                    if (json.Trim().Equals("nodata", StringComparison.OrdinalIgnoreCase))
                        return new List<ApiFileEntry>();

                    var js = new JavaScriptSerializer();
                    var r1 = js.Deserialize<ApiFileList>(json);
                    if (r1 != null && r1.success == 1 && r1.files != null) return r1.files;

                    var r2 = js.Deserialize<ApiFileList2>(json);
                    if (r2 != null && r2.ok && r2.files != null) return r2.files;

                    string err = (r1 != null && !string.IsNullOrEmpty(r1.error)) ? r1.error :
                                 (r2 != null && !string.IsNullOrEmpty(r2.error)) ? r2.error :
                                 "Файл олдсонгүй.";
                    XtraMessageBox.Show(err, "Мэдээлэл");
                    return new List<ApiFileEntry>();
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("Жагсаалт татахад алдаа: " + ex.Message, "Алдаа");
                return new List<ApiFileEntry>();
            }
        }

        // === Double-click: тухайн төхөөрөмжийн PDF жагсаалт/нээх/нэмэх/устгах ===
        private void gridControl2_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                var idObj = gridView2.GetFocusedRowCellValue("id");
                if (idObj == null) return;

                int deviceId;
                if (!int.TryParse(idObj.ToString(), out deviceId)) return;

                string folderUrl = Url.GetUrl() + "dist/uploads/devices/" + deviceId + "/";
                var files = FetchFileList(folderUrl, "pdf");

                using (var dlg = new DeviceFilesForm(
                    "Төхөөрөмжийн файлууд — ID: " + deviceId,
                    folderUrl,
                    files,
                    () => FetchFileList(folderUrl, "pdf"),
                    deviceId
                ))
                {
                    dlg.ShowDialog(this);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Алдаа: " + ex.Message);
            }
        }

        private void нэмэхToolStripMenuItem_Click(object sender, EventArgs e)
        {
            simpleButton3_Click(sender, e);
        }

        // === DEVICE файлын жагсаалт/preview + ADD/DELETE ===
        private class DeviceFilesForm : XtraForm
        {
            private readonly string _folderUrl;                 // .../dist/uploads/devices/{id}/
            private readonly Func<List<ApiFileEntry>> _reload;  // дахин ачаалах функц
            private readonly int _deviceId;                     // server API-д дамжуулна
            private List<ApiFileEntry> _files;

            private DevExpress.XtraEditors.ListBoxControl list;
            private DevExpress.XtraEditors.SimpleButton btnAdd, btnDelete, btnRefresh, btnClose;

            public DeviceFilesForm(string title,
                                   string folderUrl,
                                   List<ApiFileEntry> files,
                                   Func<List<ApiFileEntry>> reload,
                                   int deviceId)
            {
                _folderUrl = folderUrl.EndsWith("/") ? folderUrl : (folderUrl + "/");
                _files = files ?? new List<ApiFileEntry>();
                _reload = reload;
                _deviceId = deviceId;

                Text = title;
                Width = 720; Height = 520;
                StartPosition = FormStartPosition.CenterParent;

                list = new DevExpress.XtraEditors.ListBoxControl { Dock = DockStyle.Fill };
                list.DoubleClick += (s, e) => OpenSelected();
                list.KeyDown += (s, e) => { if (e.KeyCode == Keys.Enter) { e.Handled = true; OpenSelected(); } };
                Controls.Add(list);

                var pnl = new FlowLayoutPanel { Dock = DockStyle.Bottom, Height = 46, FlowDirection = FlowDirection.RightToLeft };

                btnClose = new DevExpress.XtraEditors.SimpleButton { Text = "Хаах", Width = 90, DialogResult = DialogResult.Cancel };
                btnRefresh = new DevExpress.XtraEditors.SimpleButton { Text = "Дахин ачаалах", Width = 120 };
                btnDelete = new DevExpress.XtraEditors.SimpleButton { Text = "Устгах", Width = 90 };
                btnAdd = new DevExpress.XtraEditors.SimpleButton { Text = "Нэмэх…", Width = 90 };

                btnRefresh.Click += (s, e) => ReloadList();
                btnDelete.Click += (s, e) => DeleteSelected();
                btnAdd.Click += (s, e) => AddFiles();

                pnl.Controls.Add(btnClose);
                pnl.Controls.Add(btnRefresh);
                pnl.Controls.Add(btnDelete);
                pnl.Controls.Add(btnAdd);
                Controls.Add(pnl);

                CancelButton = btnClose;

                FillList();
            }

            private class Lbi
            {
                public string Text { get; set; }
                public ApiFileEntry File { get; set; }
            }

            private Uri ApiUri(string relative)  // ж: "api/fileupload.php"
            {
                Uri baseUri = new Uri(_folderUrl);
                return new Uri(baseUri, "/" + relative.TrimStart('/'));
            }

            private void FillList()
            {
                list.DataSource = null;
                list.Items.Clear();

                List<Lbi> view = new List<Lbi>();
                int n = 1;
                foreach (ApiFileEntry f in _files)
                {
                    double mb = (f.SizeBytes > 0 ? (f.SizeBytes / 1024.0 / 1024.0) : 0.0);
                    string mod = string.IsNullOrWhiteSpace(f.modified) ? "" : " — " + f.modified;

                    view.Add(new Lbi
                    {
                        Text = string.Format("{0}. {1} ({2:0.##} MB){3}", n, f.name, mb, mod),
                        File = f
                    });
                    n++;
                }

                list.DisplayMember = "Text";
                list.ValueMember = "File";
                list.DataSource = view;
            }

            private void ReloadList()
            {
                try
                {
                    var fresh = (_reload != null ? _reload() : null);
                    if (fresh != null) { _files = fresh; FillList(); }
                }
                catch (Exception ex)
                {
                    XtraMessageBox.Show("Дахин ачаахад алдаа: " + ex.Message);
                }
            }

            private void OpenSelected()
            {
                int idx = list.SelectedIndex;
                if (idx < 0 || idx >= _files.Count) return;

                ApiFileEntry entry = _files[idx];
                string url = !string.IsNullOrEmpty(entry.url) ? entry.url : (_folderUrl + entry.name);

                try { new FileViewer(url); }
                catch (Exception ex) { XtraMessageBox.Show("Файл нээхэд алдаа: " + ex.Message); }
            }

            // ----- ФАЙЛ НЭМЭХ (зөвхөн PDF) -----
            private void AddFiles()
            {
                try
                {
                    using (OpenFileDialog ofd = new OpenFileDialog())
                    {
                        ofd.Title = "PDF файл нэмэх";
                        ofd.Multiselect = true;
                        ofd.Filter = "PDF файлууд (*.pdf)|*.pdf";
                        ofd.DefaultExt = "pdf";
                        ofd.CheckFileExists = true;

                        if (ofd.ShowDialog(this) != DialogResult.OK) return;

                        ServicePointManager.Expect100Continue = true;
                        ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072; // TLS 1.2

                        foreach (string path in ofd.FileNames)
                        {
                            if (!string.Equals(Path.GetExtension(path), ".pdf", StringComparison.OrdinalIgnoreCase))
                                continue;

                            using (WebClient wc = new WebClient())
                            {
                                wc.Headers.Add("Content-Type", "application/pdf");
                                // server: fileupload.php?deviceID=..&id=devicedoc
                                Uri up = ApiUri("api/fileupload.php?deviceID=" + _deviceId + "&id=devices");
                                wc.UploadFile(up, "POST", path);
                            }
                        }

                        ReloadList();
                    }
                }
                catch (Exception ex)
                {
                    XtraMessageBox.Show("Файл нэмэхэд алдаа: " + ex.Message);
                }
            }

            // ----- ФАЙЛ УСТГАХ -----
            private void DeleteSelected()
            {
                int idx = list.SelectedIndex;
                if (idx < 0 || idx >= _files.Count) return;

                ApiFileEntry entry = _files[idx];
                if (XtraMessageBox.Show("“" + entry.name + "” файлыг устгах уу?",
                                        "Баталгаажуулалт",
                                        MessageBoxButtons.YesNo,
                                        MessageBoxIcon.Question) != DialogResult.Yes)
                    return;

                try
                {
                    ServicePointManager.Expect100Continue = true;
                    ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072; // TLS 1.2

                    using (var wc = new WebClient())
                    {
                        wc.Encoding = Encoding.UTF8;

                        // GET: deletefile.php?id=DEVICE&deviceID=123&name=scan.pdf
                        string baseUrl = ApiUri("api/deletefile.php").ToString();
                        string url = baseUrl
                            + "?id=DEVICE"
                            + "&deviceID=" + _deviceId
                            + "&name=" + Uri.EscapeDataString(entry.name);

                        string resp = wc.DownloadString(url);
                        if (resp.IndexOf("\"ok\":true", StringComparison.OrdinalIgnoreCase) < 0)
                        {
                            XtraMessageBox.Show("Серверийн хариу: " + resp, "Анхаар");
                            return;
                        }
                    }

                    ReloadList();
                }
                catch (Exception ex)
                {
                    XtraMessageBox.Show("Файл устгахад алдаа: " + ex.Message);
                }
            }
        }
    }
}
