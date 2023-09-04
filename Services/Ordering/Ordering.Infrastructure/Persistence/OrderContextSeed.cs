using Microsoft.Extensions.Logging;
using Ordering.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Infrastructure.Persistence
{
    public class OrderContextSeed
    {
        public static async Task SeedAsync(OrderContext orderContext, ILogger<OrderContextSeed> logger)
        {
            if(orderContext.Orders.Any())
            {
                orderContext.Orders.AddRange(GetPreconfiguredOrders());
                await orderContext.SaveChangesAsync();
            }


        }

        private static IEnumerable<Order> GetPreconfiguredOrders()
        {
            return new List<Order>
            {
                new Order() {UserName = "swn", FirstName = "Mehmet", LastName = "Ozkaya", EmailAddress = "ezozkme@gmail.com", AddressLine = "Bahcelievler", Country = "Turkey", TotalPrice = 350 , State ="West Midland",ZipCode = "B151UQ",
                CardName= "Debit", CardNumber = "1111222233334444" , Expiration ="01/26", CVV = "321" ,PaymentMethod= 1, CreatedBy = "Abeed" , CreatedDate = DateTime.Now ,LastModifiedBy= "Abeed", LastModifiedDate = DateTime.Now }
            };
        }
    }
}
