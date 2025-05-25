﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs
{
    public record RegisterUserDto(
        string UserName,
        string Password,
        string FirstName,
        string LastName
        );
}
