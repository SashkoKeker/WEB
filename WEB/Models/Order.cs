using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WEB.Models
{
    public class Order
    {
        public Guid OrderId { get; set; }


        public string OrderName { get; set; }
        public string OrderText { get; set; }
        public DateTime? OrderDateCreated { get; set; }

        public Guid OrderUserId { get; set; }
        public User user { get; set; }

        public int OrderTypeId { get; set; }
        public OrderType orderTypes { get; set; }


    }
}
