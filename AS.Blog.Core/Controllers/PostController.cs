using AS.Blog.Core.DB;
using AS.Blog.Core.Models;
using AS.Blog.Core.Service;
using AS.Utils;
using LanguageExt;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace AS.Blog.Core.Controllers
{
    [Authorize]
    public class PostController : Controller
    {
        private readonly ILogger<PostController> _log;
        private readonly IBlogService _blog;
        private readonly IUserService _user;

        public PostController(ILogger<PostController> log,
            IBlogService blogService,
            IUserService user)
        {
            _log = log;
            _blog = blogService;
            _user = user;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index(string postUrl)
        {
            var post = await GetPost(postUrl).ConfigureAwait(false);

            return
                post.Right(p => (IActionResult)View(p))
                .Left(x => x);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> New(PostModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _user.FindUserByName(User.Identity.Name).ConfigureAwait(false);

                var newPost = new Post
                {
                    Content = model.Content,
                    CreateDate = Clock.Now.DateTimeUtc,
                    Deleted = false,
                    Subject = model.Subject,
                    Url = model.Url,
                    User = user
                };
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string postUrl)
        {
            var post = await GetPost(postUrl).ConfigureAwait(false);

            return
                await post.Right(async p =>
                {
                    await _blog.EditPost(p).ConfigureAwait(false);

                    return (IActionResult)View();
                })
                .Left(Task.FromResult)
                .ConfigureAwait(false);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(string postUrl, PostModel postModel)
        {
            if (ModelState.IsValid)
            {
                var post = await GetPost(postUrl).ConfigureAwait(false);

                return
                    await post.Right(async p =>
                    {
                        await _blog.EditPost(p).ConfigureAwait(false);

                        return (IActionResult)View();
                    })
                    .Left(Task.FromResult)
                    .ConfigureAwait(false);
            }

            return View(postModel);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(string postUrl)
        {
            var post = await GetPost(postUrl).ConfigureAwait(false);

            return
                await post.Right(async p =>
                {
                    await _blog.DeletePost(p).ConfigureAwait(false);

                    return (IActionResult)View();
                })
                .Left(Task.FromResult)
                .ConfigureAwait(false);
        }

        private async Task<Either<IActionResult, Post>> GetPost(string postUrl)
        {
            if (postUrl.Length < 4)
            {
                return NotFound();
            }

            var post = await _blog.GetPost(postUrl).ConfigureAwait(false);

            return post == null
                ? (Either<IActionResult, Post>)NotFound()
                : (Either<IActionResult, Post>)post;
        }
    }
}