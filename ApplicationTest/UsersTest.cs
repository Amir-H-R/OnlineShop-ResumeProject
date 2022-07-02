using Application.Interface.Context;
using Application.Services.Commands.AddUsers;
using Application.Services.Commands.RemoveUser;
using Application.Services.Common.UsersFacade;
using Application.Services.Queries.GetRoles;
using Application.Services.Queries.GetUsers;
using Common;
using Domain.Entities.Users_n_Roles;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Persistence.Context;
using System;
using System.Collections;
using System.Collections.Generic;
using Xunit;
using Xunit.Sdk;

namespace ApplicationTest
{
    public class UsersTest
    {
        // private DatabaseContext context;

        [Fact]
        public void GetUsers()
        {
            //Arrange
            List<GetUsersDto> getUsers = new List<GetUsersDto>()
            {
                 new GetUsersDto{Email = "xxx",FullName = "Amir",PhoneNumber = 09379509487,UserRole = "Admin" }
            };
            GetUsersResultDto usersResult = new GetUsersResultDto() { Users = getUsers, };
            GetUsersRequestsDto usersRequest = new GetUsersRequestsDto() { Page = 20, SearchKey = "" };

            var moq = new Mock<IGetUsersService>();

            //Act
            moq.Setup(p => p.Execute(usersRequest)).Returns(usersResult);
            var result = moq.Object.Execute(usersRequest);

            //Assert
            Assert.IsType<GetUsersResultDto>(result);
            Assert.IsAssignableFrom<List<GetUsersDto>>(result.Users);
            Assert.NotEmpty(result.Users);
            Assert.NotNull(result.Users);
        }

        [Fact]
        public void GetRoles()
        {
            //Arrange
            //  context = new DatabaseBuilder().SqlDatabase();
            //GetRolesService getRoles = new GetRolesService(context);

            List<Application.Services.Queries.GetRoles.RoleDto> roles = new List<Application.Services.Queries.GetRoles.RoleDto>()
            {
                new Application.Services.Queries.GetRoles.RoleDto{Name= "Admin"},
                new Application.Services.Queries.GetRoles.RoleDto{Name = "Operator"},
                new Application.Services.Queries.GetRoles.RoleDto{Name = "Customer"},
            };
            ResultDto<List<Application.Services.Queries.GetRoles.RoleDto>> data = new ResultDto<List<Application.Services.Queries.GetRoles.RoleDto>>() { Data = roles };
            var moq = new Mock<IGetRolesService>();
            moq.Setup(p => p.Execute()).Returns(data);

            //Act
            //var result = getRoles.Execute();
            var result = moq.Object.Execute();

            //Assert
            Assert.IsType<ResultDto<List<Application.Services.Queries.GetRoles.RoleDto>>>(result);
            Assert.NotNull(result.Data);
        }

        [Fact]
        public void AddUser()
        {
            //Arrange
            DatabaseContext context = new DatabaseBuilder().InMemoryDatabase();
            AddUserService addUser = new AddUserService(context);

            Role role = new Role() { RoleId = 4, Name = "LoL" };
            context.Roles.Add(role);
            context.SaveChanges();

            //Act
            var result = addUser.Execute(new UserDto { Email = "Amir.h.rouh@lol.com", FullName = "Amir", Password = "ArchangelArchangel", RePassword = "ArchangelArchangel", PhoneNumber = 09901913485, Roles = new List<RoleDto> { new RoleDto { Id = 4 } } });

            //Assert
            Assert.IsType<ResultDto<UsersRegistrationResult>>(result);
            Assert.NotNull(result.Data);
            Assert.NotEmpty(result.Data.UserRoles);
        }
        public void RemoveUser()
        {
            //Arrange
            //Act
            //Assert
        }

    }
}