using Application.Interface.Context;
using Application.Services.Common.UsersFacade;
using Application.Services.MailSender;
using Common;
using Domain.Entities.Users_n_Roles;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Newtonsoft.Json;
using System.Data;
using System.Security.Claims;

namespace Endpoint.Site.Controllers
{
    public class AuthenticationController : Controller
    {
        private readonly IUserFacade _facade;
        private readonly IValidator<UserDto> _userValidator;
        private readonly IValidator<LoginDto> _loginValidator;
        private readonly IMailSenderService _mailSender;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        public AuthenticationController(IUserFacade facade, IValidator<UserDto> userValidator, IValidator<LoginDto> loginValidator, IMailSenderService mailSender, UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _loginValidator = loginValidator;
            _userValidator = userValidator;
            _facade = facade;
            _mailSender = mailSender;
            _userManager = userManager;
            _signInManager = signInManager;
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

            var role = _facade.GetRoles.Execute().Data.Where(p => p.Name == "Customer").FirstOrDefault();
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
                        Name = role.Name,
                        Id = role.Id
                    }
                }
            });

            if (user.IsSuccess == true)
            {
                var claims = new List<Claim>()
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

                //send confirmation email
                var userConfirm = _userManager.FindByIdAsync(user.Data.UserId).Result;
                var token = _userManager.GenerateEmailConfirmationTokenAsync(userConfirm).Result;
                string callbackUrl = Url.Action("ConfirmEmail", "Authentication", new { userId = userConfirm.Id, token = token }, Request.Scheme);
                string body = $"Click the link for email confirmation  <br/> <a href={callbackUrl}> Link </a>";
                var emailResult = _mailSender.Execute(userConfirm.Email, body, "Email Confirmation");
            }
            return View();
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string email, string password, bool isPersistent)
        {
            var user = _userManager.FindByEmailAsync(email).Result;

            _signInManager.SignOutAsync();
            var signinResult = _signInManager.PasswordSignInAsync(user, password, isPersistent, false).Result;
            if (signinResult.RequiresTwoFactor)
            {
                return RedirectToAction("TwoFactorLogin", new { email, isPersistent });
            }
            if (signinResult.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpGet]
        public IActionResult TwoFactorLogin(string email, bool isPersistent)
        {
            var user = _userManager.FindByEmailAsync(email).Result;
            var providers = _userManager.GetValidTwoFactorProvidersAsync(user).Result;

            LoginDto loginDto = new LoginDto();
            if (providers.Contains("Phone"))
            {
                //
            }
            if (providers.Contains("Email"))
            {
                string emailCode = _userManager.GenerateTwoFactorTokenAsync(user, "Email").Result;
                _mailSender.Execute(user.Email, $"Two Factor Code:{emailCode}", "Two Factor Login Code");
                loginDto.Provider = "Email";
                loginDto.IsPersistent = isPersistent;
            }
            return View(loginDto);
        }
        [HttpPost]
        public IActionResult TwoFactorLogin(string code, string provider, bool isPersistent)
        {
            var userResult = _signInManager.GetTwoFactorAuthenticationUserAsync().Result;
            if (userResult == null)
            {
                return BadRequest();
            }

            var loginResult = _signInManager.TwoFactorSignInAsync(provider, code, isPersistent, false).Result;
            if (loginResult.Succeeded)
            {
                return RedirectToAction("index", "home");
            }
            else
            {
                ModelState.AddModelError(" ", "There is a problem in your account. Contact admins");
                return View();
            }
        }

        //[HttpPost]
        //public IActionResult Login(LoginDto login)
        //{
        //    ValidationResult result = _loginValidator.Validate(login);
        //    if (!result.IsValid)
        //    {
        //        ViewBag.Validation = result.Errors[0].ErrorMessage;
        //        return View(login);
        //    }

        //    var user = _facade.UserLogin.Execute(login);
        //    if (user.IsSuccess == true)
        //    {
        //        var claims = new List<Claim>()
        //        {
        //            new Claim(ClaimTypes.NameIdentifier,user.Data.UserId.ToString()),
        //            new Claim(ClaimTypes.Name,user.Data.Name),

        //            new Claim(ClaimTypes.Email,user.Data.Email)
        //        };
        //        foreach (var item in user.Data.Role)
        //        {
        //            claims.Add(new Claim(ClaimTypes.Role, item));
        //        }

        //        var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        //        var principal = new ClaimsPrincipal(identity);
        //        var properties = new AuthenticationProperties()
        //        {
        //            IsPersistent = true,
        //            ExpiresUtc = DateTime.Now.AddDays(20)
        //        };
        //        HttpContext.SignInAsync(principal, properties);
        //    }

        //    return RedirectToAction("Index", "Home");
        //}

        public new IActionResult SignOut()
        {
            _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        //public new IActionResult SignOut()
        //{
        //    HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        //    return RedirectToAction("Index", "Home");
        //}



        [Authorize]
        public IActionResult SendConfirmationEmail()
        {
            var user = _userManager.GetUserAsync(User).Result;
            var tokenResult = _userManager.GenerateEmailConfirmationTokenAsync(user).Result;
            var url = Url.Action("ConfirmEmail", "Authentication", new { userId = user.Id, token = tokenResult }, Request.Scheme);
            _mailSender.Execute(user.Email, $"click for email confirmation <br/> <a  href={url}>Link</a> ", "Email Confirmation");
            return RedirectToAction("Dashboard", "Home");
        }
        [Authorize]
        public IActionResult ConfirmEmail(string userId, string token)
        {
            if (userId == null || token == null)
            {
                return BadRequest();
            }
            _facade.ConfirmEmail.Execute(userId, token);
            return RedirectToAction("Index", "Home");
        }

        [Authorize]
        [HttpGet("[Controller]/TFASC")]
        public IActionResult TwoFactorAuthenticationStatusChange()
        {
            var user = _userManager.FindByNameAsync(User.Identity.Name).Result;
            var result = _userManager.SetTwoFactorEnabledAsync(user, !user.TwoFactorEnabled).Result;
            return RedirectToAction("Dashboard", "Home");
        }

        public IActionResult ExternalLogin(string returnUrl)
        {
            var callbackUrl = Url.Action("Callback", "Authentication", new { returnUrl });
            var properties = _signInManager.ConfigureExternalAuthenticationProperties("Google", callbackUrl);
            return new ChallengeResult("Google", properties);

        }
        public IActionResult Callback(string returnUrl)
        {
            try
            {
                var loginInfo = _signInManager.GetExternalLoginInfoAsync().Result;
                var email = loginInfo.Principal.FindFirst(ClaimTypes.Email).Value;
                var name = loginInfo.Principal.FindFirst(ClaimTypes.Name).Value;

                var signinResult = _signInManager.ExternalLoginSignInAsync("Google", loginInfo.ProviderKey, true, true).Result;
                var user = _userManager.FindByEmailAsync(email).Result;

                if (signinResult.Succeeded)
                {
                    _signInManager.SignInAsync(user, true).Wait();
                    return RedirectToAction("Index", "Home");
                }
                else if (user == null)
                {
                    var role = _facade.GetRoles.Execute().Data.Where(p => p.Name == "Customer").FirstOrDefault();
                    UserDto newUser = new UserDto()
                    {
                        Email = email,
                        FullName = name,
                        EmailConfirmed = true,
                        Roles = new List<RoleDto>()
                        {
                            new RoleDto
                             {
                                 Name = role.Name,
                                 Id = role.Id
                             }
                        },
                    };
                    _facade.AddUser.Execute(newUser);
                    user = _userManager.FindByEmailAsync(newUser.Email).Result;
                    //user = JsonConvert.DeserializeObject<User>(JsonConvert.SerializeObject(newUser));
                }
                var loginResult = _userManager.AddLoginAsync(user, loginInfo).Result;
                _signInManager.SignInAsync(user, true).Wait();

                return RedirectToAction("Index", "Home");
            }
            catch
            {
                ModelState.AddModelError("Signin Failure", "Signin Failed");
                return RedirectToAction("Register", "Authentication");
            }
        }
    }
}
