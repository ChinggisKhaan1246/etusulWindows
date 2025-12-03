using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ST
{
    public static class OvogHelper
    {
        public static string OvogNer(string ovog, string ner)
        {
            if (string.IsNullOrWhiteSpace(ovog) || string.IsNullOrWhiteSpace(ner))
                return "";

            string firstLetter = ovog.Substring(0, 1).ToUpper();

            return firstLetter + "." + ner;
        }
    }

}
