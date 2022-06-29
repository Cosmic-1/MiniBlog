
namespace MiniBlog.Controllers
{
    public class BlogController : Controller
    {
        private readonly ILogger<BlogController> logger;
        private readonly ManagerData manager;

        public BlogController(ILogger<BlogController> logger, ManagerData manager)
        {
            this.logger = logger;
            this.manager = manager;
        }

        [HttpGet("/{pageId:int?}")]
        [ResponseCache(NoStore = true, Duration = 3000)]
        public async Task<IActionResult> IndexAsync([FromRoute] int pageId)
        {
            if (pageId < 0) return NotFound();

            var setting = manager.Settings.Settings;

            var postsTake = await manager.Posts
                .TakePostsByPageAsync(pageId, setting.ViewSize);

            this.MetaEdit(pageId == 0 ? $"{setting.Title}" : $"{setting.Title} - Page {pageId}",
           setting.Description,
           setting.Keywords);

            this.Pagenavigation(string.Empty, postsTake.Length, pageId, setting.ViewSize);

            return View(postsTake);
        }


        [HttpGet("/category/{categoryId:int}/{pageId:int?}", Name = "CategoryRoute")]
        [ResponseCache(NoStore = true, Duration = 3000)]
        public async Task<IActionResult> CategoryAsync([FromRoute] int categoryId, [FromRoute] int pageId)
        {
            var category = await manager.Categories.GetCategoryByIdAsync(categoryId);

            if (category is null) return NotFound();

            var setting = manager.Settings.Settings;

            category.Posts = (await manager.Posts.GetPostsByCategoryAsync(categoryId, pageId, setting.ViewSize)).ToList();

            this.MetaEdit(category.MetaTitle,
                category.MetaDescription,
                category.MetaKeywords);

            this.Pagenavigation($"/category/{categoryId}", category.Posts.Count, pageId, setting.ViewSize);

            return View(category);
        }

        [HttpGet("/post/{postId:int}", Name = "PostRoute")]
        [ResponseCache(NoStore = true, Duration = 3000)]
        public async Task<IActionResult> PostAsync([FromRoute] int postId)
        {
            var post = await manager.Posts.GetPostByIdAsync(postId);

            if (post is null) return NotFound();

            this.MetaEdit(post.MetaTitle,
           post.MetaDescription,
           post.MetaKeywords);

            return View(post);
        }

        [HttpPost("/post/{postId:int}/comment/add", Name = "PostAddRoute")]
        public async Task<IActionResult> AddCommentAsync([FromRoute] int postId, [FromForm] Comment? comment)
        {
            if (comment is null)
                return RedirectToRoute("PostRoute", new { postId = postId });

            var post = await manager.Posts.GetPostByIdAsync(postId);

            if (post is null) return NotFound();

            comment.Post = post;
            await manager.Comments.AddCommentAsync(comment);

            return RedirectToRoute("PostRoute", new { postId = postId });
        }

        [HttpGet("/search", Name = "SearchRoute")]
        public async Task<IActionResult> SearchAsync([FromQuery] string? search)
        {
            if (string.IsNullOrEmpty(search)) return View(null);

            var postsFind = (await manager.Posts.GetPostsAsync())
                .Where(p => p.Title.Contains(search))
                 .Select(p => new SearchModel { PostId = p.Id, Title = p.Title });

            return View(postsFind);
        }


        [HttpGet("/Error")]
        public IActionResult Error()
        {
            ErrorModel error = new()
            {
                CodeError = Response.StatusCode,
                Message = $"Error code {Response.StatusCode}"
            };

            return View(error);
        }
    }
}
