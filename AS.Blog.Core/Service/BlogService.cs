using AS.Blog.Core.DB;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AS.Blog.Core.Service
{
    public class BlogService : IBlogService
    {
        private readonly BlogContext _context;

        public BlogService(BlogContext context)
        {
            _context = context;
        }

        public Task DeletePost(Post post)
        {
            throw new System.NotImplementedException();
        }

        public Task EditPost(Post p)
        {
            throw new System.NotImplementedException();
        }

        public Task<List<Comment>> GetComments(string postUrl)
        {
            return Task.FromResult(
                _context
                    .Comments
                    .Where(x => x.Post.Url == postUrl)
                    .ToList());
        }

        public Task<Post> GetPost(string url)
        {
            return Task.FromResult(
                _context.Posts
                    .Where(x => x.Url == url).FirstOrDefault()
                );
        }

        public Task<List<Post>> GetPosts()
        {
            return Task.FromResult(
                _context.Posts.ToList()
                );
        }

        public async Task NewComment(Comment newComment)
        {
            _context.Comments.Add(newComment);

            await _context.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task NewPost(Post newPost)
        {
            _context.Posts.Add(newPost);

            await _context.SaveChangesAsync().ConfigureAwait(false);
        }
    }
}
