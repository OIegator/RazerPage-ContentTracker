using ContentTracker.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ContentTracker.Pages.Account
{
    public class RegisterModel : PageModel
    {
        private readonly UserManager<User> _userManager;

        public RegisterModel(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        [BindProperty]
        public RegisterInput Input { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var userExists = await _userManager.FindByNameAsync(Input.Username);
                if (userExists != null)
                {
                    ModelState.AddModelError(string.Empty, "Username is already taken.");
                    return Page();
                }

                var user = new User { UserName = Input.Username, Email = Input.Email };
                var result = await _userManager.CreateAsync(user, Input.Password);

                if (result.Succeeded)
                {
                    // Пользователь успешно зарегистрирован
                    return RedirectToPage("/Games");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }

            // Если произошла ошибка, отобразить форму регистрации с ошибками
            return Page();
        }


    }

}
