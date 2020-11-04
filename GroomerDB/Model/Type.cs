using System;
using System.Collections.Generic;

namespace GroomerDB.Model
{
    public partial class Type
    {
        public Type()
        {
            Pets = new HashSet<Pets>();
        }

        public int Id { get; set; }
        public string Animal { get; set; }
        public string Breed { get; set; }

        public virtual ICollection<Pets> Pets { get; set; }
    }
}
