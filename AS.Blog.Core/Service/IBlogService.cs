using AS.Blog.Core.DB;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AS.Blog.Core.Service
{
    public interface IBlogService
    {
        Task NewPost(Post newPost);
        Task NewComment(Comment newComment);

        Task<List<Post>> GetPosts();
        Task<Post> GetPost(string url);
        Task<List<Comment>> GetComments(string postUrl);
        Task DeletePost(Post post);
        Task EditPost(Post p);
    }
}
