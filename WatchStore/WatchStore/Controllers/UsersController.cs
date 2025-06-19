using Business;
using DTOs;
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
        IUsersServices _usersServices;
        public UsersController(IUsersServices usersServices)
        {
            _usersServices = usersServices;
        }
        //delete unused code
        
        // GET: api/<UsersController>
        //[HttpGet]
        //public IEnumerable<User> Get()
        //{
        //    return null;//users;
        //}

        // GET api/<UsersController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserDto>>  Get(int id)
        {
            UserDto user = await _usersServices.GetUserById(id);
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
        public async Task<ActionResult<UserDto>> Register([FromBody] RegisterUserDto registerUserDto)
        {
            if (registerUserDto is null)
                return StatusCode(400, "user is required");

            if (string.IsNullOrEmpty(registerUserDto.Password) || string.IsNullOrEmpty(registerUserDto.UserName))
                return StatusCode(400, "Password and UserName are required");
            try
            {
                UserDto userDto = await _usersServices.Register(registerUserDto);
                return CreatedAtAction(nameof(Get), new { id = userDto.UserId }, userDto);
            }
            catch(CustomApiException ex)
            {
                return StatusCode(ex.StatusCode, new { message = ex.Message });
            }

        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login([FromBody] LoginUserDto loginUser)
        {
            if (string.IsNullOrEmpty(loginUser?.Password) || string.IsNullOrEmpty(loginUser?.UserName))
                return BadRequest(new { error = "Username and password are required." });
            try
            {
                UserDto user = await _usersServices.Login(loginUser);
                return Ok(user);
            }
            catch (CustomApiException ex)
            {
                return StatusCode(ex.StatusCode, new { message = ex.Message });
            }
        }
        
        // PUT api/<UsersController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<UserDto>> Put(int id, [FromBody] RegisterUserDto userToUpdate)
        {
            try
            {
                UserDto user = await _usersServices.UpdateUser(id, userToUpdate);
                return Ok(user);
            }
            catch (CustomApiException ex)
            {
                return StatusCode(ex.StatusCode, new { message = ex.Message });
            }
        }

        // DELETE api/<UsersController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
