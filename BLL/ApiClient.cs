using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Runtime.InteropServices.WindowsRuntime;
using System.IO;


namespace BLL
{
    public class ApiClient
    {
        private static readonly HttpClient client = new HttpClient();
        //string baseUrl = "http://localhost:9090";
        string baseUrl = "http://web.chithanh.id.vn";

        public async Task<string> GenerateKeywordsAsync(long productId, string text)
        {
            string url = $"{baseUrl}/api/v1/generate-keywords?product_id={productId}&text={Uri.EscapeDataString(text)}";
            try
            {
                HttpResponseMessage response = await client.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();
                    return responseBody;
                }
                else
                {
                    return $"Error: {response.StatusCode} - {response.ReasonPhrase}";
                }
            }
            catch (Exception ex)
            {
                return $"Exception: {ex.Message}";
            }
        }

        public async Task<bool> DownloadPdfAsync(string outputPath, string billId)
        {
            var url = baseUrl + "/api/v1/view-pdf-bill/" + billId;
            using (var client = new HttpClient())
            {
                try
                {
                    var response = await client.GetAsync(url);
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsByteArrayAsync();
                        File.WriteAllBytes(outputPath, content);
                        return true;
                    }
                    return false;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
        }
    }
}
