using Application.Services.Common.UsersFacade;
using Application.Services.Queries.GetUsers;
using Application.Validator.Users;
using Domain.Entities.Users_n_Roles;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Endpoint.Site.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UserController : Controller
    {
        private readonly IUserFacade _facade;
        private readonly IValidator<UserDto> _validations;
        public UserController(IUserFacade facade, IValidator<UserDto> validations)
        {
            _facade = facade;
            _validations = validations;
        }

        public IActionResult Index(string searchKey, int page = 1)
        {
            var res = _facade.GetUsersService.Execute(new GetUsersRequestsDto()
            {
                Page = page,
                SearchKey = searchKey
            });
            return View(res);
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Roles = new SelectList(_facade.GetRoles.Execute().Data, "Id", "Name");
            return View();
        }

        [HttpPost]
        public IActionResult Create(UserDto userDto, long RoleId)
        {
            ViewBag.Roles = new SelectList(_facade.GetRoles.Execute().Data, "Id", "Name");

            ValidationResult result = _validations.Validate(userDto);
            ViewBag.Validation = result;
            if (!result.IsValid)
            {
                ViewBag.Roles = new SelectList(_facade.GetRoles.Execute().Data, "Id", "Name");
                return View();
            }

            _facade.AddUser.Execute(new UserDto
            {
                Email = userDto.Email,
                FullName = userDto.FullName,
                Password = userDto.Password,
                PhoneNumber = userDto.PhoneNumber,
                RePassword = userDto.RePassword,
                Roles = new List<RoleDto>
                {
                    new RoleDto
                    {
                        Id = RoleId
                    }
                }
            });

            return View(userDto);
        }

        public IActionResult Delete()
        {
            return RedirectToAction("Index", "Admin");
        }

        [HttpPost]
        public IActionResult Delete(long UserId)
        {
            var result = _facade.RemoveUser.Execute(UserId);
            return Json(result);
        }

        [HttpGet]
        public IActionResult Edit(long UserId)
        {
            var user = _facade.GetUsersService.Execute(new GetUsersRequestsDto
            {
                Page = 1,
                SearchKey = ""
            });
            ViewBag.UserId = UserId;
            return View(user);
        }

        [HttpPost]
        public IActionResult Edit(UserDto user, long UserId)
        {

            var result = _facade.EditUser.Execute(new UserDto
            {
                Email = user.Email,
                FullName = user.FullName,
                PhoneNumber = user.PhoneNumber,
            }, UserId);
            return Json(result);
        }
    }
}
