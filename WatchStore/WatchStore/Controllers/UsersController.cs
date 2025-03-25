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
        // GET: api/<UsersController>
        [HttpGet]
        public IEnumerable<User> Get()
        {

            return null;//users;
        }

        // GET api/<UsersController>/5
        [HttpGet("{id}")]
        public ActionResult<User>  Get(int id)
        {
            using (StreamReader reader = System.IO.File.OpenText("users.txt"))
            {
                string? currentUserInFile;
                while ((currentUserInFile = reader.ReadLine()) != null)
                {
                    User user = JsonSerializer.Deserialize<User>(currentUserInFile);
                    if (user?.UserId == id)
                        return Ok(user);
                }
            }
            return NoContent();
        }

        // POST api/<UsersController>

        [HttpPost("register")]
        public ActionResult<User> Register([FromBody] User user)
        {
            if (user is null)
                return StatusCode(400, "user is required");
            if (string.IsNullOrEmpty(user.Password) || string.IsNullOrEmpty(user.UserName))
                return StatusCode(400, "Password and UserName are required");          
            try
            {
                int numberOfUsers = System.IO.File.Exists("users.txt") ? System.IO.File.ReadLines("users.txt").Count() : 0;
                user.UserId = numberOfUsers + 1;
                if (System.IO.File.Exists("users.txt"))
                {
                    var existingUsers = System.IO.File.ReadLines("users.txt").Select(line => JsonSerializer.Deserialize<User>(line)).ToList();
                    if (existingUsers.Any(u => u.UserName == user.UserName))
                        return StatusCode(400, "Username is already taken");
                }  
                string userJson = JsonSerializer.Serialize(user);
                System.IO.File.AppendAllText("users.txt", userJson + Environment.NewLine);
                return CreatedAtAction(nameof(Get), new { id = user.UserId }, user);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error writing user to file: " + ex.Message);
            }
        }

        [HttpPost("login")]
        public ActionResult<User> login([FromBody] LoginUser loginRequest)
        {
            if (string.IsNullOrEmpty(loginRequest?.Password) || string.IsNullOrEmpty(loginRequest?.UserName))
                return BadRequest(new { error = "Username and password are required." });
            try
            {
                if (!System.IO.File.Exists("users.txt"))
                {
                    return NotFound("No users found.");
                }

                using (StreamReader reader = System.IO.File.OpenText("users.txt"))
                {
                    string currentUserInFile;
                    while ((currentUserInFile = reader.ReadLine()) != null)
                    {
                        User user = JsonSerializer.Deserialize<User>(currentUserInFile);
                        if (user?.UserName == loginRequest.UserName && user.Password == loginRequest.Password)
                        {
                            return Ok(user);
                        }
                    }
                }
                return Unauthorized("Invalid username or password.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error reading users from file: " + ex.Message);
            }
        }
        
        // PUT api/<UsersController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] User userToUpdate)
        {
            string textToReplace = string.Empty;
            using (StreamReader reader = System.IO.File.OpenText("users.txt"))
            {
                string currentUserInFile;
                while ((currentUserInFile = reader.ReadLine()) != null)
                {
                    User user = JsonSerializer.Deserialize<User>(currentUserInFile);
                    if (user?.UserId == id)
                        textToReplace = currentUserInFile;
                }
            }

            if (textToReplace != string.Empty)
            {
                string text = System.IO.File.ReadAllText("users.txt");
                text = text.Replace(textToReplace, JsonSerializer.Serialize(userToUpdate));
                System.IO.File.WriteAllText("users.txt", text);
            }
        }

        // DELETE api/<UsersController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
