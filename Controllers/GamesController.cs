using ContentTracker.Data;
using Microsoft.AspNetCore.Mvc;

namespace ContentTracker.Controllers
{
    public class GamesController : Controller
    {
        private readonly AppDbContext _dbContext;

        public GamesController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Index(string username)
        {
            // Получите пользователя с заданным именем пользователя из базы данных
            var user = _dbContext.Users.FirstOrDefault(u => u.UserName == username);

            if (user != null)
            {
                // Здесь можно выполнить дополнительную логику, связанную с отображением страницы игр для конкретного пользователя
                return View();
            }

            // Если пользователя не существует, вернуть NotFound
            return NotFound();
        }
    }
}
