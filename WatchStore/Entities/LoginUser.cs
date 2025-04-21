using System.ComponentModel.DataAnnotations;

namespace Entities
{
    public class LoginUser
    {
        [MaxLength(40)]
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
