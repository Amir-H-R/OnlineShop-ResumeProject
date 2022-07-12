using Domain.Entities.HomePage;
using Microsoft.AspNetCore.Http;


public class HomePageImageDto
{
    public IFormFile File { get; set; }
    public string Src { get; set; }
    public string Link { get; set; }
    public ImageLocation ImageLocation { get; set; }
}

