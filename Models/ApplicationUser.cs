using Microsoft.AspNetCore.Identity;

namespace urun_katalog.Models
{
    public class ApplicationUser:IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        
    }
}