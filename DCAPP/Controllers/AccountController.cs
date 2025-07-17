using Microsoft.AspNetCore.Mvc;
using DCAPP.Models;
using DCAPP.Services.Contracts;
using DCAPPLIB.Entities.Dtos.User;
using DCAPPLIB.Entities.Dtos;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.IdentityModel.Tokens.Jwt;

namespace DCAPP.Controllers;

public class AccountController(IAuthService authService) : Controller
{
    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Register()
    {
        if (User.Identity is not null && User.Identity.IsAuthenticated)
            return RedirectToAction("Index", "Home");
        else
            return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Register([FromForm] RegisterViewModel model)
    {
        if (ModelState.IsValid)
        {
            var userRegister = new UserDtoForRegistration()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                UserName = model.Username,
                Password = model.Password,
                Email = model.Email,
                Roles = ["User"]
            };

            var response = await authService.TryRegisterAsync(userRegister);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Login", "Account");
            }
            else
                ModelState.AddModelError("", "Kayıt işlemi başarısız oldu!");
        }
        return View(model);
    }

    public IActionResult Login()
    {
        if (User.Identity is not null && User.Identity.IsAuthenticated)
            return RedirectToAction("Index", "Home");
        else
            return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login([FromForm] LoginViewModel model)
    {
        if (ModelState.IsValid)
        {
            var userLogin = new UserDtoForAuth()
            {
                UserName = model.Username,
                Password = model.Password,
            };

            var response = await authService.TryLoginAsync(userLogin);
            if (response.IsSuccessStatusCode)
            {
                var tokenDto = await response.Content.ReadFromJsonAsync<TokenDto>();

                var claims = new List<Claim>
                {
                    new(ClaimTypes.Name, model.Username),
                    new("accessToken", tokenDto?.AccessToken!),
                };

                var handler = new JwtSecurityTokenHandler();
                var jwtToken = handler.ReadJwtToken(tokenDto!.AccessToken);

                jwtToken.Claims.ToList().ForEach(x =>
                {
                    if (x.Type == ClaimTypes.Role)
                        claims.Add(x);
                });

                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                return RedirectToAction("Index", "Account");
            }
            else
                ModelState.AddModelError("", "Giriş işlemi başarısız!");
        }
        return View(model);
    }

    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction("Index", "Home");
    }

    public IActionResult AccessDenied() => View();
}