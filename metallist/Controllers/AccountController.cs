using metallist.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace metallist.Controllers
{
    public class AccountController : Controller
    {
        ApplicationDbContext db;
        public AccountController(ApplicationDbContext context)
        {
            db = context;
        }

        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(string Login, string Password)
        {
            var admin = db.Admins.SingleOrDefault(a => a.Login == Login && a.Password == Password);

            if (admin != null)
            {
                // Создаем Claims
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, admin.Login),
                    new Claim(ClaimTypes.Role, "Admin")
                };

                // Создаем ClaimsIdentity
                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                // Создаем объект ClaimsPrincipal
                var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

                // Аутентификация с использованием куки
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal);

                return RedirectToAction("Index", "AdminPanel");
            }

            ModelState.AddModelError("", "Invalid login or password");
            return View();
        }
    }
}
