using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Server.Model;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Http;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        // POST api/Users/SignUp
        [HttpPost("SignUp")]
        public void SignUp([FromForm] string username, [FromForm] string password)
        {
        }

        // POST api/Users/Login
        [HttpPost("Login")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(Connection), StatusCodes.Status200OK)]
        public ActionResult<Connection> Login([FromForm] string username, [FromForm] string password)
        {
            //HashAlgorithm sha = new SHA512CryptoServiceProvider();
            //byte[] result = sha.ComputeHash(Encoding.UTF8.GetBytes(password));
            ChatByKeyContext context = HttpContext.RequestServices.GetService(typeof(ChatByKeyContext)) as ChatByKeyContext;
            User user = context.GetUtenteByUsernameAndPassword(username, password);
            //if (user == null) { return NotFound("Wrong Password or Username"); }
            ////else if (result.Equals(sha.ComputeHash(Encoding.UTF8.GetBytes(user.Password)))) return Ok();
            //else if (password == user.Password) return Ok(new Connection());
            //else return NotFound("Wrong Password or Username");
            return Ok(new Connection());
        }
    }
}
