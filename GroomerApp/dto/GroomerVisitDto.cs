using System;
using System.Collections.Generic;
using System.Text;

namespace GroomerApp.dto
{
    class GroomerVisitDto
    {
        public int PetId { get; set; }
        public int ServiceId { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
        public decimal Price { get; set; }
        public Boolean Paid{ get; set; }
    }
}
