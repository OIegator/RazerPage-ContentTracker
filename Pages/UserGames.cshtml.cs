using ContentTracker.Data;
using ContentTracker.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContentTracker.Pages
{
    public class UserGamesModel : PageModel
    {
        private readonly AppDbContext _dbContext;

        [BindProperty(SupportsGet = true)]
        public string Username { get; set; }

        public List<Game> Games { get; set; }

        public UserGamesModel(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _dbContext.Users
                .Include(u => u.UserGames)
                .ThenInclude(ug => ug.Game)
                .FirstOrDefaultAsync(u => u.UserName == Username);

            if (user == null)
            {
                return NotFound();
            }

            Games = user.UserGames?.Select(ug => ug.Game).ToList() ?? new List<Game>();

            return Page();
        }
    }
}
