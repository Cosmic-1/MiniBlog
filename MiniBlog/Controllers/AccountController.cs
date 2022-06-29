using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace MiniBlog.Controllers
{
    public class AccountController : Controller
    {
        private readonly ILogger<AccountController> logger;
        private readonly ManagerData manager;
        private readonly VerificationUser verification;

        public AccountController(ILogger<AccountController> logger, ManagerData manager, VerificationUser verification)
        {
            this.logger = logger;
            this.manager = manager;
            this.verification = verification;
        }

        [HttpGet("/author/{id:int}", Name = "AuthorRoute"), Authorize]
        public async Task<IActionResult> AuthorAsync([FromRoute] int id)
        {
            var author = await manager.Users.GetUserByIdAsync(id);

            if (author is null) return LocalRedirect("/");

            return View(author);
        }

        [HttpGet("/login", Name = "LoginRoute"), AllowAnonymous]
        public IActionResult Login()
        {
            if (User.Identity?.IsAuthenticated ?? false) return RedirectToRoute("AdminRoute");

            return View();
        }

        [HttpPost("/login"), AllowAnonymous]
        public async Task<IActionResult> Login([FromForm] LoginModel? login)
        {
            if (login is null) return View();

            if (User.Identity?.IsAuthenticated ?? false) return RedirectToRoute("AdminRoute");


            if (ModelState.IsValid && await verification.IsValidUser(login))
            {

                var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
                identity.AddClaim(new Claim(ClaimTypes.Name, login.Nickname));

                var principle = new ClaimsPrincipal(identity);
                var properties = new AuthenticationProperties
                {
                    IsPersistent = login.Remember
                };

                await HttpContext.SignInAsync(principle, properties);
                await HttpContext.AuthenticateAsync();

                return RedirectToRoute("AdminRoute");
            }

            return View();

        }

        [HttpGet("/logout"), AllowAnonymous]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();

            return LocalRedirect("/");
        }
    }
}
