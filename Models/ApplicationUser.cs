
using Microsoft.AspNetCore.Identity;

namespace auth.Models
{
    public class ApplicationUser:IdentityUser
    {

        public DateTime DateOfBirth { get; set; } 
    }
}
