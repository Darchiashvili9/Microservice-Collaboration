using LoyalityProgram.Models;
using Microsoft.AspNetCore.Mvc;

namespace LoyalityProgram.Controllers
{
    [Route("/users")]
    public class UsersController : ControllerBase
    {
        private static readonly IDictionary<int, LoyaltyProgramUser> RegisteredUsers = new Dictionary<int, LoyaltyProgramUser>();

        [HttpGet("{userId:int}")]
        public ActionResult<LoyaltyProgramUser> GetUser(int userId) => RegisteredUsers.ContainsKey(userId) ? (ActionResult<LoyaltyProgramUser>)Ok(RegisteredUsers[userId]) : NotFound();

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

        [HttpPut("{userId:int}")]
        public LoyaltyProgramUser UpdateUser(int userId, [FromBody] LoyaltyProgramUser user) => RegisteredUsers[userId] = user;
    }
}
