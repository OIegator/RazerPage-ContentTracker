using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace ContentTracker.Models
{
    public class User : IdentityUser
    {
        public ICollection<UserGame>? UserGames { get; set; }
    }
}
