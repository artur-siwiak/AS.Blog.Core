using System.ComponentModel.DataAnnotations;

namespace AS.Blog.Core.Models
{
    public class PostModel
    {
        [Required]
        [Display(Name = "Subject", Description = "Subject of new post")]
        public string Subject { get; set; }

        [Required]
        [Display(Name = "Content", Description = "Content of new post")]
        public string Content { get; set; }

        [Required]
        [MinLength(4, ErrorMessage = "Url for post is too short")]
        public string Url { get; internal set; }
    }
}
