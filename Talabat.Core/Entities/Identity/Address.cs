using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Entities.Identity
{
    public class Address 
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public String City { get; set; }
        public String Street { get; set; }
        public String Country { get; set; }
        public string AppUserId { get; set; }
        public AppUser User { get; set; }
    }
}
