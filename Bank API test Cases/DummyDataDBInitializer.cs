using BankApplication_API.DataBase;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bank_API_test_Cases
{
 
        public class DummyDataDBInitializer
        {
            public DummyDataDBInitializer()
            {
            }

            public void Seed(CustomerContext context)
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

              

                context.Customer.AddRange(
                    new Customer() { Name = "ATHRI", Email = "athriksas@gmail.com", Password = "123"  },
                    new Customer() { Name = "Abhi", Email = "Abhi123@gmail.com", Password = "123"}
                );
                context.SaveChanges();
            }
        }
    
}
