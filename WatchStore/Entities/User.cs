﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Entities;

public partial class User
{
    [Key]
    [Column("User_id")]
    public int UserId { get; set; }

    [Required]
    [Column("User_name")]
    [StringLength(50)]
    public string UserName { get; set; }

    [Required]
    [StringLength(50)]
    public string Password { get; set; }

    [Column("First_name")]
    [StringLength(50)]
    public string FirstName { get; set; }

    [Column("Last_name")]
    [StringLength(50)]
    public string LastName { get; set; }

    [InverseProperty("User")]
    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}