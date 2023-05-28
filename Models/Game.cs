using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace ContentTracker.Models
{
    public class Game
    {
        [Key]
        [JsonIgnore]
        public int Id { get; set; }
        public string Name { get; set; }
        public string background_image { get; set; }
        public ICollection<UserGame> UserGames { get; set; }
    }

}
