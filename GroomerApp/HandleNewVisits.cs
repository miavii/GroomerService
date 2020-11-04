using System;
using GroomerApp.dto;
using GroomerDB.Model;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using Microsoft.WindowsAzure.Storage.Blob.Protocol;
using Newtonsoft.Json;

namespace GroomerApp
{
    public class HandleNewVisits
    {
        private readonly miadatabaseContext _groomerDbContext;

        public HandleNewVisits(miadatabaseContext groomerDbContext)
        {
            _groomerDbContext = groomerDbContext;
        }
        [FunctionName("HandleNewVisits")]
        public void Run([QueueTrigger("newvisitsqueue", Connection = "AzureWebJobsStorage")]string myQueueItem, ILogger log)
        {
            log.LogInformation($"C# Queue trigger function processed: {myQueueItem}");
            GroomerVisitDto gVisit = JsonConvert.DeserializeObject<GroomerVisitDto>(myQueueItem);
            Visit visit = new Visit
            {
                PetId = gVisit.PetId,
                ServiceId = gVisit.ServiceId,
                Time = Convert.ToDateTime(gVisit.Date + " " + gVisit.Time),
                Price = gVisit.Price,
                Paid = gVisit.Paid
            };

            _groomerDbContext.Visit.Add(visit);

            _groomerDbContext.SaveChanges();
            log.LogInformation($"Save visit: {visit.Id}");
        }
    }
}
