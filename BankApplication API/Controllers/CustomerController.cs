using BankApplication_API.DataBase;
using BankApplication_API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BankApplication_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        public IConfiguration _configuration;
        private readonly CustomerContext _context;
        public CustomerController(CustomerContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        [HttpPost]
        public async Task<IActionResult> AddCustomer(CustomerModel model)
        {
            try
            {
                var newCustomer = new Customer()
                {
                    Name = model.Name,
                    Email = model.Email,
                    Password = model.Password
                };
                await _context.Customer.AddAsync(newCustomer);
                await _context.SaveChangesAsync();

                if (newCustomer == null)
                {
                }
                return Ok(newCustomer);
            }
            catch (Exception e)
            {
                return Ok("cc");
            }
           

        }
     

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Post(CustomerModel model)
        {

            if (model != null && model.Email != null && model.Password != null)
            {
                var user = await GetUser(model.Email, model.Password);

                if (user != null)
                {
                    //create claims details based on the user information
                    var claims = new[] {
                    new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                    new Claim("Id", user.Id.ToString()),
                    new Claim("Name", user.Name),
                    new Claim("Email", user.Email)
                   };

                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));

                    var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                    var token = new JwtSecurityToken(_configuration["Jwt:Issuer"], _configuration["Jwt:Audience"], claims, expires: DateTime.UtcNow.AddDays(1), signingCredentials: signIn);

                    return Ok(new JwtSecurityTokenHandler().WriteToken(token));
                }
                else
                {
                    return BadRequest("Invalid credentials");
                }
            }
            else
            {
                return BadRequest();
            }
        }
        private async Task<Customer> GetUser(string email, string password)
        {
            return await _context.Customer.FirstOrDefaultAsync(u => u.Email == email && u.Password == password);

        }

    }

}
