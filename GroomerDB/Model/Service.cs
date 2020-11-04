using System;
using System.Collections.Generic;

namespace GroomerDB.Model
{
    public partial class Service
    {
        public Service()
        {
            Visit = new HashSet<Visit>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Visit> Visit { get; set; }
    }
}
