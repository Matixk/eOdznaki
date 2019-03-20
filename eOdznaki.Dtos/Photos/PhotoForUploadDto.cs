using Microsoft.AspNetCore.Http;

namespace eOdznaki.Dtos
{
    public class PhotoForUploadDto
    {
        public string Url { get; set; }
        public IFormFile File { get; set; }
        public string PublicId { get; set; }
    }
}