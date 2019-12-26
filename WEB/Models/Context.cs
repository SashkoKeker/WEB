using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WEB.Models;

namespace WEB.Models
{
    public class ApplicationContext : IdentityDbContext<User, Role, Guid>
    {
        public DbSet<Order> orders { get; set; }
        public DbSet<OrderType> orderTypes { get; set; }
        public DbSet<Chat> chats { get; set; }
        public DbSet<Contact> contacts { get; set; }
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
            Database.EnsureDeleted();
            Database.EnsureCreated();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            

            modelBuilder.Entity<User>()
                .Property(p => p.UserDateRegistration)
                .IsRequired()
                .HasComputedColumnSql("GETDATE()");


            //////////////////////////////////////////////////////////

            ///////////////////////////////////////////////////

            modelBuilder.Entity<Order>()
                .HasKey(u => u.OrderId);

            modelBuilder.Entity<Order>()
                .Property(p => p.OrderId)
                .IsRequired()
                .ValueGeneratedOnAdd();
            /////////////////////////////////////////////
            modelBuilder.Entity<OrderType>()
                .HasKey(u => u.OrderTypeId);

            modelBuilder.Entity<Order>()
                .HasOne(p => p.orderTypes)
                .WithMany(t => t.Orders)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Order>()
                .HasOne(p => p.user)
                .WithMany(t => t.orders)
                .OnDelete(DeleteBehavior.Cascade);



            /////////////////////////////////////
            modelBuilder.Entity<Contact>()
                .HasKey(u => u.ContactId);


            ////////////////
            modelBuilder.Entity<Chat>()
                .HasKey(u => u.ChatId);

            modelBuilder.Entity<Chat>()
                .Property(p => p.ChatId)
                .ValueGeneratedOnAdd();



            modelBuilder.Entity<Chat>()
                .HasOne(p => p.contacs)
                .WithMany(t => t.chats)
                .HasForeignKey(p => p.ChatId);
            ////////////////////////////////////////////

            modelBuilder.Entity<Contact>()
                .HasOne(p => p.userss)
                .WithMany(t => t.contacts)
                .HasForeignKey(p => p.UserId);


            modelBuilder.Entity<Contact>()
                .HasOne(q => q.useres)
                .WithMany(t => t.contactes)
                .HasForeignKey(q => q.ContactUserId);
            ///////////////////////////////////////////////////////////


            modelBuilder.Entity<OrderType>().HasData(
            new OrderType[]
            {
                new OrderType { OrderTypeId=1,  OrderTypes ="Shukaut"},
                new OrderType { OrderTypeId=2, OrderTypes ="Nadayut"},
            });

            base.OnModelCreating(modelBuilder);

        }
    }
}
