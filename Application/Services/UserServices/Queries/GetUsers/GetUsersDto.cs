namespace Application.Services.Queries.GetUsers
{
    public class GetUsersDto
    {
        public long UserId { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
         public string UserRole { get; set; }
        public long PhoneNumber { get; set; }
    }
}
