using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WEB.Models;
using WEB.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace WEB.Services
{
    public class OrderService : IOrderService
    {
        private readonly ApplicationContext _orderecontext;

        public OrderService(ApplicationContext featurecontext)
        {
            _orderecontext = featurecontext;
        }

        public IQueryable<Order> GetAll()
        {
            return _orderecontext.orders;
        }

        public Order GetById(Guid id)
        {
            return GetAll().Single(x => x.OrderId == id);
        }
        public Order GetByName(string Name)
        {
            var Order = _orderecontext.orders.FirstOrDefault(x => x.OrderName == Name);
            return Order;
        }
        public IQueryable<Order> GetPurchasePostsByUserId(Guid id)
        {
            return _orderecontext.orders.Where(x => x.OrderUserId == id);
        }
        public IQueryable<Order> GetByType(int Type)
        {
            return _orderecontext.orders.Where(x=>x.OrderTypeId == Type);
        }
        public Order CreateOrder(Order order)
        {
            _orderecontext.orders.Add(order);
            _orderecontext.SaveChanges();

            return order;
        }

        public Order UpdateOrder(Order order)
        {
            _orderecontext.ChangeTracker.Entries().ElementAt(0).State = Microsoft.EntityFrameworkCore.EntityState.Detached;
            _orderecontext.SaveChanges();
            _orderecontext.Attach(order);
            _orderecontext.Entry(order).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _orderecontext.SaveChanges();

            return order;
        }

        public void Delete(Guid Id)
        {
            _orderecontext.Remove(GetById(Id));
            _orderecontext.SaveChanges();
        }
    }
}
