using System;
using System.Globalization;
using System.IO;
using CsvHelper;
using GroomerApp.dto;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace GroomerApp
{
    public static class GetNewVisits
    {
        [FunctionName("getNewVisits")]
        public static void Run([BlobTrigger("dev/{name}", Connection = "AzureWebJobsStorage")]Stream myBlob, 
            string name,
            [Queue("newvisitsqueue", Connection = "AzureWebJobsStorage")]ICollector<string> outputQueue,
            ILogger log)
        {
            using (StreamReader reader = new StreamReader(myBlob))
            {

                log.LogInformation($"C# Blob trigger function Processed blob\n Name:{name} \n Size: {myBlob.Length} Bytes \n Added {name} to queue");
                
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    
                    var outputItem = "";
                    foreach ( GroomerVisitDto visit in csv.GetRecords<GroomerVisitDto>())
                    {
                        var json = JsonConvert.SerializeObject(visit);
                        log.LogInformation($"Json Content: \n {json}");
                        outputItem = json;
                        log.LogInformation($"Content to queue: \n {outputItem}");
                        outputQueue.Add(outputItem);
                    }
                }
            }

        }
    }
}
