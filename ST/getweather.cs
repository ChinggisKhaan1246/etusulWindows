using System;
using System.Net.Http;
using System.Threading.Tasks;
using ST;
using System.Windows.Forms;
public class getweather
{
    // HttpClient-ийг нэг удаа үүсгэж ашиглах
    private static readonly HttpClient client = new HttpClient();

    // Цаг агаарын мэдээллийг URL-ээс авах асинхрон функц
    public static async Task<string> GetWeatherDataAsync(string aimag, string sum, string date)
    {
        try
        {
            // URL параметрүүдийг кодлоход ашиглах
            string aimagEncoded = Uri.EscapeDataString(aimag);
            string sumEncoded = Uri.EscapeDataString(sum);
            string dateEncoded = Uri.EscapeDataString(date);

            BaseUrl Domainname = new BaseUrl();
            string url = string.Format(Domainname.GetUrl() + "api/getweather.php?aimag={0}&sum={1}&date={2}", aimagEncoded, sumEncoded, dateEncoded);

            MessageBox.Show(url.ToString());
            // Тайм-аут тохируулах (30 секунд)
            client.Timeout = TimeSpan.FromSeconds(30);

            // HTTP GET хүсэлт илгээх
            HttpResponseMessage response = await client.GetAsync(url);

            // Хариуг шалгах
            if (response.IsSuccessStatusCode)
            {
                // Хариуг string хэлбэрээр буцаах
                string responseBody = await response.Content.ReadAsStringAsync();
                return responseBody;
            }
            else
            {
                // Хэрвээ хариу буцаасангүй бол алдаа буцаах
                return string.Format("Алдаа гарлаа: Хариу авахад алдаа гарсан (код: {0})", response.StatusCode);
            }
        }
        catch (HttpRequestException ex)
        {
            // HttpRequestException-г ялган таньж, мэдээллийг буцаах
            return string.Format("HTTP хүсэлтийн алдаа: {0}", ex.Message);
        }
        catch (Exception ex)
        {
            // Бусад алдааны мэдээллийг буцаах
            return string.Format("Алдаа гарлаа: {0}", ex.Message);
        }
    }
}
