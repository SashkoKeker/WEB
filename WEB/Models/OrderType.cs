using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WEB.Models
{
    public class OrderType
    {
        public int OrderTypeId { get; set; }
        public string OrderTypes { get; set; }

        public List<Order> Orders { get; set; }
    }
}
