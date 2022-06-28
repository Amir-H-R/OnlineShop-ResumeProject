namespace Application.Services.Queries.GetUsers
{
    public class GetUsersResultDto
    {
        public List<GetUsersDto> Users { get; set; }
        public int Rows { get; set; }
    }
}
