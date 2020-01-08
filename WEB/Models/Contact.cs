using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WEB.Models
{
    public class Contact
    {
        public int ContactId { get; set; }
        public string ContactName { get; set; }
        public DateTime? ContactUserCreated { get; set; }

        public List<Chat> chats { get; set; }

        public string UserId { get; set; }
        public User userss { get; set; }

        public string ContactUserId { get; set; }
        public User useres { get; set; }

    }
}
