using System;

namespace AS.Blog.Core.Models
{
    public class CommentModel
    {
        public string Author { get; set; }
        public string Content { get; set; }
        public string Ip { get; set; }
        public DateTime Created { get; set; }
        public bool Deleted { get; set; }
    }
}
