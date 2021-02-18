using BankApplication_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankApplication_API.DataBase
{
    public class Bank
    {
        public int Id { get; set; }
        public string CustomerId { get; set;}
        public double AccountBalance { get; set; }
    }
}
