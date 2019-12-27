using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WEB.DTO
{
    public class ContactDTO
    {
        public int ContactId { get; set; }
        public string ContactName { get; set; }
        public DateTime? ContactUserCreated { get; set; }

    }
}
