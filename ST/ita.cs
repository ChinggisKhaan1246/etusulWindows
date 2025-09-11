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
        BaseUrl Url = new BaseUrl();
        baseinfo userInfo = new baseinfo(UserSession.LoggedUserID);
        private void gridControl1_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                var encode = gridView1.GetFocusedRowCellValue("docu").ToString().Replace(" ", "%20");
                if (encode != "")
                {
                    FileViewer vvr = new FileViewer(Url.GetUrl() + "dist/uploads/ita/docu/MA/" + gridView1.GetFocusedRowCellValue("id").ToString() + "/" + encode);
                }
                else
                {
                    MessageBox.Show("Харгалзах файл байхгүй байна.");
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.ToString());
            }
            finally { }
        }
        private void gridControl2_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                var encode = gridView2.GetFocusedRowCellValue("docu").ToString().Replace(" ", "%20");
                if (encode != "")
                {
                    FileViewer vvr = new FileViewer(Url.GetUrl() + "dist/uploads/ita/docu/OP/" + gridView2.GetFocusedRowCellValue("id").ToString() + "/" + encode);
                }
                else
                {
                    MessageBox.Show("Харгалзах файл байхгүй байна.");
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.ToString());
            }
            finally { }
        }
        private void gridControl5_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                var encode = gridView5.GetFocusedRowCellValue("docu").ToString().Replace(" ", "%20");
                if (encode != "")
                {
                    FileViewer vvr = new FileViewer(Url.GetUrl() + "dist/uploads/ita/docu/ITA/" + gridView5.GetFocusedRowCellValue("id").ToString() + "/" + encode);
                }
                else
                {
                    MessageBox.Show("Харгалзах файл байхгүй байна.");
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.ToString());
            }
            finally { }
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
    }
}
