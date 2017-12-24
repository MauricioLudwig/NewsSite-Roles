using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace NewsSite.Models
{
    public class ListClaimsVM
    {
        public string Email { get; set; }
        public List<Claim> Claims { get; set; }
    }
}
