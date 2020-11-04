using CsvHelper.Configuration;
using GroomerApp.dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace GroomerApp
{
    public class PetClassMap : ClassMap<PetDto>
    {
        public PetClassMap()
        {
            Map(m => m.PetId).Index(0);
            Map(m => m.Name).Index(1);
            Map(m => m.Animal).Index(2);
            Map(m => m.Breed).Index(3);
            Map(m => m.Pattern).Index(4);
            Map(m => m.OwnerFirstName).Index(5);
            Map(m => m.OwnerLastName).Index(6);
        }
    }
}
