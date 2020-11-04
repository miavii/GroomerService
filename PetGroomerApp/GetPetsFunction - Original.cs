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

namespace PetGroomerApp
{
    public static class GetPetsFunction
    {
        [FunctionName("getPets")]
        public static async void Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] 
            HttpRequest req,
            ILogger log)
        {
            SqlDataReader rdr = null;
            var csv = new System.Text.StringBuilder();

            var str = Environment.GetEnvironmentVariable("sqldb_connection");

            using (var conn = new SqlConnection(str))
            {
                conn.Open();

                // Query Text
                var text = "select  pets.id, pets.name, ty.animal as 'animal', ty.breed as 'breed', ow.first_name as 'first name', ow.last_name as 'last name', pets.pattern, pets.note from pets join[type] as ty on(pets.[type_id] = ty.[id]) join[owner] as ow on(pets.owner_id = ow.id);";
                
                using (var cmd = new SqlCommand(text, conn))
                {
                    // Execute the command 
                    rdr = cmd.ExecuteReader();

                   while (rdr.Read())
                    {
                        // get the results of each column
                        int id = (int)rdr["id"];
                        string name = (string)rdr["name"];
                        string animal = (string)rdr["animal"];
                        string breed = (string)rdr["breed"];
                        string ownerName = (string)rdr["first name"] +" "+ (string)rdr["last name"];
                        string pattern = (string)rdr["pattern"];
                        string note = (string)rdr["note"];

                        // create a string
                        var newLine = string.Format("{0},{1},{2},{3},{4},{5},{6}", id, name, animal, breed, ownerName, pattern, note);
                        csv.AppendLine(newLine);
                    }
                   
                }
                log.LogInformation(csv.ToString());
                log.LogInformation("C# HTTP trigger function fetched pets.");

            }
            var storageConnStr = Environment.GetEnvironmentVariable("AZURE_STORAGE_CONNECTION_STRING");
            string fileContent = csv.ToString();


            log.LogInformation("Creating BlobService Client");
            // Create a BlobServiceClient object which will be used to fetch the container client
            BlobServiceClient blobServiceClient = new BlobServiceClient(storageConnStr);

            string containerName = "groomertest";

            // Return the container client object
            BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(containerName);
            
            // Generate a unique name for the blob
            string fileName = "petGroomer"+ DateTime.Now.ToString()+".csv";

            // Get a reference to the blob. If the blob already exists, it will be overwritten
            BlobClient blobClient = containerClient.GetBlobClient(fileName);
            Console.WriteLine("Uploading to Blob storage as blob:\n\t {0}\n", blobClient.Uri);

            // Use MemoryStream to add the data into the blob and upload to the container
            using MemoryStream uploadFileStream = new MemoryStream(Encoding.UTF8.GetBytes(fileContent));
            await blobClient.UploadAsync(uploadFileStream, true);
            uploadFileStream.Close();

        }
    }
}
