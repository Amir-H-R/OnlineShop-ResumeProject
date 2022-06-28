public class UserDto
{
    public long UserId { get; set; }
    public string FullName { get; set; }
    public string Email { get; set; }
    public long PhoneNumber { get; set; }
    public string Password { get; set; }
    public string RePassword { get; set; }
    public List<RoleDto> Roles { get; set; }
}
