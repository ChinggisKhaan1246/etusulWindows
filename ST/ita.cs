using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Collections.Specialized;
using System.Web.Script.Serialization; 
using System.Windows.Forms;
using System.Security.Cryptography;
using System.Net.NetworkInformation;
using System.Management;
using System.Diagnostics;
using System.Web;
using System.Net;    // WebClient энд байна
using System.IO; 
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.LookAndFeel;
using DevExpress.XtraEditors;
using DevExpress.XtraPrinting;
using DevExpress.XtraPrintingLinks;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.Utils;
using Newtonsoft.Json;


namespace ST
{
    public partial class ita : Form
    {
        Form1 f;
        public ita(Form1 ff)
        {
            InitializeComponent();
            f = ff;

            gridView5.CustomUnboundColumnData += (sender, e) =>
            {
                GridView view = sender as GridView;
                if (e.Column.FieldName == "dd5" && e.IsGetData)
                    e.Value = view.GetRowHandle(e.ListSourceRowIndex) + 1;
            };
            gridView1.CustomUnboundColumnData += (sender, e) =>
            {
                GridView view = sender as GridView;
                if (e.Column.FieldName == "dd5" && e.IsGetData)
                    e.Value = view.GetRowHandle(e.ListSourceRowIndex) + 1;
            };
            gridView2.CustomUnboundColumnData += (sender, e) =>
            {
                GridView view = sender as GridView;
                if (e.Column.FieldName == "dd5" && e.IsGetData)
                    e.Value = view.GetRowHandle(e.ListSourceRowIndex) + 1;
            };
        }
        dataSetFill ds = new dataSetFill();
        BaseUrl Url = new BaseUrl();
        baseinfo userInfo = new baseinfo(UserSession.LoggedUserID);
        // ===== API models =====
        private class ApiFileEntry
        {
            public string name = string.Empty;          
            public string url = string.Empty;           
            public string type = string.Empty;          
            public string ext = string.Empty;          
            public long size = 0;                       
            public long bytes = 0;                      
            public string modified = string.Empty;      

            // Туслагч: аль нь ирсэн байна, тэрийг нь ашиглая
            public long SizeBytes
            {
                get { return (size > 0 ? size : bytes); }
            }
        }

        private class ApiFileList   // success/int
        {
            public int success = 0;                            
            public string error = string.Empty;                
            public List<ApiFileEntry> files = new List<ApiFileEntry>();  
        }

        private class ApiFileList2  // ok/bool
        {
            public bool ok = false;                             
            public string error = string.Empty;                 
            public List<ApiFileEntry> files = new List<ApiFileEntry>(); 
        }

        // ===== Файлын жагсаалт татах =====
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
                    // TLS 1.2 (хуучин Windows дээр хэрэгтэй)
                    ServicePointManager.Expect100Continue = true;
                    ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;

                    wc.Encoding = Encoding.UTF8;
                    string json = wc.DownloadString(api);

                    // Хэрэв сервер “nodata” гэж буцаадаг бол:
                    if (json.Trim().Equals("nodata", StringComparison.OrdinalIgnoreCase))
                        return new List<ApiFileEntry>();

                    // API URL ба JSON-ий эхний хэсгийг харъя
                     //MessageBox.Show("API:\n" + api + "\n\nJSON:\n" + (json.Length > 400 ? json.Substring(0,400) + "..." : json));

                    var js = new System.Web.Script.Serialization.JavaScriptSerializer();
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


        public void ita_Load(object sender, EventArgs e)
        {
            try
            {
                gridControl5.DataSource = ds.gridFill("getita", "itatype=ITA");
                gridControl1.DataSource = ds.gridFill("getita", "itatype=MA");
                gridControl2.DataSource = ds.gridFill("getita", "itatype=OP");
                dateEdit1.EditValue = DateTime.Now;
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.ToString());
            }
            finally { }
        }
        private void hai()
        {
            gridView5.ActiveFilterString = "ner like '%" + ner.Text + "%' and proff like '%" + proff.Text + "%' and phone like '" + phone.Text + "%' and comID = '1'";
        }
        private void textEdit1_EditValueChanged(object sender, EventArgs e)
        {
            hai();
        }
        private void textEdit2_EditValueChanged(object sender, EventArgs e)
        {
            hai();
        }
        private void textEdit3_EditValueChanged(object sender, EventArgs e)
        {
            hai();
        }
        private void simpleButton3_Click(object sender, EventArgs e)
        {
            addita ad = new addita(this);
            if (xtraTabControl1.SelectedTabPageIndex == 0)
            {
                //ad.projectID.Text = "0";
                ad.doctype.Text = "ITA";
                ad.Text = "Инженер техникийн ажилтан нэмэх";
                
            }
            if (xtraTabControl1.SelectedTabPageIndex == 1)
            {
                //ad.projectID.Text = "0";
                ad.doctype.Text = "MA";
                ad.Text = "Мэргэжилтэй ажилтан нэмэх";
                
            }
            if (xtraTabControl1.SelectedTabPageIndex == 2)
            {
                //ad.projectID.Text = "0";
                ad.doctype.Text = "OP";
                ad.Text = "Тоног төхөөрөмж дээр ажиллах ажилтан нэмэх";
               
            }
            ad.ShowDialog();
        }
        private void устгахToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                dataSetFill dc = new dataSetFill();
                if (xtraTabControl1.SelectedTabPageIndex == 0)
                {
                    string id = gridView5.GetFocusedRowCellValue("id").ToString();
                    DialogResult dr = MessageBox.Show("'" + gridView5.GetFocusedRowCellValue("ner").ToString() + "' ИТА-н мэдээллийг утсгах уу?", "Анхаар", MessageBoxButtons.YesNo);
                    if (dr == System.Windows.Forms.DialogResult.Yes)
                    {
                        var data = new NameValueCollection();
                        data["deleteid"] = id;
                        data["ali_table"] = "ita";
                        MessageBox.Show(dc.exec_command("deleteAll", data));
                    }
                }

                if (xtraTabControl1.SelectedTabPageIndex == 1)
                {
                    string id = gridView1.GetFocusedRowCellValue("id").ToString();
                    DialogResult dr = MessageBox.Show("'" + gridView1.GetFocusedRowCellValue("ner").ToString() + "' Мэргэжилтэй ажилтаны мэдээллийг утсгах уу?", "Анхаар", MessageBoxButtons.YesNo);
                    if (dr == System.Windows.Forms.DialogResult.Yes)
                    {
                        var data = new NameValueCollection();
                        data["deleteid"] = id;
                        data["ali_table"] = "ita";
                        MessageBox.Show(dc.exec_command("deleteAll", data));
                    }
                }
                if (xtraTabControl1.SelectedTabPageIndex == 2)
                {
                    string id = gridView2.GetFocusedRowCellValue("id").ToString();
                    DialogResult dr = MessageBox.Show("'" + gridView2.GetFocusedRowCellValue("ner").ToString() + "' Машин механизмийн операторчины мэдээллийг утсгах уу?", "Анхаар", MessageBoxButtons.YesNo);
                    if (dr == System.Windows.Forms.DialogResult.Yes)
                    {
                        var data = new NameValueCollection();
                        data["deleteid"] = id;
                        data["ali_table"] = "ita";
                        MessageBox.Show(dc.exec_command("deleteAll", data));
                    }
                }
             }
            catch (Exception ee)
            {
                MessageBox.Show(ee.ToString());
            }
            finally 
            { 
                ita_Load(sender, e); 
            }
        }
        private void засахToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                editita eita = new editita(this);
                if (xtraTabControl1.SelectedTabPageIndex == 0)
                {
                    eita.itaID.Text = gridView5.GetFocusedRowCellValue("id").ToString().Trim();
                    eita.ovog.Text = gridView5.GetFocusedRowCellValue("ovog").ToString();
                    eita.ner.Text = gridView5.GetFocusedRowCellValue("ner").ToString();
                    eita.atushaal.Text = gridView5.GetFocusedRowCellValue("atushaal").ToString();
                    eita.proff.Text = gridView5.GetFocusedRowCellValue("proff").ToString();
                    eita.phone.Text = gridView5.GetFocusedRowCellValue("phone").ToString();
                    eita.email.Text = gridView5.GetFocusedRowCellValue("email").ToString();
                    eita.school.Text = gridView5.GetFocusedRowCellValue("school").ToString();
                    eita.zereg.Text = gridView5.GetFocusedRowCellValue("zereg").ToString();
                    eita.ajillsan.Text = gridView5.GetFocusedRowCellValue("ajillsan").ToString();
                    eita.niitAjilsan.Text = gridView5.GetFocusedRowCellValue("niitAjilsan").ToString();
                    eita.doctype.Text = gridView5.GetFocusedRowCellValue("itatype").ToString();
                }
                if (xtraTabControl1.SelectedTabPageIndex == 1)
                {
                    eita.itaID.Text = gridView1.GetFocusedRowCellValue("id").ToString();
                    eita.ovog.Text = gridView1.GetFocusedRowCellValue("ovog").ToString();
                    eita.ner.Text = gridView1.GetFocusedRowCellValue("ner").ToString();
                    eita.atushaal.Text = gridView1.GetFocusedRowCellValue("atushaal").ToString();
                    eita.proff.Text = gridView1.GetFocusedRowCellValue("proff").ToString();
                    eita.phone.Text = gridView1.GetFocusedRowCellValue("phone").ToString();
                    eita.email.Text = gridView1.GetFocusedRowCellValue("email").ToString();
                    eita.school.Text = gridView1.GetFocusedRowCellValue("school").ToString();
                    eita.zereg.Text = gridView1.GetFocusedRowCellValue("zereg").ToString();
                    eita.ajillsan.Text = gridView1.GetFocusedRowCellValue("ajillsan").ToString();
                    eita.niitAjilsan.Text = gridView1.GetFocusedRowCellValue("niitAjilsan").ToString();
                    eita.doctype.Text = gridView1.GetFocusedRowCellValue("itatype").ToString();
                }
                if (xtraTabControl1.SelectedTabPageIndex == 2)
                {
                    eita.itaID.Text = gridView2.GetFocusedRowCellValue("id").ToString();
                    eita.ovog.Text = gridView2.GetFocusedRowCellValue("ovog").ToString();
                    eita.ner.Text = gridView2.GetFocusedRowCellValue("ner").ToString();
                    eita.atushaal.Text = gridView2.GetFocusedRowCellValue("atushaal").ToString();
                    eita.proff.Text = gridView2.GetFocusedRowCellValue("proff").ToString();
                    eita.phone.Text = gridView2.GetFocusedRowCellValue("phone").ToString();
                    eita.email.Text = gridView2.GetFocusedRowCellValue("email").ToString();
                    eita.school.Text = gridView2.GetFocusedRowCellValue("school").ToString();
                    eita.zereg.Text = gridView2.GetFocusedRowCellValue("zereg").ToString();
                    eita.ajillsan.Text = gridView2.GetFocusedRowCellValue("ajillsan").ToString();
                    eita.niitAjilsan.Text = gridView2.GetFocusedRowCellValue("niitAjilsan").ToString();
                    eita.doctype.Text = gridView2.GetFocusedRowCellValue("itatype").ToString();
                } 
                
                eita.ShowDialog();
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.ToString());
            }
            finally { }
        }

        // MA (gridControl1)
        private void gridControl1_DoubleClick(object sender, EventArgs e)
        {
            var idObj = gridView1.GetFocusedRowCellValue("id");
            if (idObj == null) return;
            int itaId; if (!int.TryParse(idObj.ToString(), out itaId)) return;

            string folderUrl = Url.GetUrl() + "dist/uploads/ita/docu/MA/" + itaId + "/";
            var files = FetchFileList(folderUrl, "pdf");

            using (var dlg = new ItaFilesForm(
                "МА файлууд — ID: " + itaId,
                folderUrl,
                files,
                () => FetchFileList(folderUrl, "pdf"),
                itaId,
                "itadocMA"                 // ← ЧУХАЛ
            ))
            {
                dlg.ShowDialog(this);
            }
        }

        // OP (gridControl2)
        private void gridControl2_DoubleClick(object sender, EventArgs e)
        {
            var idObj = gridView2.GetFocusedRowCellValue("id");
            if (idObj == null) return;
            int itaId; if (!int.TryParse(idObj.ToString(), out itaId)) return;

            string folderUrl = Url.GetUrl() + "dist/uploads/ita/docu/OP/" + itaId + "/";
            var files = FetchFileList(folderUrl, "pdf");

            using (var dlg = new ItaFilesForm(
                "Оператор файлууд — ID: " + itaId,
                folderUrl,
                files,
                () => FetchFileList(folderUrl, "pdf"),
                itaId,
                "itadocOP"                 // ← ЧУХАЛ
            ))
            {
                dlg.ShowDialog(this);
            }
        }

        private void gridControl5_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                var idObj = gridView5.GetFocusedRowCellValue("id");
                if (idObj == null) return;

                int itaId;
                if (!int.TryParse(idObj.ToString(), out itaId)) return;

                string folderUrl = Url.GetUrl() + "dist/uploads/ita/docu/ITA/" + itaId + "/";

                // Файлын жагсаалтыг татна (бүгдийг харуулах бол "*", зөвхөн PDF бол "pdf")
                var files = FetchFileList(folderUrl, "pdf");
                
                using (var dlg = new ItaFilesForm("ITA файлууд — ID: " + itaId, folderUrl, files, () => FetchFileList(folderUrl, "pdf")))
                {
                    dlg.ShowDialog(this);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Алдаа: " + ex.Message);
            }
        }

        private void gridControl1_Click(object sender, EventArgs e)
        {
            // GridControl дээр дарахад хийгдэх үйлдлийг энд бичнэ үү
        }
        private void panelControl6_Paint(object sender, PaintEventArgs e)
        {
            // Your custom painting code
        }
        private void simpleButton2_Click(object sender, EventArgs e)
        {
            if (xtraTabControl1.SelectedTabPageIndex == 0)
            {
                PrintGridview.Print(
                gridView5,
                20, 15, 15, 10,  // Margin-ууд
                xtraTabPage5.Text,
                "",
                userInfo.comName,     // Header хэсэг
                "Хэвлэсэн:" + userInfo.userName,
                DateTime.Now.ToString("yyyy-MM-dd"), // Footer хэсэг
                true // Landscape чиглэл
                );
            }
            if (xtraTabControl1.SelectedTabPageIndex == 1)
            {
                PrintGridview.Print(
                gridView1,
                20, 15, 15, 10,  // Margin-ууд
                xtraTabPage1.Text,
                "",
                userInfo.comName,     // Header хэсэг
                "Хэвлэсэн:" + userInfo.userName,
                DateTime.Now.ToString("yyyy-MM-dd"), // Footer хэсэг
                true // Landscape чиглэл
                );
            }
            if (xtraTabControl1.SelectedTabPageIndex == 2)
            {
                PrintGridview.Print(
                gridView2,
                20, 15, 15, 10,  // Margin-ууд
                xtraTabPage2.Text,
                "",
                userInfo.comName,     // Header хэсэг
                "Хэвлэсэн:" + userInfo.userName,
                DateTime.Now.ToString("yyyy-MM-dd"), // Footer хэсэг
                true // Landscape чиглэл
                );
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {

        }


        // === ITA файлын жагсаалт/preview жижиг цонх ===
        // === ITA файлын жагсаалт/preview + ADD/DELETE ===
        // === ITA файлын жагсаалт/preview + ADD/DELETE ===
        private class ItaFilesForm : XtraForm
        {
            // --- fields ---
            private readonly string _folderUrl;                  // .../dist/uploads/ita/docu/{ITA|MA|OP}/{itaId}/
            private readonly Func<List<ApiFileEntry>> _reload;   // дахин ачаалах функц
            private readonly int _itaId;                         // тухайн ажилтны ID
            private readonly string _type;                       // "itadoc" | "itadocMA" | "itadocOP"  (upload-д ашиглагдана)
            private List<ApiFileEntry> _files;

            private DevExpress.XtraEditors.ListBoxControl list;
            private DevExpress.XtraEditors.SimpleButton btnAdd, btnDelete, btnRefresh, btnClose;

            // --- 4 аргументтэй (ITA-д хэрэглэнэ) ---
            public ItaFilesForm(string title, string folderUrl, List<ApiFileEntry> files, Func<List<ApiFileEntry>> reload)
            {
                _folderUrl = folderUrl.EndsWith("/") ? folderUrl : (folderUrl + "/");
                _files = files ?? new List<ApiFileEntry>();
                _reload = reload;
                _itaId = ParseItaIdFromFolderUrl(_folderUrl);
                _type = "itadoc"; // ITA default

                BuildUi(title);
                FillList();
            }

            // --- 6 аргументтэй (MA/OP-д хэрэглэнэ) ---
            public ItaFilesForm(
                string title,
                string folderUrl,
                List<ApiFileEntry> files,
                Func<List<ApiFileEntry>> reload,
                int itaId,
                string uploadType // "itadocMA" эсвэл "itadocOP"
            )
                : this(title, folderUrl, files, reload)
            {
                _itaId = itaId;
                _type = string.IsNullOrEmpty(uploadType) ? "itadoc" : uploadType;
            }

            // ---------------- UI ----------------
            private void BuildUi(string title)
            {
                Text = title;
                Width = 720; Height = 520;
                StartPosition = FormStartPosition.CenterParent;

                list = new DevExpress.XtraEditors.ListBoxControl { Dock = DockStyle.Fill };
                list.DoubleClick += (s, e) => OpenSelected();                     // 2-click → нээх
                list.KeyDown += (s, e) => { if (e.KeyCode == Keys.Enter) { e.Handled = true; OpenSelected(); } };

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

                Controls.Add(list);
                Controls.Add(pnl);

                CancelButton = btnClose;
            }

            // ---------------- Туслахууд ----------------
            private int ParseItaIdFromFolderUrl(string url)
            {
                try
                {
                    Uri u = new Uri(url);
                    string[] seg = u.AbsolutePath.Trim('/').Split('/');
                    // ... dist / uploads / ita / docu / {ITA|MA|OP} / {id}
                    if (seg.Length >= 6)
                    {
                        int id;
                        if (int.TryParse(seg[seg.Length - 1], out id)) return id;
                    }
                }
                catch { }
                return 0;
            }

            private Uri ApiUri(string relative)  // ж: "api/fileupload.php"
            {
                // _folderUrl-аас домэйн авч /api/... руу заана
                Uri baseUri = new Uri(_folderUrl);
                return new Uri(baseUri, "/" + relative.TrimStart('/'));
            }

            private class Lbi
            {
                public string Text { get; set; }
                public ApiFileEntry File { get; set; }
            }

            // ---------------- Жагсаалт ----------------
            private void FillList()
            {
                list.DataSource = null;
                list.Items.Clear();

                var view = new List<Lbi>();
                int n = 1;
                foreach (var f in _files)
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

            // ---------------- Файл нэмэх ----------------
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
                                // UploadFile өөрөө multipart/form-data болгодог тул Content-Type тохируулах хэрэггүй
                                Uri up = ApiUri("api/fileupload.php?itaID=" + _itaId + "&id=" + _type);
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

            // ---------------- Файл устгах ----------------
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

                    // deletefile.php → id=ITA|MA|OP  (UPLOAD type-оос хөрвүүлнэ)
                    string idParam = "ITA";
                    if (string.Equals(_type, "itadocMA", StringComparison.OrdinalIgnoreCase)) idParam = "MA";
                    else if (string.Equals(_type, "itadocOP", StringComparison.OrdinalIgnoreCase)) idParam = "OP";

                    using (var wc = new WebClient())
                    {
                        wc.Encoding = Encoding.UTF8;
                        // GET: /api/deletefile.php?id=MA&itaID=12&name=abc.pdf
                        string baseUrl = ApiUri("api/deletefile.php").ToString();
                        string url = baseUrl
                            + "?id=" + Uri.EscapeDataString(idParam)
                            + "&itaID=" + _itaId
                            + "&name=" + Uri.EscapeDataString(entry.name);

                        string resp = wc.DownloadString(url);
                        // амжилтын энгийн шалгалт
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
