using ContentTracker.Data;
using ContentTracker.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace ContentTracker.Pages
{
    [Authorize]
    public class GamesModel : PageModel
    {
        private readonly AppDbContext _dbContext;
        private readonly HttpClient _httpClient;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        public GamesModel(HttpClient httpClient, AppDbContext dbContext, UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _httpClient = httpClient;
            _dbContext = dbContext;
            _signInManager = signInManager;
        }

        [BindProperty]
        public string? GameName { get; set; }

        [BindProperty]
        public string? SelectedMonth { get; set; }

        public Game? Game { get; set; }
        public List<Game>? Games { get; set; }

        public List<User>? Users { get; set; }
        public async Task<IActionResult> OnGetAsync()
        {
            string userId = _userManager.GetUserId(User)!;

            Users = await _dbContext.Users
                .Include(u => u.UserGames!)
                .ThenInclude(ug => ug.Game)
                .Where(u => u.Id == userId)
                .ToListAsync();

            Games = Users.SelectMany(u => u.UserGames!)
                .Select(ug => ug.Game!)
                .ToList();

            return Page();
        }


        public async Task<IActionResult> OnPostLogoutAsync()
        {
            await _signInManager.SignOutAsync();
            return RedirectToPage("/Index");
        }


        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            string apiKey = "c593f58019084152a8f8bd09c3c88048";
            string apiUrl = $"https://api.rawg.io/api/games/{GameName}?key={apiKey}";

            HttpResponseMessage response = await _httpClient.GetAsync(apiUrl);
            if (response.IsSuccessStatusCode)
            {
                string jsonResponse = await response.Content.ReadAsStringAsync();
                Game = JsonConvert.DeserializeObject<Game>(jsonResponse);
                string userId = _userManager.GetUserId(User)!;

                User? user = await _dbContext.Users
                    .Include(u => u.UserGames)
                    .FirstOrDefaultAsync(u => u.Id == userId);

                if (user != null)
                {
                    // Проверяем, существует ли игра в базе данных
                    Game? existingGame = await _dbContext.Games
                        .FirstOrDefaultAsync(g => g.Name == Game!.Name);

                    if (existingGame == null)
                    {
                        // Игра не существует, добавляем ее в базу данных
                        _dbContext.Games.Add(Game!);
                        await _dbContext.SaveChangesAsync();
                    }
                    else
                    {
                        // Игра уже существует, используем ее вместо новой
                        Game = existingGame;
                    }

                    UserGame userGame = new UserGame
                    {
                        User = user,
                        Game = Game
                    };

                    // Добавляем связь пользователя и игры в базу данных
                    _dbContext.UserGames.Add(userGame);
                    await _dbContext.SaveChangesAsync();
                }
            }
            else
            {
                Game = new Game();
            }

            return RedirectToPage("/Games");
        }

    }
}
