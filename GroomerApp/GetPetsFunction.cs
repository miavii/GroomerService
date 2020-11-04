using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using GroomerDB.Model;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Collections.Generic;
using GroomerApp.dto;
using CsvHelper;
using System.Globalization;

namespace GroomerApp
{
    public class GetPetsFunction
    {
        private readonly miadatabaseContext _groomerDbContext;

        public GetPetsFunction(miadatabaseContext groomerDbContext)
        {
            _groomerDbContext = groomerDbContext;
        }
        [FunctionName("GetPetsFunction")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string name = req.Query["name"];

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            name = name ?? data?.name;

            string responseMessage = name;

            List<Pets> pets = await _groomerDbContext.Pets
                .Include(p=> p.Owner)
                .Include(p=> p.Type)
                .Where(p => p.Name.Contains(name))
                .ToListAsync();

            var petDto = pets.Select(p => new PetDto
            {
                PetId = p.Id,
                Name = p.Name,
                Animal = p.Type.Animal,
                Breed = p.Type.Breed,
                Pattern = p.Pattern,
                OwnerFirstName = p.Owner.FirstName,
                OwnerLastName = p.Owner.LastName
            });

            await using (var writer = new StringWriter())
            {
                await using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
                {
                    csv.Configuration.RegisterClassMap<PetClassMap>();

                    await csv.WriteRecordsAsync(petDto);

                    await csv.FlushAsync();
                }

                responseMessage = writer.ToString();
            }

            return new OkObjectResult(responseMessage);
        }
    }
}
