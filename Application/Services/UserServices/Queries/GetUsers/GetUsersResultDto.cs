namespace Application.Services.Queries.GetUsers
{
    public class GetUsersResultDto
    {
        public List<UserDto> Users { get; set; }
        public int Rows { get; set; }
    }
}
