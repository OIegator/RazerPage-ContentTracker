using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ContentTracker.Models
{
    public class UserGame
    {
        public string? UserId { get; set; }
        public User? User { get; set; }
        public int GameId { get; set; }
        public Game? Game { get; set; }
        public string? Month { get; set; } // Добавленное поле "Month"
    }
}

