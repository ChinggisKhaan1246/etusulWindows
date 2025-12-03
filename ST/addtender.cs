using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Http;
using HtmlAgilityPack;
using Newtonsoft.Json;
using DevExpress.XtraRichEdit;

namespace ST
{
    public partial class addtender : Form
    {
        tender t;
        public addtender(tender tt)
        {
            InitializeComponent();
            t = tt;
        }

        private void simpleButton6_Click(object sender, EventArgs e)
        {
            try
            {
                
                var HttpClient = new HttpClient();
                var html = HttpClient.GetStringAsync(URLtextedit.Text).Result;
                var htmlDocument = new HtmlAgilityPack.HtmlDocument();
                htmlDocument.LoadHtml(html);


                //richEditControl1.Text = html;
              

                // Тендерийн нэр
                var nername = htmlDocument.DocumentNode.SelectSingleNode("//h3[@class='invitation-title']");
                if (nername != null)
                {
                    var ner1 = nername.InnerText.Trim();
                    ner.Text = ner1;
                }
                else
                {
                    MessageBox.Show("Тендерийн нэр олдсонгүй.");
                }

                // Хэрэглэгчийн мэдээлэл
                var clientname = htmlDocument.DocumentNode.SelectNodes("//div[@class = 'col-md-7 col-sm-7 col-xs-12 simple-row-value']");
                if (clientname != null && clientname.Count > 0)
                {
                    zahialagch.Text = clientname[0].InnerText.Trim();
                    turul.Text = clientname[1].InnerText.Trim();
                    Tdugaar.Text = clientname[2].InnerText.Trim();
                    Udugaar.Text = clientname[3].InnerText.Trim();
                    ehuusver.Text = clientname[4].InnerText.Trim();
                    niittusuv.Text = clientname[5].InnerText.Trim().Replace(",", "").Replace("₮", "");
                    onitusuv.Text = clientname[6].InnerText.Trim().Replace(",", "").Replace("₮", "");
                    juram.Text = clientname[7].InnerText.Trim();
                    tsahim.Text = clientname[8].InnerText.Trim();
                    tuluv.Text = clientname[9].InnerText.Trim();
                }
                else
                {
                    MessageBox.Show("Хэрэглэгчийн мэдээлэл олдсонгүй.");
                }

                // Тендерийн тайлбар
                var bigtextname = htmlDocument.DocumentNode.SelectSingleNode("//div[@class='tender-info-content padding-top-25 padding-bottom-10']");
                if (bigtextname != null)
                {
                    var bigtext = bigtextname.InnerText.Trim();
                    richEditControl1.Text = bigtext;
                    richEditControl1.Text = richEditControl1.Text.Replace("&nbsp;", " ");
                    richEditControl1.Text = richEditControl1.Text.Replace("&ndash;", "-");
                    richEditControl1.Text = richEditControl1.Text.Replace("&quot;", "\"");
                }
                else
                {
                    MessageBox.Show("Тендерийн тайлбар олдсонгүй.");
                }

                // Нээлтийн огноо
                var dateDay = htmlDocument.DocumentNode.SelectNodes("//div[@class='time']");
                var Time1 = htmlDocument.DocumentNode.SelectNodes("//div[@class='time padding-top-10']");

                if (dateDay != null && Time1 != null && dateDay.Count > 1 && Time1.Count > 1)
                {
                    ognooOpen.Text = dateDay[0].InnerText.Trim() + " " + Time1[0].InnerText.Trim();
                    ognooGet.Text = dateDay[1].InnerText.Trim() + " " + Time1[1].InnerText.Trim();
                }
                else
                {
                    MessageBox.Show("Огноо болон цагийн мэдээлэл олдсонгүй.");
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.ToString());
            }
            finally
            {
                // Алдаа гарсан үед энд хэрэглэх зүйлс
            }
        }


        private void simpleButton2_Click(object sender, EventArgs e)
        {
            /*ner.Text = "";
            zahialagch.Text = "";
            Tdugaar.Text = "";
            Udugaar.Text = "";
            ehuusver.Text = "";
            niittusuv.Text = "";
            onitusuv.Text = "";
            turul.Text = "";
            juram.Text = "";
            tsahim.Text = "";
            tuluv.Text = "";
            richEditControl1.Text = "";*/

            try
            {
                var HttpClient = new HttpClient();
                var html = HttpClient.GetStringAsync(URLtextedit.Text).Result;

                var htmlDocument = new HtmlAgilityPack.HtmlDocument();
                htmlDocument.LoadHtml(html);

                // Скрипт хэсгүүдийг устгах
                var scriptNodes = htmlDocument.DocumentNode.SelectNodes("//script");
                if (scriptNodes != null)
                {
                    foreach (var scriptNode in scriptNodes)
                    {
                        scriptNode.Remove(); // Скриптийг устгана
                    }
                }

                // HTML-ийн үлдсэн агуулгыг richEditControl1 дээр харуулах
                richEditControl1.Text = htmlDocument.DocumentNode.OuterHtml;
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.ToString());
            }

        }

        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("asdsad", "sdsfsdfsdfs", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                richEditControl1.SaveDocument("Урилга.docx", DevExpress.XtraRichEdit.DocumentFormat.OpenXml);
            }
            else
            {
                richEditControl1.SaveDocumentAs();
                System.Windows.Forms.MessageBox.Show("ok saved");
            }
        }

        dataSetFill dcd = new dataSetFill();
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            var data = new NameValueCollection();
            
            if (Tdugaar.Text != "" && Udugaar.Text != "" && ner.Text != "")
            {
                try
                {
                    data["comID"] = "999";
                    data["Tdugaar"] = Tdugaar.Text.Trim();
                    data["Udugaar"] = Udugaar.Text.Trim();
                    data["zahialagch"] = zahialagch.Text.Trim();
                    data["ner"] = ner.Text.Trim();
                    data["ehuusver"] = ehuusver.Text.Trim();
                    data["niittusuv"] = niittusuv.Text.Trim();
                    data["onitusuv"] = onitusuv.Text.Trim();
                    data["juram"] = juram.Text.Trim();
                    data["turul"] = turul.Text.Trim();
                    data["tuluv"] = tuluv.Text.Trim();
                    data["tsahim"] = tsahim.Text.Trim();
                    data["URL"] = URLtextedit.Text.Trim();
                    data["ognooZar"] = ognooZar.DateTime.ToString("yyyy-MM-dd");
                    data["ognooGet"] = ognooGet.Text;
                    data["ognooOpen"] = ognooOpen.Text;
                    data["bigtext"] = ""+Udugaar.Text.Trim().Replace("/", "-")+".rtf";
                    data["aimag"] = aimag.Text;
                    data["sumname"] = sumname.Text;

                   
                    richEditControl1.SaveDocument(""+Udugaar.Text.Trim().Replace("/","-")+".rtf", DocumentFormat.Rtf);
                    MessageBox.Show(dcd.exec_command("addtender", data));
                    t.label1.Text = t.label1.Text + "1";
                    this.Hide();
                }
                catch (Exception ee)
                {
                    MessageBox.Show(ee.ToString());
                }
                finally { }
            }
            else
            {
                MessageBox.Show("Өгөгдөл дутуу байна.");
            }
        }

        private void aimag_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                sumname.Properties.Items.Clear();
                switch (aimag.SelectedText.Trim())
                {
                    case "Улаанбаатар":
                        {
                            sumname.Properties.Items.Add("Сүхбаатар дүүрэг");
                            sumname.Properties.Items.Add("Чингэлтэй дүүрэг");
                            sumname.Properties.Items.Add("Баянзүрх дүүрэг");
                            sumname.Properties.Items.Add("Баянгол дүүрэг");
                            sumname.Properties.Items.Add("Хан-Уул дүүрэг");
                            sumname.Properties.Items.Add("Сонгино хайрхан дүүрэг");
                            sumname.Properties.Items.Add("Налайх дүүрэг");
                            sumname.Properties.Items.Add("Бага хангай дүүрэг");
                            sumname.Properties.Items.Add("Багануур дүүрэг");
                            break;
                        }
                    case "Өвөрхангай":
                        {
                            sumname.Properties.Items.Add("Арвайхээр сум");
                            sumname.Properties.Items.Add("Тарагт сум");
                            sumname.Properties.Items.Add("Нарийнтээл сум");
                            sumname.Properties.Items.Add("Богд сум");
                            sumname.Properties.Items.Add("Баянгол сум");
                            sumname.Properties.Items.Add("Баян-Өндөр сум");
                            sumname.Properties.Items.Add("Сант сум");
                            sumname.Properties.Items.Add("Төгрөг сум");
                            sumname.Properties.Items.Add("Хужирт сум");
                            sumname.Properties.Items.Add("Хархорин сум");
                            sumname.Properties.Items.Add("Бүрд сум");
                            sumname.Properties.Items.Add("Өлзийт сум");
                            sumname.Properties.Items.Add("Зүүнбаян-Улаан сум");
                            sumname.Properties.Items.Add("Баруунбаян-Улаан сум");
                            sumname.Properties.Items.Add("Бат-Өлзий сум");
                            sumname.Properties.Items.Add("Хайрхандулаан сум");
                            sumname.Properties.Items.Add("Уянга сум");
                            sumname.Properties.Items.Add("Гучин-Ус сум");
                            break;
                        }
                    case "Дархан":
                        {
                            sumname.Properties.Items.Add("Дархан сум");
                            sumname.Properties.Items.Add("Хонгор сум");
                            sumname.Properties.Items.Add("Шарын гол сум");
                            break;
                        }
                    case "Өмнөговь":
                        {
                            sumname.Properties.Items.Add("Даланзадгад сум");
                            sumname.Properties.Items.Add("Булган сум");
                            sumname.Properties.Items.Add("Ноён сум");
                            sumname.Properties.Items.Add("Манлай сум");
                            sumname.Properties.Items.Add("Номгон сум");
                            sumname.Properties.Items.Add("Хүрмэн сум");
                            sumname.Properties.Items.Add("Баяндалай сум");
                            sumname.Properties.Items.Add("Цогтцэций сум"); 
                            sumname.Properties.Items.Add("Цогт-Овоо сум");
                            sumname.Properties.Items.Add("Ханхонгор сум");
                            sumname.Properties.Items.Add("Ханбогд сум");
                            sumname.Properties.Items.Add("Сэврэй сум");
                            sumname.Properties.Items.Add("Гурван тэс сум");
                            sumname.Properties.Items.Add("Баян-Овоо сум");
                            sumname.Properties.Items.Add("Мандал-Овоо сум");
                            
                            break;
                        }
                    case "Сэлэнгэ":
                        {
                            sumname.Properties.Items.Add("Сүхбаатар сум");
                            sumname.Properties.Items.Add("Цагааннуур сум");
                            sumname.Properties.Items.Add("Баянгол сум");
                            sumname.Properties.Items.Add("Орхон сум");
                            sumname.Properties.Items.Add("Ерөө сум");
                            sumname.Properties.Items.Add("Алтанбулаг сум");
                            sumname.Properties.Items.Add("Баяндалай сум");
                            sumname.Properties.Items.Add("Жавхлант сум");
                            sumname.Properties.Items.Add("Зүүнбүрэн сум");
                            sumname.Properties.Items.Add("Баруунбүрэн сум");
                            sumname.Properties.Items.Add("Түшиг сум");
                            sumname.Properties.Items.Add("Хүдэр сум");
                            sumname.Properties.Items.Add("Хушаат сум");
                            sumname.Properties.Items.Add("Сант сум");
                            sumname.Properties.Items.Add("Сайхан сум");
                            sumname.Properties.Items.Add("Орхонтуул сум");
                            sumname.Properties.Items.Add("Мандал сум");

                            break;
                        }
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.ToString());
            }
            finally
            { }
        }
    }
}
