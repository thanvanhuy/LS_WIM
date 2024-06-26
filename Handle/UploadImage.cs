using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;


namespace Giatrican.Handle
{
    public class UploadImage
    {
        public static async Task<List<string>> UploadAsync(List<string> listOfFilePaths)
        {
            var list = new List<string>();
            var fileStreams = new List<FileStream>();

            try
            {
                using (var client = new HttpClient())
                {
                    var content = new MultipartFormDataContent();

                    foreach (var filePath in listOfFilePaths)
                    {
                        var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
                        fileStreams.Add(fileStream);
                        var fileContent = new StreamContent(fileStream);
                        content.Add(fileContent, "files", Path.GetFileName(filePath));
                    }

                    var apiUrl = "http://visioncorp.vn:8081/Live/UpdateImageFile";
                    var response = await client.PostAsync(apiUrl, content);

                    if (response.IsSuccessStatusCode)
                    {
                        var responseContent = await response.Content.ReadAsStringAsync();
                        //Console.WriteLine($"API Response: {responseContent}");

                        var returnedPaths = Newtonsoft.Json.JsonConvert.DeserializeObject<List<string>>(responseContent);
                        list.AddRange(returnedPaths);
                    }
                    else
                    {
                        Console.WriteLine($"API Request failed: {response.StatusCode}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception occurred: {ex.Message}");
            }
            finally
            {
                foreach (var fileStream in fileStreams)
                {
                    fileStream.Dispose();
                }
            }

            return list;
        }
    }
}
