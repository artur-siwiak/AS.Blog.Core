using System.Collections.Generic;

namespace AS.Blog.Core.DB
{
    public class Policy
    {
        public int PolicyId { get; set; }
        public string Name { get; set; }

        public List<Role> Roles { get; set; }
    }
}
