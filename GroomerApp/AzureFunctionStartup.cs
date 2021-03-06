﻿using GroomerApp;
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
            string connectionString = Environment.GetEnvironmentVariable("sqldb_connection");
            builder.Services.AddDbContext<miadatabaseContext>(option =>
            option.UseSqlServer(connectionString));
        }
    }
}
