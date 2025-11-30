using System;
using System.Data;

namespace ST
{
    public class baseinfo
    {
        public string userID { get; set; }
        public string userOvog { get; set; }
        public string userName { get; set; }
        public string userRD { get; set; }
        public string userAddress { get; set; }
        public string userAlbantushaal { get; set; }
        public string userPhone { get; set; }
        public string userPicture { get; set; }
        public string userStatus { get; set; }
        public string comName { get; set; }
        public string comID { get; set; }
        public string comAbout { get; set; }
        public string comAddress { get; set; }
        public string comEmail { get; set; }
        public string comFacebook { get; set; }
        public string comProfilePicture { get; set; }
        public string comStatus { get; set; }

        public baseinfo(int ID)
        {
            // dataSetFill объект үүсгэх
            dataSetFill ds = new dataSetFill();
            
            // "getbaseinfo" API-г ашиглаж, өгөгдөл унших
            DataTable dt = ds.gridFill("getbaseinfo", "id=" + ID);

            if (dt != null && dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];

                userID = row["id"] != DBNull.Value ? row["id"].ToString() : string.Empty;
                userOvog = row["ovog"] != DBNull.Value ? row["ovog"].ToString() : string.Empty;
                userName = row["ner"] != DBNull.Value ? row["ner"].ToString() : string.Empty;
                userRD = row["rd"] != DBNull.Value ? row["rd"].ToString() : string.Empty;
                userAddress = row["userAddress"] != DBNull.Value ? row["userAddress"].ToString() : string.Empty;
                userAlbantushaal = row["albantushaal"] != DBNull.Value ? row["albantushaal"].ToString() : string.Empty;
                userPhone = row["phone"] != DBNull.Value ? row["phone"].ToString() : string.Empty;
                userPicture = row["picture"] != DBNull.Value ? row["picture"].ToString() : string.Empty;
                userStatus = row["userStatus"] != DBNull.Value ? row["userStatus"].ToString() : string.Empty;
                comName = row["comName"] != DBNull.Value ? row["comName"].ToString() : string.Empty;
                comID = row["comID"] != DBNull.Value ? row["comID"].ToString() : string.Empty;
                comAbout = row["comAbout"] != DBNull.Value ? row["comAbout"].ToString() : string.Empty;
                comAddress = row["comAddress"] != DBNull.Value ? row["comAddress"].ToString() : string.Empty;
                comEmail = row["email"] != DBNull.Value ? row["email"].ToString() : string.Empty;
                comFacebook = row["facebook"] != DBNull.Value ? row["facebook"].ToString() : string.Empty;
                comProfilePicture = row["propic"] != DBNull.Value ? row["propic"].ToString() : string.Empty;
                comStatus = row["comStatus"] != DBNull.Value ? row["comStatus"].ToString() : string.Empty;
            }
        }
    }
}
