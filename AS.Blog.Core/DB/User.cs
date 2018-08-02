namespace AS.Blog.Core.DB
{
    public class User
    {
        public int UserId { get; set; }
        public string Email { get; set; }
        public string DisplayName { get; set; }
        public bool Active { get; set; }
        public string Password { get; set; }
        public string Hash { get; set; }
    }
}
