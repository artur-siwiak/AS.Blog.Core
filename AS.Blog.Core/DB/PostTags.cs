namespace AS.Blog.Core.DB
{
    public class PostTags
    {
        public int PostTagsId { get; set; }
        public int PostId { get; set; }
        public int TagId { get; set; }

        public Post Post { get; set; }
        public Tag Tag { get; set; }
    }
}
