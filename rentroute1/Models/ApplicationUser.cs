﻿using Microsoft.AspNetCore.Identity;

namespace rentroute1.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; }
        public string Role { get; set; }
    }
}