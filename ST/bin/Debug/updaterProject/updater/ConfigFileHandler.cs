using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

class ConfigFileHandler
{
    private static readonly string encryptionKey = AdjustKey("YourEncryptionKey");

    // Түлхүүрийг зөв уртад тохируулах
    private static string AdjustKey(string key)
    {
        if (key.Length < 16)
            return key.PadRight(16, '0'); // 16 тэмдэгт хүрэх хүртэл '0'-ээр дүүргэнэ
        if (key.Length > 16)
            return key.Substring(0, 16); // 16 тэмдэгт хүртэл хасна
        return key;
    }

    public static string Encrypt(string plainText)
    {
        byte[] key = Encoding.UTF8.GetBytes(encryptionKey);
        using (Aes aes = Aes.Create())
        {
            aes.Key = key;
            aes.IV = new byte[16]; // 16-byte IV, 0-оор дүүргэсэн

            using (MemoryStream ms = new MemoryStream())
            {
                using (CryptoStream cs = new CryptoStream(ms, aes.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    using (StreamWriter sw = new StreamWriter(cs))
                    {
                        sw.Write(plainText);
                    }
                }
                return Convert.ToBase64String(ms.ToArray());
            }
        }
    }

    public static string Decrypt(string cipherText)
    {
        byte[] key = Encoding.UTF8.GetBytes(encryptionKey);
        byte[] buffer = Convert.FromBase64String(cipherText);

        using (Aes aes = Aes.Create())
        {
            aes.Key = key;
            aes.IV = new byte[16]; // 16-byte IV, 0-оор дүүргэсэн

            using (MemoryStream ms = new MemoryStream(buffer))
            {
                using (CryptoStream cs = new CryptoStream(ms, aes.CreateDecryptor(), CryptoStreamMode.Read))
                {
                    using (StreamReader sr = new StreamReader(cs))
                    {
                        return sr.ReadToEnd();
                    }
                }
            }
        }
    }
}
