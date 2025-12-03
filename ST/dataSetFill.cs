using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Net;
using Newtonsoft.Json;
using System.Collections.Specialized;
using System.Windows;
using System.IO;
namespace ST
{
    class dataSetFill
    {
        public string mainurl;
        BaseUrl Url = new BaseUrl();
        public DataTable gridFill(string url, string param = null)
        {
            
            if (param != null)
            {
                param = "?" + param;
            }
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls;
            ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
           
            var data = new NameValueCollection();
            try
            {
                using (var wb = new WebClient())
                {
                    mainurl = Url.GetUrl();
                    
                    var response = wb.UploadValues(mainurl+"api/" + url + ".php" + param, "POST", data);
                    string json = Encoding.UTF8.GetString(response);
                    if (json == null || json.Trim() == "" || json.Trim() == "[]" || json.Trim().ToLower().Contains("nodata") || json.Trim().ToLower().Contains("error"))
                    {
                        return null;
                    }
                    return (DataTable)JsonConvert.DeserializeObject(json, (typeof(DataTable)));
                }
            }
            catch (Exception)
            {
                return new DataTable();
            }
            finally { }
        }

        public string exec_command(string url, NameValueCollection data)
        {
            try
            {
                string mainurl = Url.GetUrl();
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls;
                ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
                using (var wb = new WebClient())
                {
                   // MessageBox.Show(mainurl);
                    var response = wb.UploadValues(Url.GetUrl()+"api/" + url + ".php", "POST", data);
                    string responseInString = Encoding.UTF8.GetString(response);
                    
                    return responseInString;
                }
            }
            catch (Exception ee) {
                return "Алдаа гарлаа" + ee.ToString();
            }
        }

        
    }
}
