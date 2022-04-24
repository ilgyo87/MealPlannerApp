using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Capstone.DAO;
using Capstone.Models;

namespace Capstone.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class AccountController : ControllerBase
    {
        private readonly IAccountDao accountDao;

        public AccountController(IAccountDao _accountDao)
        {
            accountDao = _accountDao;
        }

        [HttpGet()]
        public ActionResult<Accounts> GetAccount()
        {
            string username = User.Identity.Name;
            Accounts account = accountDao.GetAccount(username);
            if (account != null)
            {
                return Ok(account);
            }
            return NotFound();
        }

        //[HttpGet("otheraccounts")]
        //public ActionResult<Accounts> List()
        //{
        //    string username = User.Identity.Name;
        //    IList<Accounts> accounts = accountDao.GetAllAccountsListButMe(username);
        //    if (accounts != null)
        //    {
        //        return Ok(accounts);
        //    }
        //    else
        //    {
        //        return NoContent();
        //    }
        //}

        [HttpPost("post")]
        public ActionResult<Accounts> PostAccount(Accounts account)
        {
            Accounts postingAccount = accountDao.PostAccount(account);
            if (postingAccount != null)
            {
                return Created($"/accounts/{postingAccount.Username}", postingAccount);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPut("update")]
        public ActionResult<Accounts> UpdateAccount(Accounts account)
        {
            if (account != null)
            {
                int currentUserId = Int32.Parse(User.FindFirst("sub")?.Value);

                if (currentUserId == account.UserId)
                {
                    Accounts updatedAccount = accountDao.UpdateAccount(account);
                    return Created($"/accounts/{updatedAccount.Username}", updatedAccount);
                }
                else
                {
                    return Unauthorized();
                }              
            }
            else
            {
                return BadRequest();
            }
        }

    }
}
