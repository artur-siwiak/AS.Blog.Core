namespace AS.Blog.Core.DB
{
    public class Comment
    {
        public int CommentId { get; set; }
        public int? UserId { get; set; }
        public string UserName { get; set; }
        public string Content { get; set; }
    }
}
