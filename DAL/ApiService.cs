using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.IO;

namespace DAL
{
    public class ApiService
    {
        private readonly HttpClient _httpClient;

        public ApiService()
        {
            _httpClient = new HttpClient();
        }

        // Hàm GET
        public async Task<string> GetAsync(string url)
        {
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode(); // Kiểm tra trạng thái HTTP
                return await response.Content.ReadAsStringAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi GET: {ex.Message}");
            }
        }

        // Hàm POST
        public async Task<string> PostAsync(string url, object data)
        {
            try
            {
                string jsonData = JsonSerializer.Serialize(data);
                var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await _httpClient.PostAsync(url, content);
                response.EnsureSuccessStatusCode(); // Kiểm tra trạng thái HTTP
                return await response.Content.ReadAsStringAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi POST: {ex.Message}");
            }
        }

        // Hàm PUT
        public async Task<string> PutAsync(string url, object data)
        {
            try
            {
                string jsonData = JsonSerializer.Serialize(data);
                var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await _httpClient.PutAsync(url, content);
                response.EnsureSuccessStatusCode(); // Kiểm tra trạng thái HTTP
                return await response.Content.ReadAsStringAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi PUT: {ex.Message}");
            }
        }

        // Hàm DELETE
        public async Task<string> DeleteAsync(string url)
        {
            try
            {
                HttpResponseMessage response = await _httpClient.DeleteAsync(url);
                response.EnsureSuccessStatusCode(); // Kiểm tra trạng thái HTTP
                return await response.Content.ReadAsStringAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi DELETE: {ex.Message}");
            }
        }

        // Hàm upload file
        public async Task<string> UploadFileAsync(string url, string filePath)
        {
            try
            {
                using (var content = new MultipartFormDataContent())
                {
                    // Đọc file từ đường dẫn
                    var fileContent = new ByteArrayContent(File.ReadAllBytes(filePath));
                    fileContent.Headers.Add("Content-Type", "application/octet-stream");

                    // Thêm file vào form data
                    content.Add(fileContent, "file", Path.GetFileName(filePath));

                    // Gửi yêu cầu POST
                    HttpResponseMessage response = await _httpClient.PostAsync(url, content);

                    // Kiểm tra kết quả
                    response.EnsureSuccessStatusCode(); // Kiểm tra trạng thái HTTP
                    return await response.Content.ReadAsStringAsync();
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi upload file: {ex.Message}");
            }
        }

        // Hàm upload nhiều file
        public async Task<string> UploadFilesAsync(string url, string[] filePaths)
        {
            try
            {
                using (var content = new MultipartFormDataContent())
                {
                    foreach (var filePath in filePaths)
                    {
                        if (File.Exists(filePath))
                        {
                            var fileContent = new ByteArrayContent(File.ReadAllBytes(filePath));
                            fileContent.Headers.Add("Content-Type", "application/octet-stream");

                            // Thêm mỗi file vào form data
                            content.Add(fileContent, "files", Path.GetFileName(filePath));
                        }
                    }

                    // Gửi yêu cầu POST
                    HttpResponseMessage response = await _httpClient.PostAsync(url, content);

                    // Kiểm tra kết quả
                    response.EnsureSuccessStatusCode(); // Kiểm tra trạng thái HTTP
                    return await response.Content.ReadAsStringAsync();
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi upload files: {ex.Message}");
            }
        }
    }
}
