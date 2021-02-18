using BankApplication_API.Controllers;
using BankApplication_API.DataBase;
using BankApplication_API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Net;
using Xunit;

namespace Bank_API_test_Cases
{
    public class CustomerTest
    {
        public  static IConfiguration _configuration;
        private readonly static CustomerContext context;
        //private readonly CustomerController customerController;
        
        CustomerController customerController = new CustomerController(context, _configuration);

        [Fact]
        public  void Customer_Ok()
        {
            var customerNew = new CustomerModel {Name = "ATHRI", Email = "athriksas@gmail.com", Password ="123"};

           var data =  customerController.AddCustomer(customerNew);

            //Assert  
            Assert.IsType<OkObjectResult>(data);

        }
    }
}
