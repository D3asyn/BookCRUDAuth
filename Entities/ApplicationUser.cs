using Microsoft.AspNetCore.Identity;
using System.Text.Json.Serialization;

namespace BookCRUDAuth.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string? Firstname { get; set; }
        public string? Lastname { get; set; }
    }
}
