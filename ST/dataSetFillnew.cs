using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Net;
using Newtonsoft.Json;
using System.Collections.Specialized;
using System.Windows.Forms;
using System.IO;
using System.Globalization;

namespace ST
{
    class dataSetFillnew
    {
        private BaseUrl Url = new BaseUrl();

        // ✅ GET болон POST хүсэлтүүдийг дэмжих үндсэн функц
        public DataTable getData(string url, Dictionary<string, string> parameters = null, string method = "GET")
        {
            try
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                using (var wb = new WebClient())
                {
                    wb.Encoding = Encoding.UTF8;
                    string fullUrl = string.Concat(Url.GetUrl(), "api/", url, ".php");

                    if (parameters != null && method == "GET")
                    {
                        List<string> queryParams = new List<string>();
                        foreach (KeyValuePair<string, string> param in parameters)
                        {
                            queryParams.Add(string.Concat(param.Key, "=", Uri.EscapeDataString(param.Value)));
                        }
                        fullUrl = string.Concat(fullUrl, "?", string.Join("&", queryParams));
                    }

                    string responseInString = (method == "POST") ? Encoding.UTF8.GetString(wb.UploadValues(fullUrl, "POST", new NameValueCollection())) : wb.DownloadString(fullUrl);

                    if (responseInString == null || responseInString.Trim() == "" || responseInString.Trim() == "[]" || responseInString.Trim().ToLower().Contains("nodata"))
                    {
                        return null;
                    }
                    // ✅ JSON-ийг эхлээд List<Dictionary<string, object>> болгож хөрвүүлэх
                    var list = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(responseInString);

                    if (list == null || list.Count == 0)
                        return null;

                    // ✅ DataTable үүсгэх (эхний мөрний багануудыг харгалзан автоматаар үүсгэнэ)
                    DataTable dt = new DataTable();

                    foreach (var key in list[0].Keys)
                    {
                        dt.Columns.Add(key, typeof(string)); // Бүх багануудыг string төрлөөр авах
                    }

                    foreach (var dict in list)
                    {
                        DataRow row = dt.NewRow();
                        foreach (var key in dict.Keys)
                        {
                            if (dict[key] != null)
                            {
                                string value = dict[key].ToString();
                                decimal numValue;
                                if (decimal.TryParse(value, NumberStyles.AllowThousands, CultureInfo.InvariantCulture, out numValue))
                                {
                                    row[key] = numValue.ToString("N0", CultureInfo.InvariantCulture); // ✅ Мөнгөн дүнг форматлах (1000000 -> 1,000,000)
                                }
                                else
                                {
                                    row[key] = value;
                                }
                            }
                            else
                            {
                                row[key] = "хоосон";
                            }
                        }
                        dt.Rows.Add(row);
                    }

                    return dt;
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show("Алдаа: DataSetFillnew: " + ee.Message);
                return null;
            }
        }

        // ✅ API руу `POST` хүсэлт явуулж, хариу буцаах функц
        public string execCommand(string url, NameValueCollection data)
        {
            try
            {
                string mainurl = Url.GetUrl();
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                using (var wb = new WebClient())
                {
                    var response = wb.UploadValues(string.Concat(Url.GetUrl(), "api/", url, ".php"), "POST", data);
                    return Encoding.UTF8.GetString(response);
                }
            }
            catch (Exception ee)
            {
                return "Алдаа гарлаа: " + ee.ToString();
            }
        }
    }
}
