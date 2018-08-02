using AS.Blog.Core.DB;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AS.Blog.Core.Service
{
    public interface IBlog
    {
        Task NewPost();
        Task NewComment();

        Task<List<Post>> GetPosts();
        Task<Post> GetPost(string url);
        Task<Comment> GetComments(int postUrl);
    }
}
