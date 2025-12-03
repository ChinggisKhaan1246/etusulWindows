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
    public partial class addbichig : Form
    {
        alban f;
        public addbichig(alban ff)
        {
            InitializeComponent();
            f = ff;
           
        }
        dataSetFill ds = new dataSetFill();
        private void bichigadd_Load(object sender, EventArgs e)
        {
            ognooDoc.DateTime = DateTime.Now;
           
        }

        private void lookUpEdit2_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (combobox12.SelectedIndex == 0)
                {
                    combobox22.Properties.DataSource = ds.gridFill("getita", "itatype=SIGN1");
                }
                if (combobox12.SelectedIndex == 1)
                {
                    combobox22.Properties.DataSource = ds.gridFill("getita", "itatype=SIGN2");
                }
                combobox22.Properties.ValueMember = "ner";
                combobox22.Properties.DisplayMember = "ner";
                combobox22.ItemIndex = 0;
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.ToString());
            }
            finally { }
        }
        int checkStamp = 0;
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            var data = new NameValueCollection();
            
            dataSetFill dcd = new dataSetFill();
            if (Bnumber.Text != "" && Utga.Text != "" && tuhai.Text != "")
            {
                try
                {
                  
                    data["Bnumber"] = Bnumber.Text;
                    data["haanaas"] = haashaa.Text;
                    data["tuhai"] = tuhai.Text;
                    data["Utga"] = Utga.Text;
                    data["signTushaal"] = combobox12.Text.ToUpper();
                    data["signName"] = combobox22.Text.ToUpper();
                    data["stamp"] = checkStamp.ToString();
                    data["albantype"] = "yavsan";
                    data["ognooDoc"] = ognooDoc.DateTime.ToString("yyyy-MM-dd");
                    data["ognoo"] = DateTime.Now.ToString("yyyy-MM-dd");
                    
                    MessageBox.Show(dcd.exec_command("addbichig", data));
                    
                    
                    //  this.Hide();
                }
                catch (Exception ee)
                {
                    MessageBox.Show(ee.ToString());
                }
                finally 
                { 
                    f.FillGridYavsan(); 
                    this.Hide(); 
                }
            }
            else
            {
                MessageBox.Show("Өгөгдөл дутуу байна.");
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked) 
            {
                checkStamp = 1;
            }
            else
            {
                checkStamp = 0;
            }
        }

        private void combobox22_EditValueChanged(object sender, EventArgs e)
        {
            
        }

        private void addbichig_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) // Enter товч дарагдсан эсэхийг шалгана
            {
                simpleButton1.PerformClick(); // simpleButton1_Click функцыг дуудаж байна
            }
        }
    }
}
