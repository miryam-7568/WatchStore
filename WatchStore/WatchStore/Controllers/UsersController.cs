using Business;
using Entities;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WatchStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        IUsersServices usersServices;
        public UsersController(IUsersServices usersServices)
        {
            this.usersServices = usersServices;
        }

        // GET: api/<UsersController>
        [HttpGet]
        public IEnumerable<User> Get()
        {
            return null;//users;
        }

        // GET api/<UsersController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>>  Get(int id)
        {
            User user = await usersServices.GetUserById(id);
            if (user != null)
            {
                return Ok(user);
            }
            else
            {
                return NoContent();
            }
        }

        // POST api/<UsersController>

        [HttpPost("register")]
        public async Task<ActionResult<User>> Register([FromBody] User user)
        {
            if (user is null)
                return StatusCode(400, "user is required");

            if (string.IsNullOrEmpty(user.Password) || string.IsNullOrEmpty(user.UserName))
                return StatusCode(400, "Password and UserName are required");
            try
            {
                await usersServices.Register(user);
                return CreatedAtAction(nameof(Get), new { id = user.UserId }, user);
            }
            catch(CustomApiException ex)
            {
                //Console.WriteLine(ex.Message);
                return StatusCode(ex.StatusCode, new { message = ex.Message });
            }

        }

        [HttpPost("login")]
        public async Task<ActionResult<User>> login([FromBody] LoginUser loginUser)
        {
            if (string.IsNullOrEmpty(loginUser?.Password) || string.IsNullOrEmpty(loginUser?.UserName))
                return BadRequest(new { error = "Username and password are required." });
            try
            {
                User user = await usersServices.Login(loginUser);
                return Ok(user);
            }
            catch (CustomApiException ex)
            {
                return StatusCode(ex.StatusCode, new { message = ex.Message });
            }
        }
        
        // PUT api/<UsersController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<User>> Put(int id, [FromBody] User userToUpdate)
        {
            try
            {
                User user = await usersServices.UpdateUser(id, userToUpdate);
                return Ok(user);
            }
            catch (CustomApiException ex)
            {
                return StatusCode(ex.StatusCode, new { message = ex.Message });
            }
        }

        // DELETE api/<UsersController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
