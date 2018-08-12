using System.Collections.Generic;

namespace AS.Blog.Core.DB
{
    public class Role
    {
        public int Id { get; set; }
        public string Name { get; set; }

        //public int PolicyId { get; set; }
        //public Policy Policy { get; set; }

        public List<UserRole> Users { get; set; }
    }
}
