using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunTalk.Infrastructure.Models
{
    public class AppUser:IdentityUser
    {
        public bool IsPrivate { get; set; } = false;
    }
}
