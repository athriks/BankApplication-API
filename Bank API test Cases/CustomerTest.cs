using BankApplication_API;
using BankApplication_API.Controllers;
using BankApplication_API.DataBase;
using BankApplication_API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Net;
using Xunit;

namespace Bank_API_test_Cases
{
    public class CustomerTest
    {
        public static IConfiguration _configuration;
        private readonly CustomerController customerController;
        public static DbContextOptions<CustomerContext> dbContextOptions { get; }
        public static string connectionString = "Server=.;Database=Bank;Integrated Security=True";

        static CustomerTest()
        {
            dbContextOptions = new DbContextOptionsBuilder<CustomerContext>()
                .UseSqlServer(connectionString)
                .Options;
        }
        public CustomerTest()
        {
            var context = new CustomerContext(dbContextOptions);
            DummyDataDBInitializer db = new DummyDataDBInitializer();
            db.Seed(context);

            customerController = new CustomerController(context, _configuration);

        }
        [Fact]
        public async void AddCustomerPostOKResult()
        {
            //Arrange
            var customerNew = new CustomerModel { Name = "ATHRI", Email = "athriksas@gmail.com", Password = "123" };

            var data = await customerController.AddCustomer(customerNew);

            //Assert
            Assert.IsType<OkObjectResult>(data);
        }
        [Fact]
        public async void LoginBadRequestResult()
        {
            //Arrange
            var customerNew = new CustomerModel { Email = "athriksas@gmail.com", Password = "1234" };

            var data = await customerController.Post(customerNew);

            //Assert
            Assert.IsType<BadRequestObjectResult>(data);
        }
        [Fact]

        public async void LoginOKRequestResult()
        {
            //Arrange
            var customerNew = new CustomerModel { Email = "athriksas@gmail.com", Password = "123" };

            var data = await customerController.Post(customerNew);

            //Assert
            Assert.IsType<OkObjectResult>(data);
        }


    }
}
