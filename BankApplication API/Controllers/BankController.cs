using BankApplication_API.DataBase;
using BankApplication_API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;

namespace BankApplication_API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class BankController : ControllerBase
    {
        public IConfiguration _configuration;
        private readonly CustomerContext _context;

        public BankController(CustomerContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        [HttpPost]
        [Route("Deposit")]

        public async Task<IActionResult> Deposit(Bank model)
        {
          
            var handler = new JwtSecurityTokenHandler();
            string authHeader = Request.Headers["Authorization"];
            authHeader = authHeader.Replace("Bearer ", "");
            var jsonToken = handler.ReadToken(authHeader);
            var tokenS = handler.ReadToken(authHeader) as JwtSecurityToken;
            var Name = tokenS.Claims.First(claim => claim.Type == "Name").Value;
            var Id = tokenS.Claims.First(claim => claim.Type == "Id").Value;
            var Email = tokenS.Claims.First(claim => claim.Type == "Email").Value;
            var user = await GetUser(Email, Name);
            if (user != null)
            {
               
                
                    var BankData = await GetUpdateValues(Email);

                    if (BankData != null)
                    {
                        model.CustomerId = Email;

                        var newBalance = BankData.AccountBalance;
                        BankData.AccountBalance = newBalance + model.AccountBalance;
                        await _context.SaveChangesAsync();
                        return Ok("Deposit Successful");
                    }
                    else
                    {
                    model.CustomerId = Email;
                    model.AccountBalance = model.AccountBalance;
                    await _context.Bank.AddAsync(model);
                    await _context.SaveChangesAsync();
                    return Ok("Deposit Successful");
                    }
                
            }
            else
            {
                return NotFound("Token Not ValidUser Not Exsit");
            }


        }

        [HttpPost]
        [Route("Withdraw")]

        public async Task<IActionResult> Withdrwal(Bank model)
        {
            var handler = new JwtSecurityTokenHandler();
            string authHeader = Request.Headers["Authorization"];
            authHeader = authHeader.Replace("Bearer ", "");
            var jsonToken = handler.ReadToken(authHeader);
            var tokenS = handler.ReadToken(authHeader) as JwtSecurityToken;
            var Name = tokenS.Claims.First(claim => claim.Type == "Name").Value;
            var Id = tokenS.Claims.First(claim => claim.Type == "Id").Value;
            var Email = tokenS.Claims.First(claim => claim.Type == "Email").Value;
            var user = await GetUser(Email, Name);
            if (user != null)
            {
                var BankData = await GetUpdateValues(Email);

                if (BankData != null)
                {
                    if (model.AccountBalance > BankData.AccountBalance)
                    {
                        return NotFound("You Don't have Balance");
                    }
                    var newBalance = BankData.AccountBalance - model.AccountBalance;
                    BankData.AccountBalance = newBalance;
                    await _context.SaveChangesAsync();
                    return Ok("withdraw Successful");
                }
                else
                {
                    return NotFound("Withdrwal failed");
                }

            }
            else
            {
                return NotFound("Token Invalid");
            }
        }
        public async Task<Customer> GetUser(string email, string name)
        {
            return await _context.Customer.FirstOrDefaultAsync(u => u.Email == email && u.Name == name);

        }
        public async Task<Bank> GetUpdateValues(string email)
        {
            return await _context.Bank.FirstOrDefaultAsync(x => x.CustomerId == email);
        }

    }
}
