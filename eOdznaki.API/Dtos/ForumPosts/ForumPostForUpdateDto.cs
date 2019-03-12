using System.ComponentModel.DataAnnotations;

namespace eOdznaki.Dtos.ForumPosts
{
    public class ForumPostForUpdateDto
    {
        [Required] [MaxLength(2000)] public string Content { get; set; }
    }
}