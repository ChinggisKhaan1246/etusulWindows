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

namespace ST
{
    public partial class redbook : Form
    {
        public redbook()
        {
            InitializeComponent();
            gridView2.CustomUnboundColumnData += (sender, e) =>
            {
                GridView view = sender as GridView;
                if (e.Column.FieldName == "dd" && e.IsGetData)
                    e.Value = view.GetRowHandle(e.ListSourceRowIndex) + 1;
            };
        }
        dataSetFill ds = new dataSetFill();
        dataSetFillnew dsn = new dataSetFillnew();
        public void redbook_Load(object sender, EventArgs e)
        {
            try
            {
                gridControl2.DataSource = dsn.getData("getdaily", new Dictionary<string, string>
                    {
                         { "status", "9" }
                         
                    });
                gridView2.IndicatorWidth = 75;
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.ToString());
            }
            finally 
            {
                //gridControl2.RefreshDataSource();
            }
        }
        private void projectName_EditValueChanged(object sender, EventArgs e)
        {
            gridView2.ActiveFilterString = "projectName LIKE '%" + projectName.Text + "%'";
        }
        private void projectnameFilter_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                projectName.Text = projectnameFilter.Text;
                projectID.Text = projectnameFilter.EditValue.ToString();
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.ToString());
            }
            finally
            {
 
            }
        }
        private void simpleButton2_Click(object sender, EventArgs e)
        {
            try
            {
                if (projectName.Text != "")
                {
                    adddaily addreport = new adddaily(this);
                    addreport.projectID.Text = projectID.Text;
                    addreport.projectName.Text = projectName.Text;
                    addreport.ognoo.DateTime = DateTime.Now;
                    addreport.aimag.Text = aimag.Text.Trim();
                    addreport.sum.Text = sum.Text.Trim();
                    addreport.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Ялгах хэсгээс төсөлөө сонгоно уу.");
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.ToString());
            }
            finally  { }
        }
        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            try
            {
                dataSetFill dc = new dataSetFill();
                string id = gridView2.GetFocusedRowCellValue("id").ToString();
                DialogResult dr = MessageBox.Show("Тайлангийн мэдээллийг утсгах уу?", "Анхаар", MessageBoxButtons.YesNo);
                if (dr == System.Windows.Forms.DialogResult.Yes)
                {
                    var data = new NameValueCollection();
                    data["deleteid"] = id;
                    data["ali_table"] = "daily";
                    MessageBox.Show(dc.exec_command("deleteAll", data));
                    //f.saveLogg(f.salerID.Text, f.salerName.Text, "Агуулахын бүртгэлээс устгасан");
               
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.ToString());
            }
            finally
            {
                redbook_Load(sender, e);
            }
        }

        private void фотоЗурагОруулахToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (projectName.Text != "")
                {
                    dailypic dpic = new dailypic();
                    dpic.projectID.Text = projectID.Text;
                    dpic.label2.Text =    projectName.Text;
                    dpic.label3.Text =    gridView2.GetFocusedRowCellValue("hiisen").ToString();
                    dpic.daily.Text =     gridView2.GetFocusedRowCellValue("id").ToString();
                    dpic.engname.Text =   gridView2.GetFocusedRowCellValue("engID").ToString();
                    dpic.xabname.Text =   gridView2.GetFocusedRowCellValue("xabID").ToString();
                    dpic.daamal.Text =    gridView2.GetFocusedRowCellValue("daamalID").ToString();
                    dpic.weather.Text =   gridView2.GetFocusedRowCellValue("weather").ToString();
                    dpic.label10.Text =   gridView2.GetFocusedRowCellValue("ognoo").ToString()+" -c "+gridView2.GetFocusedRowCellValue("ognoo").ToString();
                    dpic.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Ялгах хэсгээс төсөл сонгоно уу. ");
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show("Төсөл сонгоно уу. "+ee.ToString());
            }
            finally { }
            
        }

        private void gridControl2_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                фотоЗурагОруулахToolStripMenuItem_Click(sender, e);
                
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.ToString());
            }
            finally { }
        }

        private void gridControl2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                EditRedBookData();
                e.Handled = true; 
            }
           
        }

        private void EditRedBookData()
        {
           try
                {
                    MessageBox.Show("Хадгалсан");
                    gridView2.PostEditor();
                    gridView2.UpdateCurrentRow();

                    object idObj = gridView2.GetFocusedRowCellValue("id");
                    object ognooObj = gridView2.GetFocusedRowCellValue("ognoo");
                    object ognooDObj = gridView2.GetFocusedRowCellValue("ognooD");
                    object hiisenObj = gridView2.GetFocusedRowCellValue("hiisen");
                    object weatherObj = gridView2.GetFocusedRowCellValue("weather");
                    object zorchilObj = gridView2.GetFocusedRowCellValue("zorchil");
                    object mechanismObj = gridView2.GetFocusedRowCellValue("mechanism");
                    object margaashObj = gridView2.GetFocusedRowCellValue("margaash");
                    object ajilchidObj = gridView2.GetFocusedRowCellValue("ajilchid");

                    if (idObj == null || ognooObj == null || hiisenObj == null)
                    {
                        MessageBox.Show("Мэдээлэл дутуу байна!", "Алдаа", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    string id = idObj.ToString().Trim();
                    string ognoo = Convert.ToDateTime(ognooObj).ToString("yyyy-MM-dd");
                    string ognooD = Convert.ToDateTime(ognooDObj).ToString("yyyy-MM-dd");
                    string hiisen = hiisenObj != null ? hiisenObj.ToString().Trim() : "";
                    string weather = weatherObj != null ? weatherObj.ToString().Trim() : "";
                    string zorchil = zorchilObj != null ? zorchilObj.ToString().Trim() : "";
                    string mechanism = mechanismObj != null ? mechanismObj.ToString().Trim() : "";
                    string margaash = margaashObj != null ? margaashObj.ToString().Trim() : "";
                    string ajilchid = ajilchidObj != null ? ajilchidObj.ToString().Trim() : "";

                    var data = new System.Collections.Specialized.NameValueCollection
                        {
                            { "id", id },
                            { "ognoo", ognoo },
                            { "ognooD", ognooD },
                            { "hiisen", hiisen },
                            { "weather", weather },
                            { "zorchil", zorchil },
                            { "mechanism", mechanism },
                            { "margaash", margaash },
                            { "ajilchid", ajilchid }
                        };

                    dataSetFill ds = new dataSetFill();
                    string response = ds.exec_command("editdaily", data);

                    if (!string.IsNullOrEmpty(response) && response.Contains("success"))
                    {
                        gridView2.SetFocusedRowCellValue("ognoo", ognoo);
                        gridView2.SetFocusedRowCellValue("ognooD", ognooD);
                        gridView2.SetFocusedRowCellValue("hiisen", hiisen);
                        gridView2.SetFocusedRowCellValue("zorchil", zorchil);
                        gridView2.SetFocusedRowCellValue("mechanism", mechanism);
                        gridView2.SetFocusedRowCellValue("margaash", margaash);
                        gridView2.SetFocusedRowCellValue("ajilchid", ajilchid);
                    }
                    else
                    {
                        MessageBox.Show("Хадгалах явцад алдаа гарлаа: " + response, "Алдаа", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Алдаа гарлаа: " + ex.Message, "Алдаа", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            
        }
        baseinfo userInfo = new baseinfo(UserSession.LoggedUserID);
        private void simpleButton1_Click(object sender, EventArgs e)
        {
                   PrintGridview.Print(
                   gridView2,
                   20, 15, 15, 10,  // Margin-ууд
                   gridView2.GroupPanelText + ": " + projectnameFilter.Text,
                   "",
                   userInfo.comName,     // Header хэсэг
                   "Хэвлэсэн:" + userInfo.userName,
                   DateTime.Now.ToString("yyyy-MM-dd"), // Footer хэсэг
                   true); // Landscape чиглэл);
        }

        private void gridView2_CustomDrawRowIndicator(object sender, RowIndicatorCustomDrawEventArgs e)
        {
            if (e.Info.IsRowIndicator)
            {
                e.Info.DisplayText = "Зураг..."; // Энд текстээ оруулна
                e.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center; // Төвд байршуулах
            }
        }

    }
}
