using LoyalityProgram.Models;
using Microsoft.AspNetCore.Mvc;

namespace LoyalityProgram.Controllers
{
    [Route("/users")]
    public class UsersController : ControllerBase
    {
        [HttpPost("")]
        public ActionResult<LoyaltyProgramUser> CreateUser(
        [FromBody] LoyaltyProgramUser user)
        {
            if (user == null)
                return BadRequest();


            var newUser = RegisterUser(user);
            return Created(new Uri($"/users/{newUser.Id}", UriKind.Relative), newUser);
        }
        private LoyaltyProgramUser RegisterUser(LoyaltyProgramUser user)
        {
            return user;
        }
    }
}
