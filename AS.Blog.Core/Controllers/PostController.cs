using AS.Blog.Core.DB;
using AS.Blog.Core.Helpers;
using AS.Blog.Core.Models;
using AS.Blog.Core.Security;
using AS.Blog.Core.Service;
using AS.Utils;
using LanguageExt;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AS.Blog.Core.Controllers
{
    [Authorize(Roles = "Post")]
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

        public IActionResult Index()
        {
            return RedirectToActionPermanent(nameof(HomeController.Index), "Home");
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Entry(string id)
        {
            var post = await GetPost(id).ConfigureAwait(false);

            return
                post.Right(p => (IActionResult)View(p))
                .Left(x => x);
        }

        public IActionResult New()
        {
            return View();
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
                    Url = model.Url.Pretty(),
                    User = user
                };

                await _blog.NewPost(newPost).ConfigureAwait(false);

                return RedirectToAction(nameof(PostController.Entry), new { id = newPost.Url });
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

            if (post == null)
            {
                return (Either<IActionResult, Post>)NotFound();
            }

            if (Clock.Now.DateTimeUtc < post.PublishDate || post.Deleted)
            {
                var permissions = new List<string>
                {
                    nameof(PoliciesEnum.Administrator),
                    nameof(PoliciesEnum.Post)
                };

                if (!User.Claims.Any(x => permissions.Contains(x.Value)))
                {
                    var user = User.Identity.IsAuthenticated
                        ? await _user.FindUserByName(User.Identity.Name)
                            .ConfigureAwait(false)
                        : new User();

                    if (post.UserId != user.UserId)
                    {
                        return (Either<IActionResult, Post>)NotFound();
                    }
                }
            }

            return post == null
                ? (Either<IActionResult, Post>)NotFound()
                : (Either<IActionResult, Post>)post;
        }
    }
}