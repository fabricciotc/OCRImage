namespace Casero.Models
{
    public class ImagenOCR
    {
        public string Filename { get; set; }
        public IFormFile FormFile { get; set; }
        public string Result { get; set; }
    }
}
