using GroomerApp;
using GroomerDB.Model;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

[assembly: FunctionsStartup(typeof(AzureFunctionStartup))]

namespace GroomerApp
{
    public class AzureFunctionStartup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddDbContext<miadatabaseContext>(option =>
            option.UseSqlServer("Server=tcp:miaco.database.windows.net,1433;Initial Catalog=mia-database;Persist Security Info=False;User ID=magnificent;Password=Jaejoong01;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;")
            );
        }
    }
}
