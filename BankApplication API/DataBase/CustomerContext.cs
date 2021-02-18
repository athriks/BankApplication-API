using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankApplication_API.DataBase
{
    public class CustomerContext : DbContext
    {
        public CustomerContext(DbContextOptions<CustomerContext> options)
            : base(options)
        {

        }
        public DbSet<Customer> Customer { get; set;}
        public object CustomerModel { get; internal set; }
        public DbSet<Bank> Bank { get; set; }
        public object Category { get; set; }
    }
}
