using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

public class LoginDto
{
    public string Email { get; set; }
    public string Password { get; set; }
    public bool IsPersistent { get; set; } = false;
    public string Code { get; set; }
    public string Provider { get; set; }

    public string ReturnUrl { get; set; }
}

