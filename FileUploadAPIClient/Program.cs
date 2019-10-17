using System;
using System.IO;
using System.Net.Http;

namespace FileUploadAPIClient
{
    class Program
    {
        static void Main(string[] args)
        {
            FileTransfer();
        }

        static void FileTransfer()
        {
            FileInfo file = new FileInfo(@"C:\FileTest\CLI.pdf");
            using (var client = new HttpClient())
            {
                using (var content = new MultipartFormDataContent())
                {
                    var fileStream = File.Open(@"C:\FileTest\CLI.pdf", FileMode.Open);
                    content.Add(new StreamContent(fileStream), "\"file\"", string.Format("\"{0}\"", file.Name));
                    //var requestUri = "https://localhost:44319/api/v1/filestream";
                    var requestUri = "http://localhost:5000/api/v1/filestream";
                    var result = client.PostAsync(requestUri, content).Result;
                    if (result.StatusCode == System.Net.HttpStatusCode.Created)
                    {
                        var body = result.Content.ReadAsStringAsync().Result;
                    }
                }
            }
        }
    }
}
