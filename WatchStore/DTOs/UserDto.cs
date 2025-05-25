using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs
{
    public record UserDto(
        int UserId,
        string UserName,
        string FirstName,
        string LastName
    );

}
