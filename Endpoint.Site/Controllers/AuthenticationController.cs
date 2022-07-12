using Application.Interface.Context;
using Application.Services.Common.UsersFacade;
using Common;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Endpoint.Site.Controllers
{
    public class AuthenticationController : Controller
    {
        private readonly IUserFacade _facade;
        private readonly IValidator<UserDto> _userValidator;
        private readonly IValidator<LoginDto> _loginValidator;
        public AuthenticationController(IUserFacade facade, IValidator<UserDto> userValidator, IValidator<LoginDto> loginValidator)
        {
            _loginValidator = loginValidator;
            _userValidator = userValidator;
            _facade = facade;
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(UserDto dto)
        {
            ValidationResult result = _userValidator.Validate(dto);
            if (!result.IsValid)
            {
                return View();
            }

            var user = _facade.AddUser.Execute(new UserDto
            {
                Email = dto.Email,
                FullName = dto.FullName,
                Password = dto.Password,
                PhoneNumber = dto.PhoneNumber,
                RePassword = dto.RePassword,
                Roles = new List<RoleDto>()
                {
                    new RoleDto
                    {
                        Id  = UserDefaultRolesId.Customer
                    }
                }
            });

            if (user.IsSuccess == true)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier,user.Data.UserId.ToString()),
                    new Claim(ClaimTypes.Email,dto.Email),
                    new Claim(ClaimTypes.Name,dto.FullName),
                    new Claim(ClaimTypes.Role,UserDefaultRoles.Customer)
                };
                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);
                var properties = new AuthenticationProperties()
                {
                    IsPersistent = true,
                    ExpiresUtc = DateTime.Now.AddDays(20),
                };
                HttpContext.SignInAsync(principal, properties);
            }
            return View();
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginDto login)
        {
            ValidationResult result = _loginValidator.Validate(login);
            if (!result.IsValid)
            {
                ViewBag.Validation = result.Errors[0].ErrorMessage;
                return View(login);
            }

            var user = _facade.UserLogin.Execute(login);
            if (user.IsSuccess == true)
            {
                var claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.NameIdentifier,user.Data.UserId.ToString()),
                    new Claim(ClaimTypes.Name,user.Data.Name),

                    new Claim(ClaimTypes.Email,user.Data.Email)
                };
                foreach (var item in user.Data.Role)
                {
                    claims.Add(new Claim(ClaimTypes.Role, item));
                }

                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);
                var properties = new AuthenticationProperties()
                {
                    IsPersistent = true,
                    ExpiresUtc = DateTime.Now.AddDays(20)
                };
                HttpContext.SignInAsync(principal, properties);
            }

            return RedirectToAction("Index", "Home");
        }

        public new IActionResult SignOut()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }
    }
}
