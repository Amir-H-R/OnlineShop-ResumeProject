using Microsoft.AspNetCore.Http;

    public class SliderDto
    {
        public string Link { get; set; }
        public string Src{ get; set; }
        public IFormFile File { get; set; }
    }

