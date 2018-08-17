using System.Collections.Generic;

namespace AS.Blog.Core.DB
{
    public class Tag
    {
        public int TagId { get; set; }
        public string Name { get; set; }

        public List<PostTags> Posts { get; set; }
    }
}
