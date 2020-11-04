using System;
using System.Collections.Generic;

namespace GroomerDB.Model
{
    public partial class Visit
    {
        public int Id { get; set; }
        public int PetId { get; set; }
        public int ServiceId { get; set; }
        public DateTime? Time { get; set; }
        public decimal Price { get; set; }
        public bool Paid { get; set; }

        public virtual Pets Pet { get; set; }
        public virtual Service Service { get; set; }
    }
}
