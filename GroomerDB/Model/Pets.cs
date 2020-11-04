using System;
using System.Collections.Generic;

namespace GroomerDB.Model
{
    public partial class Pets
    {
        public Pets()
        {
            Visit = new HashSet<Visit>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int? TypeId { get; set; }
        public DateTime? BirthDate { get; set; }
        public string Pattern { get; set; }
        public int OwnerId { get; set; }
        public string Note { get; set; }

        public virtual Owner Owner { get; set; }
        public virtual Type Type { get; set; }
        public virtual ICollection<Visit> Visit { get; set; }
    }
}
