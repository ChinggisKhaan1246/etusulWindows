using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;
using System.Net;
using System.IO;
using System.Collections.Specialized;
using Newtonsoft.Json;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.LookAndFeel;
using DevExpress.XtraEditors;
using DevExpress.XtraPrinting;
using DevExpress.XtraPrintingLinks;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.Utils;
using Microsoft.Win32;
namespace ST
{
    public partial class ildaldact : Form
    {
       
        public ildaldact()
        {
          
            InitializeComponent();
            gridView1.CustomUnboundColumnData += (sender, e) =>
            {
                GridView view = sender as GridView;
                if (e.Column.FieldName == "dd" && e.IsGetData)
                    e.Value = view.GetRowHandle(e.ListSourceRowIndex) + 1;
            };

            gridView2.CustomUnboundColumnData += (sender, e) =>
            {
                GridView view = sender as GridView;
                if (e.Column.FieldName == "dd2" && e.IsGetData)
                    e.Value = view.GetRowHandle(e.ListSourceRowIndex) + 1;
            };
        }

        dataSetFill dc = new dataSetFill();

        private void aguulax_Load(object sender, EventArgs e)
        {
            try
            {
                var parameters = new Dictionary<string, string> { { "status", "filter" }, { "comID", UserSession.LoggedComID.ToString() } };
                var projectData = dsn.getData("getproject", parameters);

                if (projectData != null && projectData.Rows.Count != 0)
                {
                    projectnameFilter.Properties.DataSource = projectData;
                    projectnameFilter.Properties.ValueMember = "projectID";
                    projectnameFilter.Properties.DisplayMember = "projectName";

                    int rowCount = projectData.Rows.Count;
                    projectnameFilter.Properties.DropDownRows = rowCount + 1;

                    projectnameFilter.Properties.Columns.Clear();
                    projectnameFilter.Properties.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("projectName", "Төслийн нэр"));
                }
                else
                {
                    MessageBox.Show("Идэвхтэй төсөл олдсонгүй.", "Анхааруулга", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Алдаа: Төслийн жагсаалт үүсгэхэд алдаа гарлаа.");
            }
        }

        baseinfo userInfo = new baseinfo(UserSession.LoggedUserID);
        dataSetFill dsaz = new dataSetFill();
        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            try
            {
                bookID.Text = gridView1.GetFocusedRowCellValue("id").ToString();
                addactbefore addaB = new addactbefore(this);
                addaB.projectName.Text = projectName.Text;
                addaB.projectID.Text = projectID.Text;
                addaB.bookID.Text = bookID.Text;
                addaB.ognoo.DateTime = DateTime.Now;
                addaB.actname.Text = gridView1.GetFocusedRowCellValue("actname").ToString();
                addaB.rtffilename.Text = gridView1.GetFocusedRowCellValue("rtffile").ToString();
                addaB.companyname.Text = userInfo.comName;
                addaB.aimag.Text = aimag.Text; 
                addaB.sumname.Text = sumname.Text;  
                addaB.zahorg.Text = zahorg.Text; 
                addaB.zohorg.Text = zohorg.Text;  
                addaB.ShowDialog();
            }
            catch (Exception)
            {
                MessageBox.Show("АКТ бэлдэх үед алдаа гарлаа.");
            }

            
        }
        dataSetFill ds = new dataSetFill();
        dataSetFillnew dsn = new dataSetFillnew();
        public void gridControl1_Click(object sender, EventArgs e)
        {
            if (gridView1.GetFocusedRowCellValue("id") != null && projectID.Text.Trim() != null)
            {
                bookID.Text = gridView1.GetFocusedRowCellValue("id").ToString();
                grid2_refresh();
            }
            else
            {
                MessageBox.Show("Та мөр сонгоогүй байна!!!", "Анхааруулга", MessageBoxButtons.OK, MessageBoxIcon.Warning);// энэ ажиллаад байн ауу
            }
        }
        BaseUrl Url = new BaseUrl();
        public void grid2_refresh() {
            try
            {
                if (bookID.Text != null)
                {
                    using (WebClient client = new WebClient())
                    {
                        NameValueCollection values = new NameValueCollection();
                        values["bookID"] = bookID.Text.Trim(); 
                        values["projectID"] = projectID.Text.Trim(); 

                        byte[] response = client.UploadValues(Url.GetUrl()+"api/getactdata.php", "POST", values);
                        string responseText = Encoding.UTF8.GetString(response);

                        Dictionary<string, object> jsonResponse = JsonConvert.DeserializeObject<Dictionary<string, object>>(responseText);

                        if (jsonResponse.ContainsKey("status") && jsonResponse["status"].ToString() == "success")
                        {
                            List<Dictionary<string, object>> dataList = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(jsonResponse["data"].ToString());
                            DataTable dt = new DataTable();

                            if (dataList.Count > 0)
                            {
                                foreach (string key in dataList[0].Keys)
                                {
                                    dt.Columns.Add(key);
                                }
                            }

                             foreach (Dictionary<string, object> row in dataList)
                            {
                                DataRow dr = dt.NewRow();
                                foreach (string key in row.Keys)
                                {
                                    if (key == "actdata" && row[key] != null)
                                    {
                                        if (row[key] is Newtonsoft.Json.Linq.JObject)
                                        {
                                            dr[key] = JsonConvert.SerializeObject(row[key]); // JSON объектыг string болгох
                                        }
                                        else
                                        {
                                            dr[key] = row[key].ToString();
                                        }
                                    }
                                    else
                                    {
                                        if (row[key] != null)
                                        {
                                            dr[key] = row[key].ToString(); //
                                        }
                                        else
                                        {
                                            dr[key] = ""; // 
                                        }
                                    }
                                }
                                dt.Rows.Add(dr);
                            }

                            gridControl2.DataSource = dt; // 
                        }
                        else
                        {
                             gridControl2.DataSource = null;
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Та мөр сонгоогүй байна!!!", "Анхааруулга", MessageBoxButtons.OK, MessageBoxIcon.Warning);// энэ ажиллаад байн ауу
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Алдаа гарлаа: " + ex.Message, "Алдаа", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void LoadActDataToRichEditControl()
        {
            try
            {
                editact edda = new editact(this);
                if (gridView2.GetFocusedRowCellValue("actdata") != null)
                {
                    string actData = gridView2.GetFocusedRowCellValue("actdata").ToString();
                    if (actData.StartsWith("{") || actData.StartsWith("["))
                    {
                        var jsonData = JsonConvert.DeserializeObject<Dictionary<string, object>>(actData);
                        if (jsonData.ContainsKey("rtfContent"))
                        {
                            string rtfContent = jsonData["rtfContent"].ToString();

                            using (MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(rtfContent)))
                            {

                                edda.richEditControl1.LoadDocument(ms, DevExpress.XtraRichEdit.DocumentFormat.Rtf);
                                edda.actIDEdit.Text = gridView2.GetFocusedRowCellValue("id").ToString();
                                edda.actnamefromuser.Text = gridView2.GetFocusedRowCellValue("actnamefromuser").ToString();
                                edda.ShowDialog();
                            }
                        }
                        else
                        {
                            MessageBox.Show("RTF өгөгдөл олдсонгүй!", "Анхааруулга", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                    else
                    {
                        using (MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(actData)))
                        {
                            edda.richEditControl1.LoadDocument(ms, DevExpress.XtraRichEdit.DocumentFormat.Rtf);
                            edda.actIDEdit.Text = gridView2.GetFocusedRowCellValue("id").ToString();
                            edda.ShowDialog();
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Та мөр сонгоогүй байна!", "Анхааруулга", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Алдаа гарлаа: " + ex.Message, "Алдаа", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void gridControl2_DoubleClick(object sender, EventArgs e)
        {
            LoadActDataToRichEditControl();
        }


        private void textEdit1_EditValueChanged(object sender, EventArgs e)
        {
            gridView1.ActiveFilterString = "actname LIKE '%" + textEdit1.Text + "%'";
        }

        private void усгтгахToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                dataSetFill dc = new dataSetFill();
                string id = gridView2.GetFocusedRowCellValue("id").ToString();
                DialogResult ds = MessageBox.Show(id + " - кодтой aкт-ийн мэдээллийг устгахдаа итгэлтэй байна уу.", "Анхаар", MessageBoxButtons.YesNo);
                if (ds == System.Windows.Forms.DialogResult.Yes)
                {
                    var data = new NameValueCollection();
                    data["deleteid"] = id.Trim();
                    MessageBox.Show(dc.exec_command("deleteactdata", data));
                    grid2_refresh();
                }
            }
            catch (Exception ee)
            { MessageBox.Show(ee.ToString()); }
            finally { }
        }

        private void contextMenuStrip2_MouseDown(object sender, MouseEventArgs e)
        {
          if (e.Button == MouseButtons.Right)
             {
                var hitInfo = gridView1.CalcHitInfo(e.Location);
                  if (hitInfo.RowHandle >= 0)
                     {
                        bookID.Text = gridView1.GetRowCellValue(hitInfo.RowHandle, "id").ToString();
                        contextMenuStrip1.Show(gridControl1, e.Location);
                     }
             }
        }

        private void засахToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadActDataToRichEditControl();
        }

        private void хэвлэхToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadActDataToRichEditControl();
        }

        private void projectnameFilter_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
               
                gridView2.ActiveFilterString = "projectName LIKE '%" + projectName.Text + "%'";
                    if (projectnameFilter.EditValue != null)
                    {
                        DataRowView selectedRow = projectnameFilter.GetSelectedDataRow() as DataRowView;
                        if (selectedRow != null)
                        {
                            aimag.Text          = selectedRow["aimag"].ToString();
                            sumname.Text        = selectedRow["sumname"].ToString();
                            projectName.Text    = selectedRow["projectName"].ToString();
                            projectID.Text      = selectedRow["projectID"].ToString();
                            zahorg.Text         = selectedRow["hyanagch"].ToString();
                            zohorg.Text         = selectedRow["author"].ToString();
                        }
                    }
                    try
                    {
                        var DT = dc.gridFill("getbookact", "projectID=" + projectID.Text.Trim());
                        gridControl1.DataSource = DT;
                        gridView1.Columns["dd"].Width = 35;
                        gridView2.Columns["dd2"].Width = 35;

                    }
                    catch (Exception ee)
                    {
                        MessageBox.Show("aldaa", ee.ToString());
                    }
                
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.ToString());
            }
        }

        private void projectName_EditValueChanged(object sender, EventArgs e)
        {
            projectnameFilter_EditValueChanged(projectnameFilter, EventArgs.Empty);
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

       

        private void гарынҮсэгToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int projectId;
            if (!string.IsNullOrWhiteSpace(projectID.Text) && int.TryParse(projectID.Text.Trim(), out projectId))

            {
                try
                {
                    signatures sings = new signatures();
                    sings.projectID.Text = projectID.Text;
                    sings.ShowDialog();

                }
                catch (Exception ee)
                {
                    MessageBox.Show("Алдаа:", ee.ToString());
                }

            }
            else
            {
                MessageBox.Show("Гарын үсэг бэлдэх төслийг сонгоогүй байна.");
            }

        }

        private void contextMenuStrip2_Opening(object sender, CancelEventArgs e)
        {
            if (gridView1.RowCount == 0)
            {
                e.Cancel = true; 
            }
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            if (gridView2.RowCount == 0)
            {
                e.Cancel = true; 
            }
        }

        private void textEdit1_EditValueChanged_1(object sender, EventArgs e)
        {
            try
            {
                gridView1.ActiveFilterString = "actname LIKE '%" + textEdit1.Text + "%'";
            }
            catch (Exception)
            {
                MessageBox.Show("Өгөгдөл байхгүй.");
            }
        }

       

      
    }
}
