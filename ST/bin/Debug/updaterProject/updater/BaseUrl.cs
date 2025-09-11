using System;
using System.IO;
using System.Windows.Forms; // MessageBox ашиглахад шаардлагатай.

namespace ST
{
    class BaseUrl
    {
        public string MainUrl { get; private set; }
        

        public BaseUrl()
        {
            string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "config.txt").Trim();

            if (File.Exists(filePath))
            {
                string line = File.ReadAllText(filePath).Trim();
                if (!string.IsNullOrEmpty(line))
                {
                    string decryptedUrl = ConfigFileHandler.Decrypt(line);
                    MainUrl = decryptedUrl;//decryptedUrl; // URL-г хадгална.
                    // MessageBox.Show(MainUrl);
                }
                else
                {
                    MessageBox.Show("Файлын мөр хоосон байна.");
                }
            }
            else
            {
                MessageBox.Show("Файл олдсонгүй: " + filePath);
            }
        }

        public string GetUrl()
        {
            return MainUrl; // URL-г буцаана.
        }

        public string GetUpdateUrl()
        {
            return MainUrl+"/downloads/updates/"; // URL-г буцаана.
        }
    }
}
