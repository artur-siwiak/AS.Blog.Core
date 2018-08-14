using System.Collections.Generic;

namespace AS.Blog.Core.DB
{
    public class Role
    {
        public int RoleId { get; set; }
        public string Name { get; set; }

        public List<UserRoles> Users { get; set; }
    }
}
