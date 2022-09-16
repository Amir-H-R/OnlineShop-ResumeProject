using Application.Dto;
using Application.Interface.Context;
using Application.Services.Common.UsersFacade;
using Domain.Entities.Users_n_Roles;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Endpoint.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly IUserFacade _userFacade;
        private readonly IDatabaseContext _context;
        public UsersController(UserManager<User> userManager, RoleManager<Role> roleManager, IUserFacade userFacade, IDatabaseContext context)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _userFacade = userFacade;
            _context = context;
        }
        // GET: api/<UsersController>
        [HttpGet]
        public IActionResult Get()
        {
            var users = _userManager.Users.ToList();
            List<ApiUserDto> userDto = new List<ApiUserDto>();
            foreach (var item in users)
            {
                ApiUserDto user = new ApiUserDto()
                {
                    UserId = item.Id,
                    Email = item.Email,
                    EmailConfirmed = item.EmailConfirmed,
                    FullName = item.FullName,
                    PhoneNumber = item.PhoneNumber,
                    PhoneNumberConfirmed = item.PhoneNumberConfirmed,
                    TwoFactorEnabled = item.TwoFactorEnabled,
                    Links = new List<ApiLinksDto>()
                    {
                        new ApiLinksDto()
                        {
                        Href = Url.Action("Get", "Users", new { item.Id }, Request.Scheme),
                        Method = "Get",
                        Rel = "Detail"
                        },
                        new ApiLinksDto()
                        {
                        Href = Url.Action("Put", "Users", new { item.Id }, Request.Scheme),
                        Method = "Put",
                        Rel = "Update"
                        },
                        new ApiLinksDto()
                        {
                        Href = Url.Action("Delete", "Users", new { item.Id }, Request.Scheme),
                        Method = "Delete",
                        Rel = "Delete"
                        },
                    },
                };
                userDto.Add(user);
            }
            return Ok(userDto);
        }

        //GET api/<UsersController>/5
        [HttpGet("{id}")]
        public IActionResult Get(string id)
        {
            var user = _userManager.FindByIdAsync(id).Result;

            ApiUserDto userDto = new ApiUserDto()
            {
                UserId = user.Id,
                Email = user.Email,
                EmailConfirmed = user.EmailConfirmed,
                FullName = user.FullName,
                PhoneNumber = user.PhoneNumber,
                PhoneNumberConfirmed = user.PhoneNumberConfirmed,
                TwoFactorEnabled = user.TwoFactorEnabled,
                Links = new List<ApiLinksDto>()
                {
                    new ApiLinksDto
                    {
                        Href = Url.Action("Put", "Users", new { user.Id }, Request.Scheme),
                        Method = "Put",
                        Rel = "Update"
                    },
                    new ApiLinksDto
                    {
                        Href = Url.Action("Delete", "Users", new {user.Id }, Request.Scheme),
                        Method = "Delete",
                        Rel = "Delete"
                    }

                },
            };

            return Ok(userDto);
        }

        // POST api/<UsersController>
        [HttpPost]
        public IActionResult Post([FromBody] ApiUserRegisterDto registerDto)
        {
            UserDto dto = new UserDto()
            {
                FullName = registerDto.FullName,
                Email = registerDto.Email,
                PhoneNumber = registerDto.PhoneNumber,
                TwoFactorEnabled = registerDto.TwoFactorEnabled,
                Password = registerDto.Password
            };

            var role = _roleManager.FindByNameAsync("Customer").Result;
            var user = _userFacade.AddUser.Execute(new UserDto
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
            return Created(Url.Action("Get", "Users", new { id = user.Data.UserId }, Request.Scheme), "User Created");
        }

        // PUT api/<UsersController>/5
        [HttpPut]
        public IActionResult Put([FromBody] ApiUserEditDto editDto)
        {
            var userResult = _userManager.FindByIdAsync(editDto.UserId).Result;
            if (userResult != null)
            {
                userResult.UserName = editDto.Email;
                userResult.Email = editDto.Email;
                userResult.FullName = editDto.FullName;
                userResult.TwoFactorEnabled = editDto.TwoFactorEnabled;
                userResult.PhoneNumber = editDto.PhoneNumber;
                var result = _userManager.UpdateAsync(userResult).Result;
                return Created(Url.Action("Get", "Users", new { id = editDto.UserId }, Request.Scheme), "User Edited");
            }
            else
            {
                ApiUserRegisterDto registerDto = new ApiUserRegisterDto()
                {
                    Email = editDto.Email,
                    FullName = editDto.FullName,
                    PhoneNumber = editDto.PhoneNumber,
                    TwoFactorEnabled = editDto.TwoFactorEnabled,
                };
                return Post(registerDto);
            }

        }

        // DELETE api/<UsersController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            var userResult = _userManager.FindByIdAsync(id).Result;
            var user = _context.Users.Where(p => p.Id == userResult.Id).FirstOrDefault();
            user.IsRemoved = true;
            _context.SaveChanges();
            return Ok();
        }
    }
}
