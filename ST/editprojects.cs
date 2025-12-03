using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using System.Collections.Specialized;


namespace ST
{
    public partial class editprojects : Form
    {
        Form1 f;
        public editprojects(Form1 ff)
        {
            f = ff;
            InitializeComponent();
        }
        dataSetFill ds = new dataSetFill();
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            try
            {
                var data = new NameValueCollection();
                data["id"] = projectID.Text.Trim();
                data["projectName"] = projectName.Text.Trim();
                data["ognooG"] = ognooG.DateTime.ToString("yyyy-MM-dd");
                data["ognoo1"] = ognoo1.DateTime.ToString("yyyy-MM-dd");
                data["ognoo2"] = ognoo2.DateTime.ToString("yyyy-MM-dd");
                data["gereeNo"] = gereeNo.Text.Trim();
                data["locationP"] = locationP.Text.Trim();
                data["budget"] = budget.Text.Trim();
                data["zahialagch"] = zahialagch.Text.Trim();
                data["zahialagchT"] = zahialagchT.Text.Trim();
                data["author"] = author.Text.Trim();
                data["authorT"] = authorT.Text.Trim();
                data["injhai"] = injhai.Text.Trim();
                data["hyanagch"] = hyanagch.Text.Trim();
                data["aimag"] = aimag.Text.Trim();
                data["sumname"] = sumname.Text.Trim();
                data["baritsaa"] = baritsaa.Text.Trim();
                data["guitsetgel"] = spinEdit1.Value.ToString();
                data["Zphone"] = Zphone.Text.Trim();
                data["Aphone"] = Aphone.Text.Trim();
                data["Xphone"] = Xphone.Text.Trim();

                string response = ds.exec_command("editproject", data);

                if (response.Contains("амжилттай"))
                {
                    MessageBox.Show(response, "Амжилттай", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    f.FillGridOdoo();
                    this.Hide();
                }
                else
                {
                     MessageBox.Show(response, "Анхааруулга", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show("Алдаа гарлаа: " + ee.Message, "Алдаа", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void editprojects_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) // Enter товч дарагдсан эсэхийг шалгана
            {
                simpleButton1.PerformClick(); // simpleButton1_Click функцыг дуудаж байна
            }
        }

        private void aimag_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                sumname.Properties.Items.Clear();
                switch (aimag.SelectedText.Trim())
                {
                    case "Нийслэл":
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
                            sumname.Properties.Items.Add("Арвайхээр");
                            sumname.Properties.Items.Add("Тарагт");
                            sumname.Properties.Items.Add("Нарийнтээл");
                            sumname.Properties.Items.Add("Богд");
                            sumname.Properties.Items.Add("Баянгол");
                            sumname.Properties.Items.Add("Баян-Өндөр");
                            sumname.Properties.Items.Add("Сант");
                            sumname.Properties.Items.Add("Төгрөг");
                            sumname.Properties.Items.Add("Хужирт");
                            sumname.Properties.Items.Add("Хархорин");
                            sumname.Properties.Items.Add("Бүрд");
                            sumname.Properties.Items.Add("Өлзийт");
                            sumname.Properties.Items.Add("Зүүнбаян-Улаан");
                            sumname.Properties.Items.Add("Баруунбаян-Улаан");
                            sumname.Properties.Items.Add("Бат-Өлзий");
                            sumname.Properties.Items.Add("Хайрхандулаан");
                            sumname.Properties.Items.Add("Уянга");
                            sumname.Properties.Items.Add("Гучин-Ус");

                            break;
                        }
                    case "Дархан-Уул":
                        {
                            sumname.Properties.Items.Add("Дархан");
                            sumname.Properties.Items.Add("Хонгор");
                            sumname.Properties.Items.Add("Шарын гол");
                            break;
                        }
                    case "Өмнөговь":
                        {
                            sumname.Properties.Items.Add("Даланзадгад");
                            sumname.Properties.Items.Add("Булган");
                            sumname.Properties.Items.Add("Ноён");
                            sumname.Properties.Items.Add("Манлай");
                            sumname.Properties.Items.Add("Номгон");
                            sumname.Properties.Items.Add("Цогт-Овоо");
                            sumname.Properties.Items.Add("Хүрмэн");
                            sumname.Properties.Items.Add("Баяндалай");
                            sumname.Properties.Items.Add("Цогтцэций");
                            sumname.Properties.Items.Add("Ханхонгор");
                            sumname.Properties.Items.Add("Ханбогд");
                            sumname.Properties.Items.Add("Сэврэй");
                            sumname.Properties.Items.Add("Мандал-Овоо");
                            sumname.Properties.Items.Add("Гурвантэс");
                            break;
                        }
                    case "Орхон":
                        {
                            sumname.Properties.Items.Add("Баян-Өндөр");
                            sumname.Properties.Items.Add("Жаргалант");
                            break;
                        }
                    case "Хэнтий":
                        {
                            sumname.Properties.Items.Add("Хэрлэн");
                            sumname.Properties.Items.Add("Дархан");
                            sumname.Properties.Items.Add("Баян-Овоо");
                            sumname.Properties.Items.Add("Дадал");
                            sumname.Properties.Items.Add("Биндэр");
                            sumname.Properties.Items.Add("Галшар");
                            sumname.Properties.Items.Add("Дэлгэрхаан");
                            sumname.Properties.Items.Add("Өмнөдэлгэр");
                            sumname.Properties.Items.Add("Батноров");
                            sumname.Properties.Items.Add("Батширээт");
                            sumname.Properties.Items.Add("Баян-Адрага");
                            sumname.Properties.Items.Add("Баянмөнх");
                            sumname.Properties.Items.Add("Жаргалтхаан");
                            sumname.Properties.Items.Add("Норовлин");
                            sumname.Properties.Items.Add("Цэнхэрмандал");
                            sumname.Properties.Items.Add("Гурванбаян");
                            break;
                        }
                    case "Сэлэнгэ":
                        {
                            sumname.Properties.Items.Add("Сүхбаатар");
                            sumname.Properties.Items.Add("Баянгол");
                            sumname.Properties.Items.Add("Орхон");
                            sumname.Properties.Items.Add("Ерөө");
                            sumname.Properties.Items.Add("Дархан");
                            sumname.Properties.Items.Add("Орхонтуул");
                            sumname.Properties.Items.Add("Мандал");
                            sumname.Properties.Items.Add("Баруунбүрэн");
                            sumname.Properties.Items.Add("Зүүнбүрэн");
                            sumname.Properties.Items.Add("Зэлтэр");
                            sumname.Properties.Items.Add("Хүдэр");
                            sumname.Properties.Items.Add("Бугант");
                            sumname.Properties.Items.Add("Хушаат");

                            break;
                        }
                    case "Баянхонгор":
                        {
                            sumname.Properties.Items.Add("Баянбулаг");
                            sumname.Properties.Items.Add("Жаргалант");
                            sumname.Properties.Items.Add("Галуут");
                            sumname.Properties.Items.Add("Баянхонгор");
                            sumname.Properties.Items.Add("Баянцагаан");
                            sumname.Properties.Items.Add("Шинэжинст");
                            sumname.Properties.Items.Add("Богд");
                            sumname.Properties.Items.Add("Баянговь");
                            sumname.Properties.Items.Add("Баацагаан");
                            sumname.Properties.Items.Add("Гурванбулаг");
                            sumname.Properties.Items.Add("Баянлиг");
                            sumname.Properties.Items.Add("Баян-Өндөр");
                            sumname.Properties.Items.Add("Бөмбөгөр");
                            sumname.Properties.Items.Add("Бууцагаан");
                            sumname.Properties.Items.Add("Жинст");
                            sumname.Properties.Items.Add("Хүрээ марал");
                            sumname.Properties.Items.Add("Эрдэнэцогт");
                            sumname.Properties.Items.Add("Шаргалжуут");

                            break;
                        }

                    case "Архангай":
                        {
                            sumname.Properties.Items.Add("Тариат");
                            sumname.Properties.Items.Add("Эрдэнэмандал");
                            sumname.Properties.Items.Add("Төвшрүүлэх");
                            sumname.Properties.Items.Add("Эрдэнэбулган");
                            sumname.Properties.Items.Add("Батцэнгэл");
                            sumname.Properties.Items.Add("Булган");
                            sumname.Properties.Items.Add("Жаргалант");
                            sumname.Properties.Items.Add("Өгийнуур");
                            sumname.Properties.Items.Add("Өндөр-Улаан");
                            sumname.Properties.Items.Add("Хайрхан");
                            sumname.Properties.Items.Add("Хангай");
                            sumname.Properties.Items.Add("Хашаат");
                            sumname.Properties.Items.Add("Цэцэрлэг");
                            sumname.Properties.Items.Add("Чулуут");
                            sumname.Properties.Items.Add("Цахир");

                            break;
                        }
                    case "Булган":
                        {
                            sumname.Properties.Items.Add("Тэшиг");
                            sumname.Properties.Items.Add("Хутаг-Өндөр");
                            sumname.Properties.Items.Add("Сэлэнгэ");
                            sumname.Properties.Items.Add("Тогод");
                            sumname.Properties.Items.Add("Булган");
                            sumname.Properties.Items.Add("Гурванбулаг");
                            sumname.Properties.Items.Add("Баян-Агт");
                            sumname.Properties.Items.Add("Бугат");
                            sumname.Properties.Items.Add("Бүрэгхангай");
                            sumname.Properties.Items.Add("Дашинчилэн");
                            sumname.Properties.Items.Add("Сайхан");
                            sumname.Properties.Items.Add("Хангал");
                            sumname.Properties.Items.Add("Хишиг-Өндөр");
                            sumname.Properties.Items.Add("Баяннуур");
                            sumname.Properties.Items.Add("Рашаант");

                            break;
                        }
                    case "Төв":
                        {
                            sumname.Properties.Items.Add("Баянчандмань");
                            sumname.Properties.Items.Add("Угтаалцайдам");
                            sumname.Properties.Items.Add("Мөнгөнморьт");
                            sumname.Properties.Items.Add("Жаргалант");
                            sumname.Properties.Items.Add("Эрдэнэсант");
                            sumname.Properties.Items.Add("Зуунмод");
                            sumname.Properties.Items.Add("Баян");
                            sumname.Properties.Items.Add("Лүн");
                            sumname.Properties.Items.Add("Бүрэн");
                            sumname.Properties.Items.Add("Баян-Өнжүүл");
                            sumname.Properties.Items.Add("Алтанбулаг");
                            sumname.Properties.Items.Add("Цээл");
                            sumname.Properties.Items.Add("Батсүмбэр");
                            sumname.Properties.Items.Add("Баянжаргалан");
                            sumname.Properties.Items.Add("Борнуур");
                            sumname.Properties.Items.Add("Дэлгэрхаан");
                            sumname.Properties.Items.Add("Заамар");
                            sumname.Properties.Items.Add("Өндөрширээт");
                            sumname.Properties.Items.Add("Аргалтант");
                            sumname.Properties.Items.Add("Эрдэнэ");
                            sumname.Properties.Items.Add("Сэргэлэн");
                            break;
                        }
                    case "ГовьСүмбэр":
                        {
                            sumname.Properties.Items.Add("Сүмбэр");

                            break;
                        }
                    case "Дорнод":
                        {
                            sumname.Properties.Items.Add("Баян-Уул");
                            sumname.Properties.Items.Add("Дашбалбар");
                            sumname.Properties.Items.Add("Чойбалсан");
                            sumname.Properties.Items.Add("Халхгол");
                            sumname.Properties.Items.Add("Матад");
                            sumname.Properties.Items.Add("Баяндун");
                            sumname.Properties.Items.Add("Баянтүмэн");
                            sumname.Properties.Items.Add("Хэрлэн");
                            sumname.Properties.Items.Add("Булган");
                            sumname.Properties.Items.Add("Гурванзагал");
                            sumname.Properties.Items.Add("Сэргэлэн");
                            sumname.Properties.Items.Add("Хөлөнбуйр");
                            sumname.Properties.Items.Add("Цагаан-Овоо");
                            sumname.Properties.Items.Add("Чулуунхороот");
                            break;
                        }
                    case "Сүхбаатар":
                        {
                            sumname.Properties.Items.Add("Баруун-Урт");
                            sumname.Properties.Items.Add("Эрдэнэцагаан");
                            sumname.Properties.Items.Add("Баяндэлгэр");
                            sumname.Properties.Items.Add("Дарьганга");
                            sumname.Properties.Items.Add("Асгат");
                            sumname.Properties.Items.Add("Мөнххаан");
                            sumname.Properties.Items.Add("Наран");
                            sumname.Properties.Items.Add("Онгон");
                            sumname.Properties.Items.Add("Сүхбаатар");
                            sumname.Properties.Items.Add("Түвшинширээ");
                            sumname.Properties.Items.Add("Түмэнцогт");
                            sumname.Properties.Items.Add("Уулбаян");
                            sumname.Properties.Items.Add("Халзан");

                            break;
                        }
                    case "Дундговь":
                        {
                            sumname.Properties.Items.Add("Цагаандэлгэр");
                            sumname.Properties.Items.Add("Эрдэнэдалай");
                            sumname.Properties.Items.Add("Сайхан-Овоо");
                            sumname.Properties.Items.Add("Сайнцагаан");
                            sumname.Properties.Items.Add("Говь-Угтаал");
                            sumname.Properties.Items.Add("Гурвансайхан");
                            sumname.Properties.Items.Add("Адаацаг");
                            sumname.Properties.Items.Add("Баянжаргалан");
                            sumname.Properties.Items.Add("Дэлгэрхангай");
                            sumname.Properties.Items.Add("Дэрэн");
                            sumname.Properties.Items.Add("Луус");
                            sumname.Properties.Items.Add("Өлзийт");
                            sumname.Properties.Items.Add("Өндөршил");
                            sumname.Properties.Items.Add("Хулд");

                            break;
                        }
                    case "Дорноговь":
                        {
                            sumname.Properties.Items.Add("Мандах");
                            sumname.Properties.Items.Add("Дэлгэрэх");
                            sumname.Properties.Items.Add("Сайншанд");
                            sumname.Properties.Items.Add("Эрдэнэ");
                            sumname.Properties.Items.Add("Замын-Үүд");
                            sumname.Properties.Items.Add("Хөвсгөл");
                            sumname.Properties.Items.Add("Айраг");
                            sumname.Properties.Items.Add("Алтанширээ");
                            sumname.Properties.Items.Add("Иххэт");
                            sumname.Properties.Items.Add("Өргөн");
                            sumname.Properties.Items.Add("Сайхандулаан");
                            sumname.Properties.Items.Add("Улаанбадрах");
                            sumname.Properties.Items.Add("Хатанбулаг");
                            break;
                        }
                    case "Баян-Өлгий":
                        {
                            sumname.Properties.Items.Add("Баяннуур");
                            sumname.Properties.Items.Add("Ногооннуур");
                            sumname.Properties.Items.Add("Өлгий");
                            sumname.Properties.Items.Add("Алтай");
                            sumname.Properties.Items.Add("Дэлүүн");
                            sumname.Properties.Items.Add("Булган");
                            sumname.Properties.Items.Add("Буянт");
                            sumname.Properties.Items.Add("Сагсай");
                            sumname.Properties.Items.Add("Толбо");
                            sumname.Properties.Items.Add("Улаанхус");
                            sumname.Properties.Items.Add("Цэнгэл");
                            break;
                        }
                    case "Увс":
                        {
                            sumname.Properties.Items.Add("Өндөрхангай");
                            sumname.Properties.Items.Add("Наранбулаг");
                            sumname.Properties.Items.Add("Малчин");
                            sumname.Properties.Items.Add("Ховд");
                            sumname.Properties.Items.Add("Тэс");
                            sumname.Properties.Items.Add("Улаангом");
                            sumname.Properties.Items.Add("Баруунтуруун");
                            sumname.Properties.Items.Add("Өмнөговь");
                            sumname.Properties.Items.Add("Завхан");
                            sumname.Properties.Items.Add("Бөхмөрөн");
                            sumname.Properties.Items.Add("Давст");
                            sumname.Properties.Items.Add("Зүүнговь");
                            sumname.Properties.Items.Add("Зүүнхангай");
                            sumname.Properties.Items.Add("Өлгий");
                            sumname.Properties.Items.Add("Сагил");
                            sumname.Properties.Items.Add("Түргэн");
                            sumname.Properties.Items.Add("Хяргас");
                            sumname.Properties.Items.Add("Цагаанхайрхан");

                            break;
                        }
                    case "Ховд":
                        {
                            sumname.Properties.Items.Add("Жаргалант");
                            sumname.Properties.Items.Add("Мөнххайрхан");
                            sumname.Properties.Items.Add("Зэрэг");
                            sumname.Properties.Items.Add("Булган");
                            sumname.Properties.Items.Add("Мөст");
                            sumname.Properties.Items.Add("Чандмань");
                            sumname.Properties.Items.Add("Цэцэг");
                            sumname.Properties.Items.Add("Манхан");
                            sumname.Properties.Items.Add("Ховд");
                            sumname.Properties.Items.Add("Алтай");
                            sumname.Properties.Items.Add("Дарви");
                            sumname.Properties.Items.Add("Дуут");
                            sumname.Properties.Items.Add("Мянгад");
                            sumname.Properties.Items.Add("Үенч");
                            sumname.Properties.Items.Add("Эрдэнэбүрэн");
                            sumname.Properties.Items.Add("Дөргөн");
                            break;
                        }
                    case "Завхан":
                        {
                            sumname.Properties.Items.Add("Дөрвөлжин");
                            sumname.Properties.Items.Add("Нөмрөг");
                            sumname.Properties.Items.Add("Баянтэс");
                            sumname.Properties.Items.Add("Цэцэн-Уул");
                            sumname.Properties.Items.Add("Тосонцэнгэл");
                            sumname.Properties.Items.Add("Завханмандал");
                            sumname.Properties.Items.Add("Улиастай");
                            sumname.Properties.Items.Add("Отгон");
                            sumname.Properties.Items.Add("Алдархаан");
                            sumname.Properties.Items.Add("Баянхайрхан");
                            sumname.Properties.Items.Add("Идэр");
                            sumname.Properties.Items.Add("Их-Уул");
                            sumname.Properties.Items.Add("Сантмаргац");
                            sumname.Properties.Items.Add("Сонгино");
                            sumname.Properties.Items.Add("Түдэвтэй");
                            sumname.Properties.Items.Add("Тэлмэн");
                            sumname.Properties.Items.Add("Тэс");
                            sumname.Properties.Items.Add("Ургамал");
                            sumname.Properties.Items.Add("Цагаанхайрхан");
                            sumname.Properties.Items.Add("Цагаанчулуут");
                            sumname.Properties.Items.Add("Шилүүстэй");
                            sumname.Properties.Items.Add("Эрдэнэхайрхан");
                            sumname.Properties.Items.Add("Яруу");
                            break;
                        }
                    case "Говь-Алтай":
                        {
                            sumname.Properties.Items.Add("Хөхморьт");
                            sumname.Properties.Items.Add("Тонхил");
                            sumname.Properties.Items.Add("Бугат");
                            sumname.Properties.Items.Add("Есөн булаг");
                            sumname.Properties.Items.Add("Алтай");
                            sumname.Properties.Items.Add("Цогт");
                            sumname.Properties.Items.Add("Баян-Уул");
                            sumname.Properties.Items.Add("Бигэр");
                            sumname.Properties.Items.Add("Дарви");
                            sumname.Properties.Items.Add("Дэлгэр");
                            sumname.Properties.Items.Add("Жаргалан");
                            sumname.Properties.Items.Add("Тайшир");
                            sumname.Properties.Items.Add("Төгрөг");
                            sumname.Properties.Items.Add("Халиун");
                            sumname.Properties.Items.Add("Цээл");
                            sumname.Properties.Items.Add("Чандмань");
                            sumname.Properties.Items.Add("Шарга");
                            sumname.Properties.Items.Add("Эрдэнэ");
                            sumname.Properties.Items.Add("Дэлгэр");

                            break;
                        }
                }
                sumname.Properties.DropDownRows = sumname.Properties.Items.Count;
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.ToString());
            }
            finally
            { }
        }

        private void editprojects_Load(object sender, EventArgs e)
        {
            try
            {
                aimag.Properties.DropDownRows = aimag.Properties.Items.Count;
                spinEdit1.Properties.MinValue = 0;
                spinEdit1.Properties.MaxValue = 100;
                spinEdit1.Properties.IsFloatValue = false;
            }
            catch (Exception)
            {
                MessageBox.Show("алдаа items count");
            }
            finally { }
        }

    }
}
