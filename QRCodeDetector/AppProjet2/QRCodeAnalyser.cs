using Android.Graphics;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace AppProjet2
{
    public static class QRCodeAnalyser
    {
        public static string ConvertToByte(Stream stream)
        {
            byte[] bytes;
            using (var memoryStream = new MemoryStream())
            {
                stream.CopyTo(memoryStream);
                bytes = memoryStream.ToArray();
            }

            string base64 = Convert.ToBase64String(bytes);
            return base64;
        }

        public static string ConvertToByte(byte[] byteStr)
        {
            string base64 = Convert.ToBase64String(byteStr);
            return base64;
        }

        public static byte[] ConvertBitmapToByteArray(Bitmap bitmap)
        {
            using (var stream = new MemoryStream())
            {
                bitmap.Compress(Bitmap.CompressFormat.Png, 0, stream);
                return stream.ToArray();
            }
        }

        public static async Task<string> DetectQRCode(string base64string)
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("User-Agent", ".NET Foundation Repository Reporter");
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            
            string WebAPIUrl = "http://51.77.137.170:4201/DetectQRCode";
            var uri = new Uri(WebAPIUrl);
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(@"{""image"" : """ + base64string);
                sb.Append(@"""}");

                string jsonData = sb.ToString();
                StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                var response = await httpClient.PostAsync(uri, content);

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    result = (string)JsonConvert.DeserializeObject(result);
                    return result;
                }
            }
            catch (Exception ex) { }
            return null;
        }
        public static async Task<string> GetConnection()
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("User-Agent", ".NET Foundation Repository Reporter");
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            string WebAPIUrl = "http://51.77.137.170:4201/";
            var uri = new Uri(WebAPIUrl);
            try
            {
                var response = await httpClient.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    result = (string)JsonConvert.DeserializeObject(result);
                    return result;
                }
            }
            catch (Exception ex) { }
            return null;
        }
    }
}