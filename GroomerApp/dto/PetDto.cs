using System;
using System.Collections.Generic;
using System.Text;

namespace GroomerApp.dto
{
    public class PetDto
    {
        public int PetId { get; set; }
        public string Name { get; set; }
        public string Animal { get; set; }
        public string Breed { get; set; }
        public string Pattern { get; set; }
        public string OwnerFirstName { get; set; }
        public string OwnerLastName { get; set; }
    }
}
