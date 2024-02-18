using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
using System.Net;

namespace KazahStore.Models
{
    public class AppUser : IdentityUser
    {
        public string? ProfileImageUrl { get; set; }
        public ICollection<Store>? Items { get; set; }
        public ICollection<Trade>? Trades { get; set; }
    }
}
