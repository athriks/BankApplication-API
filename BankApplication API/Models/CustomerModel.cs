using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankApplication_API.Models
{
    public class CustomerModel
    {
        public int Id
        {
            get; set;
        }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
