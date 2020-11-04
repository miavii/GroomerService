using System;
using System.Data.SqlClient;
using System.Configuration;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using System.IO;
using System.Text;
using Azure.Storage.Blobs;
using GroomerDB.Model;

namespace PetGroomerApp
{
    public class GetPetsFunction
    {
        private readonly GroomerDBContext _groomerDBContext;

        public GetPetsFunction(GroomerDBContext groomerDBContext)
        {
            _groomerDBContext = groomerDBContext;
        }

        [FunctionName("getPets")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] 
            HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            string name = req.Query["name"];
            name = name ?? data?.name;

            string responseMessage = name;
            //var storageConnStr = Environment.GetEnvironmentVariable("AZURE_STORAGE_CONNECTION_STRING");
            // string fileContent = csv.ToString();

            // create a string
            //var newLine = string.Format("{0},{1},{2},{3},{4},{5},{6}", id, name, animal, breed, ownerName, pattern, note);
            // csv.AppendLine(newLine);
            log.LogInformation("Creating BlobService Client");
                    // Create a BlobServiceClient object which will be used to fetch the container client
                   // BlobServiceClient blobServiceClient = new BlobServiceClient();

                    string containerName = "groomertest";

                    // Return the container client object
                    //BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(containerName);
            
                    // Generate a unique name for the blob
                    string fileName = "petGroomer"+ DateTime.Now.ToString()+".csv";

                    // Get a reference to the blob. If the blob already exists, it will be overwritten
                    BlobClient blobClient = containerClient.GetBlobClient(fileName);
                    Console.WriteLine("Uploading to Blob storage as blob:\n\t {0}\n", blobClient.Uri);

                    // Use MemoryStream to add the data into the blob and upload to the container
                    //using MemoryStream uploadFileStream = new MemoryStream(Encoding.UTF8.GetBytes(fileContent));
                    //await blobClient.UploadAsync(uploadFileStream, true);
                    //uploadFileStream.Close();
            return new OkObjectResult(fileName);
        }
    }
}
