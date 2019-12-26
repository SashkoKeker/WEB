using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WEB.Models
{
    public class Chat
    {

        public string ChatText { get; set; }
        public Guid ChatUserId { get; set; }
        public Guid ChatToUserId { get; set; }
        public DateTime? ChatTextTime { get; set; }

        public int ChatId { get; set; }
        public Contact contacs { get; set; }
    }
}
