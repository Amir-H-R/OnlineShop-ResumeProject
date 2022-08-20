namespace UserTestingData
{
    public static class UserData
    {
        public static List<object[]> GetUsers()
        {
            List<object[]> users = new List<object[]>();

            users.Add(new object[] { new List<UserDto>
            { new UserDto{
               // UserId = 1,
                FullName = "TestUser1",
                Email = "Test@Test.com",
                Password = "TestingPassword",
                RePassword = "TestingPassword",
                PhoneNumber = "00000000000",
                Roles = GetRoles(), } }
            });
            users.Add(new object[] { new List<UserDto>
            { new UserDto{
              //  UserId=2,
                FullName = "TestUser2",
                Email = "Test2@Test.com",
                Password = "Testing2Password",
                RePassword = "Testing2Password",
                PhoneNumber = "00000000001",
                Roles = GetRoles(),} }
            });
            users.Add(new object[] { new List<UserDto>
            { new UserDto{
               // UserId=3,
                FullName = "TestUser3",
                Email = "Test3@Test.com",
                Password = "Testing3Password",
                RePassword = "Testing3Password",
                PhoneNumber = "00000000010",
                Roles = GetRoles(),} }
            });

            return users;
        }

        public static List<RoleDto> GetRoles()
        {
            List<RoleDto> roles = new List<RoleDto>();
            roles.Add(new RoleDto { Name = "Admin",Id = Guid.NewGuid().ToString() });
            roles.Add(new RoleDto { Name = "Operator", Id =Guid.NewGuid().ToString() });
            roles.Add(new RoleDto { Name = "Customer", Id = Guid.NewGuid().ToString() });
            return roles;
        }
    }
}