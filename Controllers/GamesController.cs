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
            // �������� ������������ � �������� ������ ������������ �� ���� ������
            var user = _dbContext.Users.FirstOrDefault(u => u.UserName == username);

            if (user != null)
            {
                // ����� ����� ��������� �������������� ������, ��������� � ������������ �������� ��� ��� ����������� ������������
                return View();
            }

            // ���� ������������ �� ����������, ������� NotFound
            return NotFound();
        }
    }
}
