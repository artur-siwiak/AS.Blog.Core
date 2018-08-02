using System;

namespace AS.Blog.Core.DB
{
    public class Post
    {
        public int PostId { get; set; }
        public int UserId { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime PublishDate { get; set; }
        public bool Deleted { get; set; }
        public string Url { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }
    }
}
