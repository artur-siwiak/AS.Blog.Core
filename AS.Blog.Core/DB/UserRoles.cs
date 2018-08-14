namespace AS.Blog.Core.DB
{
    public class UserRoles
    {
        public int UserRolesId { get; set; }
        public int UserId { get; set; }
        public int RoleId { get; set; }

        public User User { get; set; }
        public Role Role { get; set; }
    }
}
