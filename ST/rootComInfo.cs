using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data; // Энийг нэмэх шаардлагатай

namespace ST
{
    class rootComInfo
    {
        public string domainname { get; set; }
        public string comID { get; set; }

        public rootComInfo(string RD) // Constructor нэр class-тай ижил байх ёстой
        {
            // dataSetFill объект үүсгэх
            dataSetFill ds = new dataSetFill();

            // "getcompany" API-г ашиглаж, өгөгдөл унших
            DataTable dt = ds.gridFill("getcompany", "RD=" + RD);

            if (dt != null && dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];

                domainname = row["domainname"] != DBNull.Value ? row["domainname"].ToString() : string.Empty;
                comID = row["comID"] != DBNull.Value ? row["comID"].ToString() : string.Empty;
            }
        }
    }
}