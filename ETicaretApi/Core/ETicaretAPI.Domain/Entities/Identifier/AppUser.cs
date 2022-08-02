using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Domain.Entities.Identifier
{
    public class AppUser : IdentityUser<String>
    {
        public string? FullName { get; set; }
    }
}
