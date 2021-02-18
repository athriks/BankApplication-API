using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankApplication_API.Models
{
    public class BankModel
    {
        public CustomerModel customerModel { get; set;}
       
        public double AccountBalance { get; set;}
        public int Id { get; set; }
    }
}
