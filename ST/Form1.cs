using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Collections.Specialized;
using System.Windows.Forms;
using System.Security.Cryptography;
using System.Net.NetworkInformation;
using System.Management;
using System.Diagnostics;
using System.Web;
using System.IO;
using System.Net;
using System.Drawing.Printing;
using DevExpress.LookAndFeel;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraPrinting;
using DevExpress.XtraPrinting.Control;
using DevExpress.XtraPrintingLinks;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.Utils;
using DevExpress.XtraReports.UI;
using DevExpress.XtraGrid.Views.Grid;
using Newtonsoft.Json;
using ICSharpCode.SharpZipLib.Zip;




namespace ST
{
    public partial class Form1 : Form
    {
        public static string companyName;
        public Form1()
        {
            InitializeComponent();
           


            gridView1.CustomUnboundColumnData += (sender, e) =>
            {
                GridView view = sender as GridView;
                if (e.Column.FieldName == "dd" && e.IsGetData)
                    e.Value = view.GetRowHandle(e.ListSourceRowIndex) + 1;
            };

            gridView3.CustomUnboundColumnData += (sender, e) =>
            {
                GridView view = sender as GridView;
                if (e.Column.FieldName == "dd3" && e.IsGetData)
                    e.Value = view.GetRowHandle(e.ListSourceRowIndex) + 1;
            };
            gridView2.CustomUnboundColumnData += (sender, e) =>
            {
                GridView view = sender as GridView;
                if (e.Column.FieldName == "dd2" && e.IsGetData)
                    e.Value = view.GetRowHandle(e.ListSourceRowIndex) + 1;
            };

            gridView5.CustomUnboundColumnData += (sender, e) =>
            {
                GridView view = sender as GridView;
                if (e.Column.FieldName == "dd5" && e.IsGetData)
                    e.Value = view.GetRowHandle(e.ListSourceRowIndex) + 1;
            };

            gridView4.CustomUnboundColumnData += (sender, e) =>
            {
                GridView view = sender as GridView;
                if (e.Column.FieldName == "dd4" && e.IsGetData)
                    e.Value = view.GetRowHandle(e.ListSourceRowIndex) + 1;
            };
            

        }
         dataSetFill ds = new dataSetFill();
        
        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                companyName = comName.Text;
                FillGridOdoo();
                FillGridDuussan();
                xtraTabControl1.AppearancePage.Header.Font = new Font("Tahoma", 8); // Фонт тохируулах
                xtraTabControl1.AppearancePage.Header.Options.UseFont = true;
                xtraTabControl1.Height = 55; // Табын өндөр тогтмол 33
                if (gridView1.RowCount == 0)
                {
                    MessageBox.Show("Одоо идэвхтэй хэрэгжиж байгаа төсөл арга хэмжээ байхгүй байна.");
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.ToString());
            }
            finally { }
        }
        private void haihIncome()
        {
            try
            {
               gridView3.ActiveFilterString = "incomename LIKE '%" + textEdit1.Text + "%'  or income LIKE '%" + textEdit1.Text + "%'  or ognoo LIKE '%" + textEdit1.Text + "%'";
            }
            catch (Exception)
            {
                MessageBox.Show("Орлого тал дээр өгөгдөл байхгүй.");
            }
            finally { }
        }

        private void haihCost()
        {
            try
            {
                gridView2.ActiveFilterString = "costname LIKE '%" + textEdit2.Text + "%'  or cost LIKE '%" + textEdit2.Text + "%'  or ognoo LIKE '%" + textEdit2.Text + "%'";
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.ToString());
            }
            finally { }
        }
        private void haihDocument()
        {
            try
            {
                 gridView4.ActiveFilterString = "docname LIKE '%" + textEdit3.Text + "%' or URL LIKE '%" + textEdit3.Text + "%'  or tailbar LIKE '%" + textEdit3.Text + "%'";
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.ToString());
            }
            finally { }
        }

        public void saveLogg(string saler_id, string events)
            {
                try
                {
                    dataSetFill ds = new dataSetFill();
                    var data = new NameValueCollection();
                    data["userID"] = saler_id;
                    data["events"] = events;
                    data["ognoo"] = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
                    ds.exec_command("Addlogg", data);
                }
                catch (Exception ee)
                {
                    MessageBox.Show(ee.ToString());
                }
           
            }

        private void bune_EditValueChanged(object sender, EventArgs e)
        {
            
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            try
            {
                cost z = new cost(this);
                z.projectName.Text = gridView1.GetFocusedRowCellValue("projectName").ToString().Trim();
                z.projectID.Text = gridView1.GetFocusedRowCellValue("projectID").ToString().Trim();
                z.ShowDialog();
            }
            catch (Exception ee)
            { MessageBox.Show("Алдаа" + ee.ToString(), ""); }
            finally { }
        }

        private void label6_TextChanged(object sender, EventArgs e)
        {
            try
            {
                gridView1.ExpandAllGroups();
                gridView5.ExpandAllGroups();
                Form1_Load(e, null);
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.ToString());
            }
            finally 
            {
 
            }
        }

        private void gridView1_RowStyle(object sender, RowStyleEventArgs e)
        {
            GridView g = sender as GridView;
            if(e.RowHandle > 0)
            {
                string pro = g.GetRowCellDisplayText(e.RowHandle, g.Columns["too"]);
                if (Convert.ToInt32(pro) < 5 && Convert.ToInt32(pro) > 2)
                {
                    e.Appearance.BackColor = Color.White;
                    e.Appearance.BackColor2 = Color.LightSkyBlue;
                    
                }

                else if(Convert.ToInt32(pro) < 3)
                {
                    e.Appearance.BackColor = Color.White;
                    e.Appearance.BackColor2 = Color.FromArgb(150, Color.LightCoral);
                }

            }
        }

        private void simpleButton6_Click(object sender, EventArgs e)
        {
             gridView1.ShowPrintPreview();
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
          

        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void textEdit2_EditValueChanged(object sender, EventArgs e)
        {
            haihCost();
        }

        private void checkButton1_CheckedChanged(object sender, EventArgs e)
        {
 
        }

        private void simpleButton7_Click(object sender, EventArgs e)
        {
            Ttext1.Text = "";
            Ttext2.Text = "";
            
            gridView1.ActiveFilterString = "";
        }
        private void simpleButton11_Click(object sender, EventArgs e)
        {
            //cardTable.Clear();
            label6.Text += "1";
        }

        private void simpleButton12_Click(object sender, EventArgs e)
        {
            gridView2.DeleteSelectedRows();
        }
       private void simpleButton12_Click_1(object sender, EventArgs e)
        {
 
        }

        private void simpleButton11_Click_1(object sender, EventArgs e)
        {
 
        }
        public string DataTableToJSON(DataTable table)
        {
            string JSONString = string.Empty;
            JSONString = JsonConvert.SerializeObject(table);
            return JSONString;
        } 
        private void simpleButton10_Click_2(object sender, EventArgs e)
        {
 
        }
        public void insertCard(string turul) 
        {
            var data = new NameValueCollection();
            if (gridView2.RowCount > 0)
            {
                label6.Text = label6.Text+"1";
            }
            else 
            { 
                MessageBox.Show("Сагс хоосон байна"); 
            }
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            try
            {
                if (gridView3.RowCount > 0)
                {
                dataSetFill dc = new dataSetFill();
                string id = gridView3.GetFocusedRowCellValue("id").ToString().Trim();
                DialogResult dr = MessageBox.Show("Орлогын мэдээллийг утсгах уу?", "Анхаар", MessageBoxButtons.YesNo);
                if (dr == System.Windows.Forms.DialogResult.Yes)
                {
                    var data = new NameValueCollection();
                    data["deleteid"] = id;
                    data["ali_table"] = "income";
                    dc.exec_command("deleteAll", data);
                    //f.saveLogg(f.salerID.Text, f.salerName.Text, "Агуулахын бүртгэлээс устгасан");
                }   
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.ToString());
            }
            finally 
            {
                var pro_id = Convert.ToInt16(projectID1.Text.Trim());
                FillGridIncome(pro_id);
            }
            
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            PrintBarimt();
            insertCard("дансаар");
            saveLogg(comName.Text, "Дансаар борлуулалт хийсэн");
            
        }
        private void зээлээрToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            Zform zz = new Zform(this);
            zz.ShowDialog();
        }
        public void PrintBarimt()
        {

        }
        public void simpleButton14_Click_1(object sender, EventArgs e)
        {

        }
        private void bname_EditValueChanged_2(object sender, EventArgs e)
        {
            try
            {
                if (xtraTabControl1.SelectedTabPageIndex == 0)
                {
                    gridView1.ActiveFilterString = "projectName LIKE '%" + bname.Text + "%'  or gereeNo LIKE '%" + bname.Text + "%'  or budget LIKE '%" + bname.Text + "%'";
                }
                if (xtraTabControl1.SelectedTabPageIndex == 1)
                {
                    gridView5.ActiveFilterString = "projectName LIKE '%" + bname.Text + "%'  or gereeNo LIKE '%" + bname.Text + "%'  or budget LIKE '%" + bname.Text + "%'";
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Өгөгдөл байхгүй.");
            }
        }

        private void bune_EditValueChanged_1(object sender, EventArgs e)
        {
            try
            {
                if (xtraTabControl1.SelectedTabPageIndex == 0)
                {
                    gridView1.ActiveFilterString = "ognooG Like '" + bune.Text + "%'";
                }
                if (xtraTabControl1.SelectedTabPageIndex == 1)
                {
                    gridView5.ActiveFilterString = "ognooG Like '" + bune.Text + "%'";
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Өгөгдөл байхгүй.");
            }
            
        }

        private void textEdit1_EditValueChanged_1(object sender, EventArgs e)
        {

        }

        public void simpleButton7_Click_1(object sender, EventArgs e)
        {
          
                
               
            
        }

        public void FillGridDuussan()
        {
            try
            {
                System.Threading.Thread.Sleep(110);
                DataTable DT = ds.gridFill("getproject", "status=4");

                if (DT != null && DT.Rows.Count > 0)
                {
                    gridControl5.DataSource = DT;
                    gridView5.FocusedRowHandle = 0;
                    gridView5.SelectRow(0);
                }
                else
                {
                    gridControl5.DataSource = new DataTable();
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.ToString());
            }
            finally { }

        }
        //dataSetFillnew dsn = new dataSetFillnew();
        public void FillGridOdoo()
        {
            try
            {
                DataTable DT = ds.gridFill("getproject", "status=9&comID="+UserSession.LoggedComID);
                if (DT != null && DT.Rows.Count > 0)
                {
                    gridControl1.DataSource = DT;
                    gridView1.FocusedRowHandle = 0;
                    gridView1.SelectRow(0);
                }
                else
                {
                    gridControl1.DataSource = new DataTable();
                }  
            }
            catch (Exception ee)
            {
                MessageBox.Show("Алдаа: FillGridOdoo функцээс " + ee.Message);
            }
        }
        public void FillGridCost(int proID)
        {
            try
            {
                System.Threading.Thread.Sleep(110);
                
                var DT = ds.gridFill("getcost", "projectID="+proID.ToString().Trim());

                if (DT == null || DT.Rows.Count == 0)
                {
                    gridControl2.DataSource = new DataTable();
                }
                else
                {
                    gridControl2.DataSource = DT;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Алдаа гарлаа: " + ex.Message, "Алдаа", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void FillGridIncome(int proID)
        {
            try
            {
                System.Threading.Thread.Sleep(110);
                
                DataTable incomeData = ds.gridFill("getIncome", "projectID="+proID.ToString());

                if (incomeData == null || incomeData.Rows.Count == 0)
                {
                    gridControl3.DataSource = new DataTable(); 
                }
                else
                {
                    gridControl3.DataSource = incomeData;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Алдаа гарлаа: " + ex.Message, "Алдаа", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void FillGridDocument(int proID)
        {
            try
            {
                System.Threading.Thread.Sleep(110);
                DataTable documentData = ds.gridFill("getdocument", "projectID="+proID.ToString()+"&comID="+UserSession.LoggedComID);

                if (documentData == null || documentData.Rows.Count == 0)
                {
                    gridControl4.DataSource = new DataTable(); // Хоосон DataTable оноох
                }
                else
                {
                    gridControl4.DataSource = documentData;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Алдаа гарлаа: " + ex.Message, "Алдаа", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void gridControl1_Click(object sender, EventArgs e)
        {
           
        }

        private void simpleButton15_Click(object sender, EventArgs e)
        {

        }

        private void AddGeree_Click(object sender, EventArgs e)
        {
            try
            {
                addproject b = new addproject(this);
                b.comID.Text = gridView1.GetFocusedRowCellValue("projectID").ToString();
                b.ShowDialog();
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.ToString());  //энэ ок болж байгаа tender-c addproject-дуудаад ашиглах гэхээр болохгүй байсан  тэгээд шинэ форм үзүсгэсэн
            }
        }

        private void deleteGeree_Click(object sender, EventArgs e)
        {
            try
            {
                dataSetFill dc = new dataSetFill();
                string id = gridView1.GetFocusedRowCellValue("projectID").ToString();
                DialogResult dr = MessageBox.Show("'"+gridView1.GetFocusedRowCellValue("projectName").ToString()+"' гэрээ утсгах уу?", "Анхаар", MessageBoxButtons.YesNo);
                if (dr == System.Windows.Forms.DialogResult.Yes)
                {
                    var data = new NameValueCollection();
                    data["deleteid"] = id;
                    MessageBox.Show(dc.exec_command("deleteproject", data));
                    
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.ToString());
            }
            finally 
            { 
                FillGridOdoo(); 
            }
        }

        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            try
            {
                dataSetFill dc = new dataSetFill();
                string id = gridView1.GetFocusedRowCellValue("projectID").ToString();
                DialogResult dr = MessageBox.Show("'" + gridView1.GetFocusedRowCellValue("projectName").ToString() + "' гэрээ хаах уу?", "Анхаар", MessageBoxButtons.YesNo);
                if (dr == System.Windows.Forms.DialogResult.Yes)
                {
                    var data = new NameValueCollection();
                    data["deleteid"] = id;
                    MessageBox.Show(dc.exec_command("closeproject", data));
                    //f.saveLogg(f.salerID.Text, f.salerName.Text, "Агуулахын бүртгэлээс устгасан");
                    
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.ToString());
            }
            finally 
            { 
                FillGridOdoo();
                FillGridDuussan();
            }
        }

        private void Addincome_Click(object sender, EventArgs e)
        {
            try
            {
                income i = new income(this);
                i.projectName.Text = gridView1.GetFocusedRowCellValue("projectName").ToString();
                i.projectID.Text = gridView1.GetFocusedRowCellValue("projectID").ToString();
                i.ShowDialog();
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.ToString());
            }
        }

        private void toolStripMenuItem4_Click_1(object sender, EventArgs e)
        {
            try
            {
                adddocument d = new adddocument(this);
                d.projectName.Text = gridView1.GetFocusedRowCellValue("projectName").ToString();
                d.projectID.Text = gridView1.GetFocusedRowCellValue("projectID").ToString();
                d.ShowDialog();
            }
            catch (Exception ee)
            { MessageBox.Show("Алдаа" + ee.ToString(), ""); }
            finally { }
        }

        private void PDFviewer()

        {
 
        }
        
        private void gridControl4_DoubleClick(object sender, EventArgs e)
        {

          try
            {
                var encode = gridView4.GetFocusedRowCellValue("URL").ToString().Replace(" ", "%20");
                string fileUrl = url.GetUrl() + "dist/uploads/documents/" + gridView4.GetFocusedRowCellValue("projectID").ToString().Trim() + "/" + encode;
                FileViewer viewer = new FileViewer(fileUrl);
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.ToString());
            }
            finally { }  
        }
       
        private void textEdit1_EditValueChanged(object sender, EventArgs e)
        {
            haihIncome();
        }

        private void textEdit2_EditValueChanged_2(object sender, EventArgs e)
        {
            
        }

        private void textEdit3_EditValueChanged(object sender, EventArgs e)
        {
            haihDocument();
        }

        private void simpleButton23_Click(object sender, EventArgs e)
        {
            gridView2.ShowPrintPreview();
        }

        private void simpleButton19_Click(object sender, EventArgs e)
        {
            gridView3.ShowPrintPreview();
        }

        private void simpleButton27_Click(object sender, EventArgs e)
        {
            gridView4.ShowPrintPreview();
        }

        private void toolStripMenuItem5_Click(object sender, EventArgs e)
        {
            try
            {
                ildaldact s = new ildaldact();
                s.Text = "Шинэ акт бичих";
                s.projectID.Text = gridView1.GetFocusedRowCellValue("projectID").ToString().Trim();
                s.projectName.Text = gridView1.GetFocusedRowCellValue("projectName").ToString().Trim();
                s.aimag.Text = gridView1.GetFocusedRowCellValue("aimag").ToString().Trim();
                s.sumname.Text = gridView1.GetFocusedRowCellValue("sumname").ToString().Trim();
                s.zahorg.Text = gridView1.GetFocusedRowCellValue("hyanagch").ToString().Trim();
                s.zohorg.Text = gridView1.GetFocusedRowCellValue("author").ToString().Trim();
                s.ShowDialog();
            }
            catch (Exception)
            { }
            finally { }
        }

        private void шинээрОруулахToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void toolStripMenuItem3_Click_1(object sender, EventArgs e)
        {
            try
            {
                dataSetFill dc = new dataSetFill();
                string id = gridView2.GetFocusedRowCellValue("id").ToString().Trim();
                DialogResult dr = MessageBox.Show(""+gridView2.GetFocusedRowCellValue("id").ToString()+" кодтой зардлын мэдээллийг утсгах уу?", "Анхаар", MessageBoxButtons.YesNo);
                if (dr == System.Windows.Forms.DialogResult.Yes)
                {
                    var data = new NameValueCollection();
                    data["deleteid"] = id;
                    data["ali_table"] = "costs";
                    dc.exec_command("deleteAll", data);
                    //f.saveLogg(f.salerID.Text, f.salerName.Text, "Агуулахын бүртгэлээс устгасан");
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.ToString());
            }
            finally 
            { 
                FillGridCost(Convert.ToInt16(projectID1.Text.Trim()));
            }
        }

        private void contextMenuStrip3_Opening(object sender, CancelEventArgs e)
        {
            if (gridView2.RowCount == 0)
            {
                e.Cancel = true; // ContextMenuStrip нээгдэхгүй
            }
        }

        private void toolStripMenuItem6_Click(object sender, EventArgs e)
        {
            try
            {
                dataSetFill dc = new dataSetFill();
                string id = gridView5.GetFocusedRowCellValue("projectID").ToString();
                DialogResult dr = MessageBox.Show("" + gridView5.GetFocusedRowCellValue("projectName").ToString() + " нэртэй төслийн мэдээллийг хэрэгжиж байгаа жагсаалруу шилжүүлэх гэж байна уу?", "Анхаар", MessageBoxButtons.YesNo);
                if (dr == System.Windows.Forms.DialogResult.Yes)
                {
                    var data = new NameValueCollection();
                    data["id"] = id;
                    MessageBox.Show(dc.exec_command("resetproject", data));
                    //f.saveLogg(f.salerID.Text, f.salerName.Text, "Агуулахын бүртгэлээс устгасан");
                    
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.ToString());
            }
            finally 
            {
                FillGridOdoo();
                FillGridDuussan();
            }
        }

        private void contextMenuStrip4_Opening(object sender, CancelEventArgs e)
        {
            if (gridView5.RowCount == 0)
            {
                e.Cancel = true; // ContextMenuStrip нээгдэхгүй
            }
        }

        private void gridControl5_Click(object sender, EventArgs e)
        {
            try
            {
                if (gridView5.RowCount > 0)
                {
                    string projectId = gridView5.GetFocusedRowCellValue("projectID").ToString().Trim();
                    projectName = gridView5.GetFocusedRowCellValue("projectName").ToString().Trim();
                    projectID1.Text = projectId.Trim();
                    var costData = ds.gridFill("getcost", "projectID=" + projectId); //шууд тэнцүүлэхээр адилхан биз дээ адилха. сая мсж-р харахад яг л getcost.php-с ирсэн хариуг ачаалах үед алдаа гараад байх шигбайхын
                    var incomeData = ds.gridFill("getIncome", "projectID=" + projectId);
                    var documentData = ds.gridFill("getdocument", "projectID=" + projectId);
                    gridControl2.DataSource = costData ?? new DataTable();
                    gridControl3.DataSource = incomeData ?? new DataTable();
                    gridControl4.DataSource = documentData ?? new DataTable();
                }
                else
                {

                }
            }
            catch (Exception ee)
            {
                MessageBox.Show("" + ee.ToString() + "Өгөгдлийн сангаас өгөгдөл ачаалах үед алдаа гарсан байв  шдэ. Хэрэгжиж дууссан төслүүд дээр шүү сда гэж... projectID: " + projectID1.Text);
            }
            finally
            {

            }
        }
        private void засахToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                editprojects eb = new editprojects(this);
                eb.comID.Text = gridView1.GetFocusedRowCellValue("projectID").ToString();
                eb.projectID.Text = gridView1.GetFocusedRowCellValue("projectID").ToString();
                eb.projectName.Text = gridView1.GetFocusedRowCellValue("projectName").ToString();
                eb.gereeNo.Text = gridView1.GetFocusedRowCellValue("gereeNo").ToString();
                eb.ognooG.Text = gridView1.GetFocusedRowCellValue("ognooG").ToString();
                eb.budget.Text = gridView1.GetFocusedRowCellValue("budget").ToString();
                eb.zahialagchT.Text = gridView1.GetFocusedRowCellValue("zahialagchT").ToString();
                eb.authorT.Text = gridView1.GetFocusedRowCellValue("authorT").ToString();
                eb.injhai.Text = gridView1.GetFocusedRowCellValue("injhaiT").ToString();
                eb.zahialagch.Text = gridView1.GetFocusedRowCellValue("zahialagch").ToString();
                eb.hyanagch.Text = gridView1.GetFocusedRowCellValue("hyanagch").ToString();
                eb.author.Text = gridView1.GetFocusedRowCellValue("author").ToString();
                eb.aimag.Text = gridView1.GetFocusedRowCellValue("aimag").ToString();
                eb.sumname.Text = gridView1.GetFocusedRowCellValue("sumname").ToString();
                eb.ognoo1.DateTime = Convert.ToDateTime(gridView1.GetFocusedRowCellValue("ognoo1").ToString());
                eb.ognoo2.DateTime = Convert.ToDateTime(gridView1.GetFocusedRowCellValue("ognoo2").ToString());
                eb.locationP.Text = gridView1.GetFocusedRowCellValue("location").ToString();
                eb.spinEdit1.Value = Convert.ToInt16(gridView1.GetFocusedRowCellValue("guitsetgel").ToString().Trim());
                eb.baritsaa.Text = gridView1.GetFocusedRowCellValue("baritsaa").ToString();
                eb.ShowDialog();
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.ToString());
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
        private void засахToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            try
            {
                if (gridView2.RowCount > 0)
                {
                    editcosts ecc = new editcosts(this);
                    ecc.costID.Text = gridView2.GetFocusedRowCellValue("id").ToString();
                    ecc.projectName.Text = gridView1.GetFocusedRowCellValue("projectName").ToString();
                    ecc.costname.Text = gridView2.GetFocusedRowCellValue("costname").ToString();
                    ecc.costs.Text = gridView2.GetFocusedRowCellValue("cost").ToString();
                    ecc.ognoo.DateTime = Convert.ToDateTime(gridView2.GetFocusedRowCellValue("ognoo").ToString());
                    ecc.projectID.Text = projectID1.Text;
                    ecc.ShowDialog();
                }

            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.ToString());
            }
            finally { }
        }

        private void барьцааЧөлөөлөхToolStripMenuItem_Click(object sender, EventArgs e)
        {
          
        }

        private void барьцааХөрөнгөЧөлөөлөхToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string projectid = gridView5.GetFocusedRowCellValue("projectID").ToString().Trim();
            try
            {
                    dataSetFill dc = new dataSetFill();
                    
                    string userID = UserSession.LoggedUserID.ToString();
                    DialogResult dr = MessageBox.Show("'" + gridView5.GetFocusedRowCellValue("projectName").ToString() + "' гэрээний барьцаа хөрөнгийг чөлөөлөх үү? Барьцаа хөрөнгө орлого болж бүртгэгдэх болно", "Санамж", MessageBoxButtons.YesNo);
                    if (dr == System.Windows.Forms.DialogResult.Yes)
                    {
                        var data = new NameValueCollection();
                        data["id"] = projectid;
                        data["userID"] = userID;
                        MessageBox.Show(dc.exec_command("baritsaa", data));
                    }
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.ToString());
            }
          finally 
          {
             
              FillGridIncome(Convert.ToInt32(projectid));
              FillGridDuussan();
          }  
        }

        private void улаанДэвтэрToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                redbook rdd = new redbook();
                rdd.ognoo.DateTime = DateTime.Now;
                rdd.projectID.Text = gridView1.GetFocusedRowCellValue("projectID").ToString().Trim();
                rdd.projectName.Text = gridView1.GetFocusedRowCellValue("projectName").ToString().Trim();
                rdd.zahialagchhyanalt.Text = gridView1.GetFocusedRowCellValue("zahialagch").ToString().Trim();
                rdd.zahialagch.Text = gridView1.GetFocusedRowCellValue("zahialagch").ToString().Trim();
                rdd.authorname.Text = gridView1.GetFocusedRowCellValue("author").ToString().Trim();
                rdd.aimag.Text = gridView1.GetFocusedRowCellValue("aimag").ToString().Trim();
                rdd.sum.Text = gridView1.GetFocusedRowCellValue("sumname").ToString().Trim();
                rdd.ShowDialog();
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.ToString());
            }
            finally
            {
 
            }

        }

        private void navBarItem1_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            try
            {
                comdoc a = new comdoc();
                a.xtraTabControl1.SelectedTabPage = a.xtraTabPage1;
                a.ShowDialog();
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.ToString());
            }
            finally { }
        }

        private void navBarItem7_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            try
            {
                ita a = new ita(this);
                a.ShowDialog();
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.ToString());
            }
            finally
            {

            }
        }
        private void PrintGridView(DevExpress.XtraGrid.Views.Grid.GridView gridView)
        {
            try
            {
                gridView.ShowPrintPreview();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }





        private void navBarItem12_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
           
        }

        private void navBarItem10_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            try
            {
                tender t = new tender(this);
                t.Text = comName.Text;
                t.ShowDialog();
            }
            catch (Exception)
            { }
            finally { }
        }
        dataSetFillnew dsn = new dataSetFillnew();
        private void navBarItem11_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            try
            {
                redbook rdd = new redbook();
                rdd.ognoo.DateTime = DateTime.Now;
                var parameters = new Dictionary<string, string> { { "status", "filter" }, { "comID", UserSession.LoggedComID.ToString() } };
                var projectData = dsn.getData("getproject", parameters);

                if (projectData == null || projectData.Rows.Count == 0)
                {
                    MessageBox.Show("Идэвхтэй төсөл олдсонгүй.", "Анхааруулга", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    rdd.projectnameFilter.Properties.DataSource = projectData;
                    rdd.projectnameFilter.Properties.ValueMember = "projectID";
                    rdd.projectnameFilter.Properties.DisplayMember = "projectName";
                    rdd.projectnameFilter.Properties.Columns.Clear(); // Бүх багануудаа цэвэрлэнэ
                    rdd.projectnameFilter.Properties.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("projectName", "Төслийн нэр"));
                }
                rdd.ShowDialog();
            }
            catch (Exception ee)
            {
                MessageBox.Show("Алдаа гарлаа: " + ee.Message, "Алдаа", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void navBarItem8_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            try
            {
                devices d = new devices();
                d.ShowDialog();
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.ToString());
            }
            finally
            { }
        }

        private void navBarItem6_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            try
            {
                comdoc a = new comdoc();
                a.xtraTabControl1.SelectedTabPage = a.xtraTabPage4;
                a.ShowDialog();
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.ToString());
            }
            finally { }
        }

        private void navBarItem5_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            try
            {
                comdoc a = new comdoc();
                a.xtraTabControl1.SelectedTabPage = a.xtraTabPage3;
                a.ShowDialog();
            }
             catch (Exception ee)
             {
                 MessageBox.Show(ee.ToString());
             }
             finally { }
        }

        private void navBarItem4_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            try
            {
                comdoc a = new comdoc();
                a.xtraTabControl1.SelectedTabPage = a.xtraTabPage2;
                a.ShowDialog();
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.ToString());
            }
            finally { }
        }

        private void navBarItem3_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            try
            {
                ZeelList a = new ZeelList();
                a.ShowDialog();
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.ToString());
            }
            finally { }
        }

        private void navBarItem2_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            try
            {
                alban a = new alban();
                a.ShowDialog();
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.ToString());
            }
            finally { }
        }

        private void navBarItem9_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            try
            {
                MessageBox.Show("Амжилттай хэрэгжиж дуусаад, хаагдсан гэрээнүүдийг автоматаар ажлын туршлага гэж үзнэ. Хэрэгжиж дууссан төслүүд хэсгийн харна уу.");
                xtraTabControl1.SelectedTabPage = xtraTabPage2;
                
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.ToString());
            }
            finally { }
           
        }
        public string projectName;
        private void simpleButton19_Click_1(object sender, EventArgs e)
        {
            try
            {
                PrintGridview.Print(
                gridView3,
                20, 15, 15, 10,  // Margin-ууд
                gridView3.GroupPanelText + ": " + projectName,
                "",
                userInfo.comName,     // Header хэсэг
                "Хэвлэсэн:" + userInfo.userName,
                DateTime.Now.ToString("yyyy-MM-dd"), // Footer хэсэг
                false // Landscape чиглэл
                );
            }
            catch (Exception ee)
            {
                MessageBox.Show("Алдаа:" + ee.ToString());
            }
            finally { }
        }

        private void simpleButton23_Click_1(object sender, EventArgs e)
        {
            try
            {
                PrintGridview.Print(
                gridView2,
                20, 15, 15, 10,  // Margin-ууд
                gridView2.GroupPanelText + ": " + projectName,
                "",
                userInfo.comName,     // Header хэсэг
                "Хэвлэсэн:" + userInfo.userName,
                DateTime.Now.ToString("yyyy-MM-dd"), // Footer хэсэг
                false // Landscape чиглэл
                );
            }
            catch (Exception ee)
            {
                MessageBox.Show("Алдаа:" + ee.ToString());
            }
            finally { }
        }

        private void simpleButton27_Click_1(object sender, EventArgs e)
        {
            try
            {
                PrintGridview.Print(
                gridView4,
                20, 15, 15, 10,  // Margin-ууд
                gridView4.GroupPanelText + ": " + projectName,
                "",
                userInfo.comName,     // Header хэсэг
                "Хэвлэсэн:" + userInfo.userName,
                DateTime.Now.ToString("yyyy-MM-dd"), // Footer хэсэг
                false // Landscape чиглэл
                );
            }
            catch (Exception ee)
            {
                MessageBox.Show("Алдаа:" + ee.ToString());
            }
            finally { }
        }
        private void gridView1_Click(object sender, EventArgs e)
        {
            try
            {
                if (gridView1.RowCount > 0)
                {
                    projectName = gridView1.GetFocusedRowCellValue("projectName").ToString().Trim();
                    int proID = Convert.ToInt16(gridView1.GetFocusedRowCellValue("projectID").ToString().Trim());
                    projectID1.Text = proID.ToString().Trim();
                    FillGridCost(proID);
                    FillGridIncome(proID);
                    FillGridDocument(proID);
                }
                else
                {

                }
            }
            catch (Exception ee)
            {
                MessageBox.Show("" + ee.ToString() + "Өгөгдлийн сангаас өгөгдөл ачаалах үед алдаа гарсан байв  шдэ. Одоо хэрэгжиж байгаа төсөл дээр шүү сда гэж.. projectID: " + projectID1.Text);
            }
            finally
            {

            }
        }

        private void gridView5_Click(object sender, EventArgs e)
        {
            try
            {
                if (gridView5.RowCount > 0)
                {
                    projectName = gridView5.GetFocusedRowCellValue("projectName").ToString().Trim();
                    int proID = Convert.ToInt16(gridView5.GetFocusedRowCellValue("projectID").ToString().Trim());
                    FillGridCost(proID);
                    FillGridIncome(proID);
                    FillGridDocument(proID);
                }
                else
                {

                }
            }
            catch (Exception ee)
            {
                MessageBox.Show("" + ee.ToString() + "Өгөгдлийн сангаас өгөгдөл ачаалах үед алдаа гарсан байв  шдэ. Хэрэгжиж дууссан төсөл дээр шүү сда гэж.. projectID: " + projectID1.Text);
            }
            finally
            {

            }
            
        }

        private void орлогоЗасахToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (gridView3.RowCount > 0)
                {
                    editincome edincome = new editincome(this);

                    edincome.incomeID.Text = gridView3.GetFocusedRowCellValue("id").ToString().Trim();
                    edincome.projectName.Text = gridView1.GetFocusedRowCellValue("projectName").ToString().Trim();
                    edincome.income.Text = gridView3.GetFocusedRowCellValue("income").ToString().Trim();
                    edincome.incomename.Text = gridView3.GetFocusedRowCellValue("incomename").ToString().Trim();
                    edincome.ognoo.Text = gridView3.GetFocusedRowCellValue("ognoo").ToString().Trim();
                    edincome.projectID.Text = gridView1.GetFocusedRowCellValue("projectID").ToString().Trim();
                    edincome.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Одоогоор орлогын өгөгдөл бүртгэгдээгүй байна.");
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.ToString());
            }
            finally
            {
                
            }
        }

        private void gridView2_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                freceipt fr = new freceipt();
                fr.projectID.Text = projectID1.Text.Trim();
                fr.costID.Text = gridView2.GetFocusedRowCellValue("id").ToString().Trim();
                fr.label2.Text = gridView1.GetFocusedRowCellValue("projectName").ToString().Trim();
                fr.label3.Text = gridView2.GetFocusedRowCellValue("costname").ToString().Trim();
                fr.ShowDialog();
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.ToString());
            }
            finally { }
        }

        private void баримтОруулахToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                addreceipt adrr = new addreceipt();
                adrr.costID.Text = gridView2.GetFocusedRowCellValue("id").ToString().Trim();
                adrr.projectID.Text = gridView1.GetFocusedRowCellValue("projectID").ToString().Trim();
                adrr.ShowDialog();
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.ToString());
            }
            finally { } 

        }

        private void xtraTabControl1_Click(object sender, EventArgs e)
        {
            try
            {
                if (xtraTabControl1.SelectedTabPageIndex == 0)
                {
                    contextMenuStrip2.Enabled = true;
                    contextMenuStrip3.Enabled = true;
                }
                if (xtraTabControl1.SelectedTabPageIndex == 1)
                {
                    contextMenuStrip2.Enabled = false;
                    contextMenuStrip3.Enabled = false;
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.ToString());
            }
        }
        BaseUrl url = new BaseUrl();
        baseinfo userInfo = new baseinfo(UserSession.LoggedUserID);
        private void simpleButton1_Click_1(object sender, EventArgs e)
        {
            
        }

        private void хуулганаасToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
               
                gethuulga hhh = new gethuulga(this);
                hhh.ShowDialog();
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.ToString());
            }
            finally { }
        }

        private void төлөвлөгөөToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                fplans fp = new fplans();
                fp.ognoo.DateTime = DateTime.Now;
                fp.projectID.Text = gridView1.GetFocusedRowCellValue("projectID").ToString().Trim();
                fp.projectName.Text = gridView1.GetFocusedRowCellValue("projectName").ToString().Trim();
                fp.zahialagchhyanalt.Text = gridView1.GetFocusedRowCellValue("zahialagch").ToString().Trim();
                fp.zahialagch.Text = gridView1.GetFocusedRowCellValue("zahialagch").ToString().Trim();
                fp.authorname.Text = gridView1.GetFocusedRowCellValue("author").ToString().Trim();
                fp.aimag.Text = gridView1.GetFocusedRowCellValue("aimag").ToString().Trim();
                fp.sum.Text = gridView1.GetFocusedRowCellValue("sumname").ToString().Trim();
                fp.ShowDialog();

            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.ToString());
            }
            finally
            {
 
            }
        }

        private void navBarItem13_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            try
            {
                fplans fp = new fplans();
                fp.ognoo.DateTime = DateTime.Now;
                fp.ShowDialog();
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.ToString());
            }
            finally { }
        }

        private void navBarItem14_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            try
            {
                fmaterials fm = new fmaterials();
                fm.projectnameFilter.Properties.DataSource = ds.gridFill("getproject", "status=filter&comID="+UserSession.LoggedComID.ToString().Trim());
                fm.projectnameFilter.Properties.ValueMember = "projectID";
                fm.projectnameFilter.Properties.DisplayMember = "projectName";
                fm.projectnameFilter.Properties.Columns.Clear(); // Бүх багануудаа цэвэрлэнэ
                fm.projectnameFilter.Properties.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("projectName", "Төслийн нэр"));
                fm.ShowDialog();
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.ToString());
            }
            finally { }
        }

        private void simpleButton2_Click_1(object sender, EventArgs e)
        {
           
        }

        private void navBarItem15_ItemChanged(object sender, EventArgs e)
        {
            
        }

        private void navBarItem16_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            try
            {
                fappusers a = new fappusers();
                a.xtraTabControl1.SelectedTabPage = a.xtraTabPage3;
                a.ShowDialog();
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.ToString());
            }
            finally { }
        }

        private void navBarItem17_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            try
            {
                fappusers a = new fappusers();
                a.xtraTabControl1.SelectedTabPage = a.xtraTabPage2;
                a.ShowDialog();
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.ToString());
            }
            finally { }
        }

        private void navBarItem15_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            try
            {
                fappusers a = new fappusers();
                a.xtraTabControl1.SelectedTabPage = a.xtraTabPage1;
                a.ShowDialog();
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.ToString());
            }
            finally { }
        }

        private void navBarItem18_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            try
            {
                fappusers a = new fappusers();
                a.xtraTabControl1.SelectedTabPage = a.xtraTabPage4;
                a.ShowDialog();
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.ToString());
            }
            finally { }
        }

        private void navBarItem19_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            ildaldact rdd = new ildaldact();
            rdd.ShowDialog();
        }

        private void toolStripMenuItem10_Click(object sender, EventArgs e)
        {
            try
            {
                if (xtraTabControl1.SelectedTabPageIndex == 0)
                {
                    if (gridView4.RowCount > 0)
                    {
                        dataSetFill dc = new dataSetFill();
                        string id = gridView4.GetFocusedRowCellValue("id").ToString().Trim();
                        DialogResult dr = MessageBox.Show("" + gridView4.GetFocusedRowCellValue("id").ToString() + " кодтой баримт бичгийн мэдээллийг утсгах уу?", "Анхаар", MessageBoxButtons.YesNo);
                        if (dr == System.Windows.Forms.DialogResult.Yes)
                        {
                            var data = new NameValueCollection();
                            data["deleteid"] = id;
                            data["ali_table"] = "document";
                            dc.exec_command("deleteAll", data);
                            FillGridDocument(Convert.ToInt16(projectID1.Text));
                        }

                    }

                }
                else
                {
                    MessageBox.Show("Хэрэгжиж дууссан төслийн хамаарал бүхий баримт бичгийг устгах боломжгүй.");
                }
                
                

            }
            catch (Exception)
            {
                MessageBox.Show("алдаа");
            }

        }

        private void navBarItem20_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            FillGridDuussan();
            FillGridOdoo();
        }

        private void navBarItem21_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            try
            {
                Process.Start("chrome.exe", "--app=" + url.GetUrl());
            }
            catch (Exception)
            {
                MessageBox.Show("Алдаа гарлаа");
            }
            finally
            {

            }
        }

        private void navBarItem22_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            try
            {
                if (xtraTabControl1.SelectedTabPageIndex == 0)
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
                if (xtraTabControl1.SelectedTabPageIndex == 1)
                {

                    PrintGridview.Print(gridView5,
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
            catch (Exception ee)
            {
                MessageBox.Show(ee.ToString());
            }
            finally { }
        }

 private void navBarItem23_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            try
            {
                CheckForUpdates();
            }
            catch (Exception)
            {
                MessageBox.Show("Шинэчилэл татах үед алдаа гарлаа.");
            }
        }


private void CheckForUpdates()
{
    // Сервер дээрх ZIP-ийн зөв зам (tail slash-ыг зохицуулна)
    string updateUrl    = url.GetUrl().TrimEnd('/') + "/downloads/updates/DS.zip";
    string tempFilePath = Path.Combine(Path.GetTempPath(), "DS_temp.zip"); // түр таталт

    try
    {
        using (var client = new WebClient())
        {
            // Хэрэв HTTPS бол TLS 1.2 хэрэг болж магадгүй:
            // ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072; // Tls12

            try
            {
                client.DownloadFile(updateUrl, tempFilePath);
            }
            catch (WebException wex)
            {
                var resp = wex.Response as HttpWebResponse;
                string http = resp != null ? ((int)resp.StatusCode + " " + resp.StatusCode) : "no HTTP response";
                MessageBox.Show("Шинэчлэлийн файлыг татаж чадсангүй. " + http,
                                "Шинэчлэл", MessageBoxButtons.OK, MessageBoxIcon.Information);
                SafeDelete(tempFilePath);
                return;
            }
        }

        // Татсан файл хоосон биш эсэх
        var fi = new FileInfo(tempFilePath);
        if (!fi.Exists || fi.Length == 0)
        {
            MessageBox.Show("Татсан ZIP файл буруу эсвэл хоосон байна.", "Шинэчлэл",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
            SafeDelete(tempFilePath);
            return;
        }

        // 1) Локаль ажиллаж буй DS.exe-ийн хэш
        string localExePath = Application.ExecutablePath;
        string localExeHash = GetFileHash(localExePath);

        // 2) Серверийн ZIP доторх DS.exe-ийн хэш
        string serverExeHash = GetZipEntryHash(tempFilePath, "DS.exe");
        if (serverExeHash == null)
        {
            MessageBox.Show("ZIP дотор DS.exe олдсонгүй.", "Шинэчлэл",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
            SafeDelete(tempFilePath);
            return;
        }

        // 3) Харьцуулалт: ижил бол шинэчлэл алга, өөр бол updater ажиллуулна
        if (string.Equals(localExeHash, serverExeHash, StringComparison.OrdinalIgnoreCase))
        {
            MessageBox.Show("Одоогоор шинэчлэл хийгдээгүй байна.", "Шинэчлэл",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
            SafeDelete(tempFilePath);
            return;
        }
        else
        {
            // Updater.exe-ээ дуудах
            string updaterPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "updater", "Updater.exe");
            if (!File.Exists(updaterPath))
            {
                MessageBox.Show("Updater.exe олдсонгүй. 'updater' хавтсанд байршуулна уу.", "Шинэчлэл",  MessageBoxButtons.OK, MessageBoxIcon.Error);
                SafeDelete(tempFilePath);
                return;
            }
            DialogResult dr = MessageBox.Show("Шинэ хувилбар олдлоо! Шинэчлэлийг суулгах уу?", "Шинэчлэл", MessageBoxButtons.YesNo,  MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                Process.Start(updaterPath); 
                SafeDelete(tempFilePath);
                Application.Exit(); // үндсэн аппыг хаана
            }
            else
            {
                SafeDelete(tempFilePath); // түр татсан файлыг устгаад зогсоно
            }
        }
    }
    catch (Exception ex)
    {
        SafeDelete(tempFilePath);
        MessageBox.Show("Алдаа гарлаа: " + ex.Message, "Алдаа",  MessageBoxButtons.OK, MessageBoxIcon.Error);
    }
}

// ---- Туслах функцууд ----

// Файлын SHA-256 хэш
private static string GetFileHash(string path)
{
    using (var sha = SHA256.Create())
    using (var fs = File.OpenRead(path))
    {
        var hash = sha.ComputeHash(fs);
        return BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
    }
}

// ZIP доторх тодорхой entry-г (энд DS.exe) уншаад SHA-256 хэшийг нь буцаана
private static string GetZipEntryHash(string zipPath, string entryFileName)
{
    using (var fs = File.OpenRead(zipPath))
    using (var zis = new ZipInputStream(fs))
    using (var sha = SHA256.Create())
    {
        ZipEntry entry;
        while ((entry = zis.GetNextEntry()) != null)
        {
            if (!entry.IsFile) continue;

            // Фолдерийн бүтцийг үл харгалзан зөвхөн файлын нэрээр шүүнэ
            if (!string.Equals(Path.GetFileName(entry.Name), entryFileName, StringComparison.OrdinalIgnoreCase))
                continue;

            var hash = sha.ComputeHash(zis); // тухайн entry-г дуустал уншаад хэш тооцно
            return BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
        }
    }
    return null; // олдсонгүй
}

// Аюулгүй устгал
private static void SafeDelete(string path)
{
    try { if (File.Exists(path)) File.Delete(path); } catch { }
}

       
        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            if (gridView1.RowCount == 0)
            {
                e.Cancel = true; // ContextMenuStrip нээгдэхгүй
            }
        }

        private void contextMenuStrip2_Opening(object sender, CancelEventArgs e)
        {
            if (gridView3.RowCount == 0)
            {
                e.Cancel = true; // ContextMenuStrip нээгдэхгүй
            }
        }

        private void contextMenuStrip5_Opening(object sender, CancelEventArgs e)
        {
            if (gridView4.RowCount == 0)
            {
                e.Cancel = true; // ContextMenuStrip нээгдэхгүй
            }
        }
        private decimal ToDecimalSafe(object value)
        {
            if (value == null || value == DBNull.Value) return 0m;
            decimal result;
            if (decimal.TryParse(value.ToString(), out result))
                return result;
            return 0m;
        }
        private void toolStripMenuItem7_Click(object sender, EventArgs e)
        {
           

        }

        private void дангаарToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                int rowHandle = gridView1.FocusedRowHandle;
                if (rowHandle < 0)
                {
                    MessageBox.Show("Мөр сонгоогүй байна!", "Анхааруулга", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Grid-ээс FieldName-аар утга авах
                string name = Convert.ToString(gridView1.GetRowCellValue(rowHandle, "projectName"));
                decimal budget = ToDecimalSafe(gridView1.GetRowCellValue(rowHandle, "budget"));
                string ognooG = Convert.ToString(gridView1.GetRowCellValue(rowHandle, "ognooG"));
                string gereeD = Convert.ToString(gridView1.GetRowCellValue(rowHandle, "gereeNo"));
                string begin = Convert.ToString(gridView1.GetRowCellValue(rowHandle, "ognoo1"));
                string end = Convert.ToString(gridView1.GetRowCellValue(rowHandle, "ognoo2"));
                decimal zahilagch = ToDecimalSafe(gridView1.GetRowCellValue(rowHandle, "zahialagchT"));
                decimal authorT = ToDecimalSafe(gridView1.GetRowCellValue(rowHandle, "authorT"));
                decimal injhaiT = ToDecimalSafe(gridView1.GetRowCellValue(rowHandle, "injhaiT"));
                decimal baritsaa = ToDecimalSafe(gridView1.GetRowCellValue(rowHandle, "guitsetgel"));
                decimal income = ToDecimalSafe(gridView1.GetRowCellValue(rowHandle, "income"));

                // Report үүсгээд утгуудыг оноох
                reportfinance rf = new reportfinance();

                rf.projectName.Text = name;
                rf.total.Text = budget.ToString("#,##0");
                rf.gereeNo.Text = gereeD;
                rf.ognooG.Text = ognooG;
                rf.ognoo1.Text = begin;
                rf.ognoo2.Text = end;
                rf.zahi.Text = zahilagch.ToString("#,##0");
                rf.zohi.Text = authorT.ToString("#,##0");
                rf.zurag.Text = injhaiT.ToString("#,##0");
                rf.baritsaa.Text = baritsaa.ToString("#,##0");

                decimal mytotal = budget - zahilagch - authorT - injhaiT - baritsaa;
                decimal uld = mytotal - income;
                rf.mytotal.Text = mytotal.ToString("#,##0");


                rf.urid.Text = income.ToString("#,##0");
                rf.uld.Text = uld.ToString("#,##0");

                rf.ShowPreview();
            }
            catch (Exception exx)
            {
                MessageBox.Show("Алдаа гарлаа: " + exx.Message, "Алдаа", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void бүгдToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string company = userInfo.comName;
            reportfinanceAll rf = new reportfinanceAll();
            List<ReportData> reportDataList = new List<ReportData>();

            for (int i = 0; i < gridView1.RowCount; i++)
            {
                decimal budget = Convert.ToDecimal(gridView1.GetRowCellValue(i, "budget"));
                decimal income = Convert.ToDecimal(gridView1.GetRowCellValue(i, "income"));
                decimal injhaiT = Convert.ToDecimal(gridView1.GetRowCellValue(i, "injhaiT"));
                decimal zahiT = Convert.ToDecimal(gridView1.GetRowCellValue(i, "zahialagchT"));
                decimal zohiT = Convert.ToDecimal(gridView1.GetRowCellValue(i, "authorT"));
                decimal baritsaa = Convert.ToDecimal(gridView1.GetRowCellValue(i, "baritsaa"));
                decimal cost = Convert.ToDecimal(gridView1.GetRowCellValue(i, "cost"));
                

                decimal uldegdel = budget - (income + zohiT + zahiT + injhaiT + baritsaa);

                reportDataList.Add(new ReportData
                {
                    classDDR = Convert.ToString(gridView1.GetRowCellValue(i, "dd")),
                    classProjectName = Convert.ToString(gridView1.GetRowCellValue(i, "projectName")),
                    classbudgetR = budget,
                    classgereeNo = Convert.ToString(gridView1.GetRowCellValue(i, "gereeNo")),
                    classzahi = zahiT,
                    classzohi = zohiT,
                    classinjhaiT = injhaiT,
                    classbaritsaa = baritsaa,
                    classincome = income,
                    classcost = cost,
                    classuld = uldegdel
                });
            }

            // Тайланд өгөгдөл оноох
            rf.DataSource = reportDataList;

            // Bindings
            rf.dd.DataBindings.Add(new XRBinding("Text", null, "classDDR"));
            rf.projectName.DataBindings.Add(new XRBinding("Text", null, "classProjectName"));
            rf.budget.DataBindings.Add(new XRBinding("Text", null, "classbudgetR", "{0:#,##0}"));
            rf.gereeNo.DataBindings.Add(new XRBinding("Text", null, "classgereeNo"));
            rf.zahi.DataBindings.Add(new XRBinding("Text", null, "classzahi", "{0:#,##0}"));
            rf.zohi.DataBindings.Add(new XRBinding("Text", null, "classzohi", "{0:#,##0}"));
            rf.injhai.DataBindings.Add(new XRBinding("Text", null, "classinjhaiT", "{0:#,##0}"));
            rf.costs.DataBindings.Add(new XRBinding("Text", null, "classcost", "{0:#,##0}"));
            rf.baritsaa.DataBindings.Add(new XRBinding("Text", null, "classbaritsaa", "{0:#,##0}"));
            rf.income.DataBindings.Add(new XRBinding("Text", null, "classincome", "{0:#,##0}"));
            rf.uld.DataBindings.Add(new XRBinding("Text", null, "classuld", "{0:#,##0}"));

            // Нийлбэрүүдийг бодох
            decimal sumbudget = reportDataList.Sum(x => x.classbudgetR);
            decimal sumzahi = reportDataList.Sum(x => x.classzahi);
            decimal sumzohi = reportDataList.Sum(x => x.classzohi);
            decimal suminjhai = reportDataList.Sum(x => x.classinjhaiT);
            decimal sumcost = reportDataList.Sum(x => x.classcost);
            decimal sumincome = reportDataList.Sum(x => x.classincome);
            decimal sumbaritsaa = reportDataList.Sum(x => x.classbaritsaa);
            decimal sumuld = reportDataList.Sum(x => x.classuld);

            // Footer label-д оноох
            rf.sumbudget.Text = sumbudget.ToString("#,##0");
            rf.sumzahi.Text = sumzahi.ToString("#,##0");
            rf.sumzohi.Text = sumzohi.ToString("#,##0");
            rf.suminjhai.Text = suminjhai.ToString("#,##0");
            rf.sumcost.Text = sumcost.ToString("#,##0");
            rf.sumincome.Text = sumincome.ToString("#,##0");
            rf.sumbaritsaa.Text = sumbaritsaa.ToString("#,##0");
            rf.sumuldegdel.Text = sumuld.ToString("#,##0");
            rf.comName.Text = UserSession.LoggedComName ?? "Е-төсөл";

            // Preview гаргах
            rf.ShowPreview();
        }

        private void ажилДууссанМэдэгдэлToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                // 1) Хоёр огноо асуух жижиг диалог үүсгэнэ
                DateTime? docDate;      // Албан бичгийн огноо
                DateTime? doneDate;     // Ажил дууссан огноо
                if (!AskDates("Ажил дууссан огноо:", out docDate, out doneDate)) return; // Cancel дарвал зогсооно

                // 2) Grid-ээс утгуудыг аюулгүй унших
                int row = gridView1.FocusedRowHandle;
                if (row < 0) { XtraMessageBox.Show("Мөр сонгоно уу."); return; }

                string gereeNo = Convert.ToString(gridView1.GetRowCellValue(row, "gereeNo"));
                string projectName = Convert.ToString(gridView1.GetRowCellValue(row, "projectName"));
                string zahialagch = Convert.ToString(gridView1.GetRowCellValue(row, "zahialagch"));
                string company = (userInfo != null) ? Convert.ToString(userInfo.comName) : "";

                // 3) Тайлан бэлдэх
                reporttendermat rpt = new reporttendermat();
                rpt.tuhai.Text = "Мэдэгдэл хүргүүлэх тухай";
                rpt.Bdugaar.Text = "М/15";

                // Албан бичгийн огноо -> тайлангийн 'ognoo' control руу
                if (docDate.HasValue) rpt.ognoo.Text = docDate.Value.ToString("yyyy-MM-dd");

                // Ажил дууссан огноо (тайланд тусдаа control байгаа бол түүн рүү нь дамжуулж болно)
                // Жишээлбэл: rpt.ajilDuussanOgnoo?.Text = doneDate.Value.ToString("yyyy-MM-dd");

                // Текстдээ дууссан огноог шингээнэ
                string donePart = doneDate.HasValue ? doneDate.Value.ToString("yyyy-MM-dd") : "";
                rpt.utga.Text =
                    "      Бид " + company + " нь \"№" + gereeNo + "\" дугаартай \"" + projectName +
                    "\" ажлыг " + donePart + " өдөр гүйцэтгэж дууссан тул байнгын ашиглалтад хүлээн авна уу.";

                rpt.haashaa.Text = Convert.ToString(zahialagch);

                rpt.ShowPreview();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.ToString());
            }
       
        }
        private bool AskDates(string secondLabel, out DateTime? docDate, out DateTime? doneDate)
        {
            docDate = null;
            doneDate = null;

            // === Диалог үүсгэнэ ===
            XtraForm dlg = new XtraForm();
            dlg.Text = "Огноо оруулах";
            dlg.StartPosition = FormStartPosition.CenterParent;
            dlg.FormBorderStyle = FormBorderStyle.FixedDialog;
            dlg.MinimizeBox = false;
            dlg.MaximizeBox = false;
            dlg.ClientSize = new Size(380, 160);

            // === Албан бичгийн огноо ===
            LabelControl lbl1 = new LabelControl();
            lbl1.Text = "Албан бичгийн огноо:";
            lbl1.Location = new Point(16, 20);

            DateEdit de1 = new DateEdit();
            de1.Location = new Point(180, 16);
            de1.Width = 170;
            de1.Properties.Mask.EditMask = "yyyy-MM-dd";
            de1.Properties.DisplayFormat.FormatString = "yyyy-MM-dd";
            de1.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            de1.Properties.EditFormat.FormatString = "yyyy-MM-dd";
            de1.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            de1.Properties.VistaDisplayMode = DevExpress.Utils.DefaultBoolean.True;
            de1.Properties.VistaEditTime = DevExpress.Utils.DefaultBoolean.True;
            de1.DateTime = DateTime.Today;

            // === Ажил дууссан огноо ===
            LabelControl lbl2 = new LabelControl();
            lbl2.Text = secondLabel;
            lbl2.Location = new Point(16, 60);

            DateEdit de2 = new DateEdit();
            de2.Location = new Point(180, 56);
            de2.Width = 170;
            de2.Properties.Mask.EditMask = "yyyy-MM-dd";
            de2.Properties.DisplayFormat.FormatString = "yyyy-MM-dd";
            de2.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            de2.Properties.EditFormat.FormatString = "yyyy-MM-dd";
            de2.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            de2.Properties.VistaDisplayMode = DevExpress.Utils.DefaultBoolean.True;
            de2.Properties.VistaEditTime = DevExpress.Utils.DefaultBoolean.True;
            de2.DateTime = DateTime.Today;

            // === Товчнууд ===
            SimpleButton btnOK = new SimpleButton();
            btnOK.Text = "Oк";
            btnOK.DialogResult = DialogResult.OK;
            btnOK.Location = new Point(190, 110);
            btnOK.Width = 75;

            SimpleButton btnCancel = new SimpleButton();
            btnCancel.Text = "Болих";
            btnCancel.DialogResult = DialogResult.Cancel;
            btnCancel.Location = new Point(275, 110);
            btnCancel.Width = 75;

            // === Элементийг нэмэх ===
            dlg.Controls.Add(lbl1);
            dlg.Controls.Add(de1);
            dlg.Controls.Add(lbl2);
            dlg.Controls.Add(de2);
            dlg.Controls.Add(btnOK);
            dlg.Controls.Add(btnCancel);

            dlg.AcceptButton = btnOK;
            dlg.CancelButton = btnCancel;

            // === Диалогыг харуулах ===
            DialogResult dr = dlg.ShowDialog();
            if (dr != DialogResult.OK)
                return false; // Хэрэв Cancel бол зогсооно

            // === Хариу буцаах ===
            docDate = de1.EditValue == null ? (DateTime?)null : de1.DateTime.Date;
            doneDate = de2.EditValue == null ? (DateTime?)null : de2.DateTime.Date;
            return true;
        }

        private void гүйцэтгэлийнБаталгааЦуцлуулахToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                // 1) Хоёр огноо асуух жижиг диалог үүсгэнэ
                DateTime? docDate;      // Албан бичгийн огноо
                DateTime? doneDate;     // Ажил дууссан огноо
                if (!AskDates("Комиссийн дүгнэлтийн огноо:", out docDate, out doneDate)) return; // Cancel дарвал зогсооно

                // 2) Grid-ээс утгуудыг аюулгүй унших
                int row = gridView1.FocusedRowHandle;
                if (row < 0) { XtraMessageBox.Show("Мөр сонгоно уу."); return; }

                string gereeNo = Convert.ToString(gridView1.GetRowCellValue(row, "gereeNo"));
                string projectName = Convert.ToString(gridView1.GetRowCellValue(row, "projectName"));
                string zahialagch = Convert.ToString(gridView1.GetRowCellValue(row, "zahialagch"));
                string company = (userInfo != null) ? Convert.ToString(userInfo.comName) : "";

                // 3) Тайлан бэлдэх
                reporttendermat rpt = new reporttendermat();
                rpt.tuhai.Text = "Хүсэлт гаргах тухай";
                rpt.Bdugaar.Text = "М/15";

                // Албан бичгийн огноо -> тайлангийн 'ognoo' control руу
                if (docDate.HasValue) rpt.ognoo.Text = docDate.Value.ToString("yyyy-MM-dd");

                // Ажил дууссан огноо (тайланд тусдаа control байгаа бол түүн рүү нь дамжуулж болно)
                // Жишээлбэл: rpt.ajilDuussanOgnoo?.Text = doneDate.Value.ToString("yyyy-MM-dd");

                // Текстдээ дууссан огноог шингээнэ
                string donePart = doneDate.HasValue ? doneDate.Value.ToString("yyyy-MM-dd") : "";
                rpt.utga.Text =
                    "      Бид " + company + " нь \"№" + gereeNo + "\" дугаартай \"" + projectName +
                    "\" ажлыг " + donePart + " өдөр хүлээлгэн өгсөн байх тул манай гүйцэтгэлийн баталгааг цуцлуулж өгнө үү.";

                rpt.haashaa.Text = Convert.ToString(zahialagch);

                rpt.ShowPreview();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.ToString());
            }
        }

        private void чанарынБаталгааГаргахТухайToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                // 1) Хоёр огноо асуух жижиг диалог үүсгэнэ
                DateTime? docDate;      // Албан бичгийн огноо
                DateTime? doneDate;     // Ажил дууссан огноо
                if (!AskDates("Комиссийн дүгнэлтийн огноо:", out docDate, out doneDate)) return; // Cancel дарвал зогсооно

                // 2) Grid-ээс утгуудыг аюулгүй унших
                int row = gridView1.FocusedRowHandle;
                if (row < 0) { XtraMessageBox.Show("Мөр сонгоно уу."); return; }

                string gereeNo = Convert.ToString(gridView1.GetRowCellValue(row, "gereeNo"));
                string projectName = Convert.ToString(gridView1.GetRowCellValue(row, "projectName"));
                string zahialagch = Convert.ToString(gridView1.GetRowCellValue(row, "zahialagch"));
                string company = (userInfo != null) ? Convert.ToString(userInfo.comName) : "";

                // 3) Тайлан бэлдэх
                reporttendermat rpt = new reporttendermat();
                rpt.tuhai.Text = "Баталгаа гаргах тухай";
                rpt.Bdugaar.Text = "М/15";

                // Албан бичгийн огноо -> тайлангийн 'ognoo' control руу
                if (docDate.HasValue) rpt.ognoo.Text = docDate.Value.ToString("yyyy-MM-dd");

                // Ажил дууссан огноо (тайланд тусдаа control байгаа бол түүн рүү нь дамжуулж болно)
                // Жишээлбэл: rpt.ajilDuussanOgnoo?.Text = doneDate.Value.ToString("yyyy-MM-dd");

                // Текстдээ дууссан огноог шингээнэ
                string donePart = doneDate.HasValue ? doneDate.Value.ToString("yyyy-MM-dd") : "";
                rpt.utga.Text =
                    "      Бид " + company + " нь \"№" + gereeNo + "\" дугаартай \"" + projectName +
                    "\" ажлыг байнгын ашиглалтад хүлээлгэн өгсөн өдрөөс хойш 1 жилийн хугацаанд чанарын баталгаа гаргаж буй үүгээр баталгаажуулж байна.";

                rpt.haashaa.Text = Convert.ToString(zahialagch);

                rpt.ShowPreview();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.ToString());
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            MessageBox.Show(url.GetUrl());
        }

        private void statuscombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (xtraTabControl1.SelectedTabPageIndex == 0)
                {
                   
                }
                if (xtraTabControl1.SelectedTabPageIndex == 1)
                {
                    switch (statuscombo.SelectedIndex)
                    {
                        case 0: gridView5.ActiveFilterString = "";
                                break;
                        case 1: gridView5.ActiveFilterString = "[baritsaa] = 0";
                                break;
                        case 2: gridView5.ActiveFilterString = "[baritsaa] <> 0";
                                break;
                        case 3: gridView5.ActiveFilterString = "[status] = 3";
                                break;
                        default:
                                gridView5.ActiveFilterString = "";
                                break;
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Өгөгдөл байхгүй.");
            }
        }



       
      

      

    }
}
