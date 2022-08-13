public class UserDto
{
    public string UserId { get; set; }
    public string FullName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string Password { get; set; }
    public string RePassword { get; set; }
    public List<RoleDto> Roles { get; set; }
}
