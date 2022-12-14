using Application.Services.Common.UsersFacade;
using Domain.Entities.Users_n_Roles;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Endpoint.Site.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class RoleController : Controller
    {
        private readonly RoleManager<Role> _roleManager;
        private readonly IUserFacade _userFacade;
        public RoleController(RoleManager<Role> roleManager, IUserFacade userFacade)
        {
            _roleManager = roleManager;
            _userFacade = userFacade;
        }

        public IActionResult Index()
        {
            var roles = _roleManager.Roles.ToList().Select(p => new RoleDto
            {
                Id = p.Id,
                Name = p.Name
            }).ToList();
            return View(roles);
        }

        [HttpPost]
        public IActionResult Create(RoleDto roleDto)
        {
          var result =  _userFacade.AddRole.Execute(roleDto);
                return Json(result);
        }
    }
}
