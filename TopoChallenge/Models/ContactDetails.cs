using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TopoChallenge.Models
{
    public class ContactDetails
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public int PhoneNo { get; set; }
        public string EmailId { get; set; }
        public string Notes { get; set; }
        public string Category { get; set; }
        public string OrgName { get; set; }
        public string WebsiteUrl { get; set; }
        public string Tags { get; set; }

    }
}
