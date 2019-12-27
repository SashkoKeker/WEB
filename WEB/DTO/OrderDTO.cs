using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WEB.DTO
{
    public class OrderDTO
    {
        public Guid OrderId { get; set; }
        public string OrderName { get; set; }
        public string OrderText { get; set; }
        public DateTime? OrderDateCreated  { get; set; }
    }
}
