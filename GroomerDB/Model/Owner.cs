using System;
using System.Collections.Generic;

namespace GroomerDB.Model
{
    public partial class Owner
    {
        public Owner()
        {
            Pets = new HashSet<Pets>();
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public virtual ICollection<Pets> Pets { get; set; }
    }
}
