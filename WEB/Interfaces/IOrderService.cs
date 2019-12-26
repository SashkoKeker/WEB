using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WEB.Models;

namespace WEB.Interfaces
{
    interface IOrderService
    {
        IQueryable<Order> GetAll();
        Order GetById(Guid id);
        Order GetByName(string Name);
        IQueryable<Order> GetByType(int Type);
        IQueryable<Order> GetPurchasePostsByUserId(Guid id);
        Order CreateOrder(Order order);
        Order UpdateOrder(Order order);
        void Delete(Guid Id);
    }
}
