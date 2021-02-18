using BankApplication_API.DataBase;
using BankApplication_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankApplication_API
{
   public interface CustomerInterface
    {
        public Task<Customer> AddCustomer(CustomerModel model);
       
    }
}
