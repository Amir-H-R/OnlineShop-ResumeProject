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
using System.Linq;
using UserTestingData;
using Xunit;
using Xunit.Sdk;

namespace ApplicationTest
{
    public class UsersTest
    {
        [Theory]
        [MemberData(nameof(UserData.GetUsers), MemberType = typeof(UserData))]
        public void GetUsers(List<UserDto> users)
        {
            //Arrange
            GetUsersResultDto usersResult = new GetUsersResultDto() { Users = users };
            GetUsersRequestsDto usersRequest = new GetUsersRequestsDto() { Page = 20, SearchKey = "" };

            var moq = new Mock<IGetUsersService>();

            //Act
            moq.Setup(p => p.Execute(usersRequest)).Returns(usersResult);
            var result = moq.Object.Execute(usersRequest);

            //Assert
            Assert.IsType<GetUsersResultDto>(result);
            Assert.IsAssignableFrom<List<UserDto>>(result.Users);
            Assert.NotEmpty(result.Users);
            Assert.NotNull(result.Users);
        }

        [Fact]
        public void GetRoles()
        {
            //Arrange

            List<RoleDto> roles = new List<RoleDto>();
            roles = UserData.GetRoles();
        
            ResultDto<List<RoleDto>> data = new ResultDto<List<RoleDto>>() { Data = roles };
            var moq = new Mock<IGetRolesService>();

            moq.Setup(p => p.Execute()).Returns(data);

            //Act
            var result = moq.Object.Execute();

            //Assert
            Assert.IsType<ResultDto<List<RoleDto>>>(result);
            Assert.NotNull(result.Data);
        }

        [Fact]
        public void AddUser()
        {
            //Arrange
            DatabaseContext context = new DatabaseBuilder().InMemoryDatabase();
            AddUserService addUser = new AddUserService(context);

            Role role = new Role() { Id = Guid.NewGuid().ToString(), Name = "LoL" };
            context.Roles.Add(role);
            context.SaveChanges();

            //Act
            var result = addUser.Execute(new UserDto { Email = "Amir.h.rouh@lol.com", FullName = "Amir", Password = "ArchangelArchangel", RePassword = "ArchangelArchangel", PhoneNumber = "09901913485", Roles = new List<RoleDto> { new RoleDto { Id = Guid.NewGuid().ToString() } } });

            //Assert
            Assert.IsType<ResultDto<UsersRegistrationResult>>(result);
            Assert.NotNull(result.Data);
            Assert.NotEmpty(result.Data.UserRoles);
        }

        [Theory]
        [MemberData(nameof(UserData.GetUsers), MemberType = typeof(UserData))]
        public void RemoveUser(List<UserDto> users)
        {
            //Arrange
            DatabaseContext context = new DatabaseBuilder().InMemoryDatabase();

            foreach (var item in UserData.GetRoles())
            {
                context.Roles.Add(new Role { Name = item.Name, Id = item.Id });
            }
            context.SaveChanges();

            //Act
            AddUserService addUser = new AddUserService(context);
            addUser.Execute(users.First());

            RemoveUserService removeUser = new RemoveUserService(context);
            var result = removeUser.Execute(1);

            //Assert
            Assert.True(result.IsSuccess,"Something's wrong");
        }

    }
}