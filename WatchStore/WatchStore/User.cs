using System.ComponentModel.DataAnnotations;

namespace WatchStore
{
    public class User
    {
        public int UserId { get; set; }

        [MaxLength(40)]
        public string UserName { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

    }
}
