using API.Database;
using API.Models;
using API.Models.Interfaces;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    /// <summary>
    /// User accounts: list, look up and create.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("OpenPolicy")]
    public class accountController : ControllerBase
    {
        // GET: api/account
        [HttpGet]
        public List<Account> Get()
        {
            IGetAllAccounts reader = new ReadAccountData();
            return reader.GetAllAccounts();
        }

        // GET: api/account/5
        [HttpGet("{id}", Name = "GetAccount")]
        public Account Get(int id)
        {
            IGetAccount reader = new ReadAccountData();
            return reader.GetAccount(id);
        }

        // POST: api/account
        [HttpPost]
        public void Post([FromBody] Account value)
        {
            IPostAccount writer = new PostAccount();
            writer.PostAccount(value);
        }
    }
}
