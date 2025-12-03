using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ST
{
    public static class UserSession
    {
        public static int LoggedUserID { get; set; }
        public static string LoggedComID { get; set; }
        public static string LoggedComName { get; set; } // string
        public static string LoggedComRD { get; set; }   // string
        public static string LoggedComAbout { get; set; } // string
        public static string LoggedComCountry { get; set; } // string
        public static string LoggedComDomainName { get; set; } // string
        public static string LoggedComStatus { get; set; } // string
        public static string LoggedComPropic { get; set; } // string
        public static string LoggedComOgnoo { get; set; } // string (эсвэл DateTime)
        public static string LoggedComAddress { get; set; } // string
        public static string LoggedUserStatus { get; set; } // (Нэмэлт: Хэрэглэгчийн status)
        
    }
}