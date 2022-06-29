using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MiniBlog.Controllers
{
    public class AdminController : Controller
    {
        private readonly ILogger<AdminController> logger;
        private readonly ManagerData manager;

        public AdminController(ILogger<AdminController> logger, ManagerData manager)
        {
            this.logger = logger;
            this.manager = manager;
        }

        [HttpGet("/admin", Name = "AdminRoute"), Authorize]
        public IActionResult Index()
        {
            return View();
        }

        #region List

        [HttpGet("/admin/categories/{pageId:int?}", Name = "ListCategoriesRoute"), Authorize]
        public async Task<IActionResult> ListCategoryAsync([FromRoute] int pageId)
        {
            var setting = manager.Settings.Settings;

            var categories = await manager.Categories.TakeCategoriesByPageAsync(pageId, setting.ViewSize);

            this.Pagenavigation("/admin/categories", categories.Length, pageId, setting.ViewSize);

            return View(categories);
        }

        [HttpGet("/admin/posts/{pageId:int?}", Name = "ListPostsRoute"), Authorize]
        public async Task<IActionResult> ListPostAsync([FromRoute] int pageId)
        {
            var setting = manager.Settings.Settings;

            var posts = await manager.Posts.TakePostsByPageAsync(pageId, setting.ViewSize);

            this.Pagenavigation("/admin/posts", posts.Length, pageId, setting.ViewSize);

            return View(posts);
        }

        [HttpGet("/admin/comments/{pageId:int?}", Name = "ListCommentsRoute"), Authorize]
        public async Task<IActionResult> ListCommentAsync([FromRoute] int pageId)
        {
            var setting = manager.Settings.Settings;

            var comments = await manager.Comments.GetCommentsByPageAsync(pageId, setting.ViewSize);

            this.Pagenavigation("/admin/comments", comments.Length, pageId, setting.ViewSize);

            return View(comments);
        }
        #endregion

        #region Edit

        [HttpGet("/admin/categories/{categoryId:int}/edit", Name = "CategoryEditRoute"), Authorize]
        public async Task<IActionResult> EditCategoryAsync([FromRoute] int categoryId)
        {
            var findCategory = await manager.Categories.GetCategoryByIdAsync(categoryId);

            if (findCategory is null) return NotFound();

            return View(findCategory);
        }

        [HttpPost("/admin/categories/{categoryId:int}/edit", Name = "CategoryEditRoute"), Authorize]
        public async Task<IActionResult> EditCategoryAsync([FromRoute] int categoryId, [FromForm] Category? category)
        {
            if (category is not null && ModelState.IsValid)
            {
                var findCategory = await manager.Categories.GetCategoryByIdAsync(categoryId);

                if(findCategory is null) return NotFound();

                findCategory.Slug = category.Slug;
                findCategory.Title = category.Title;
                findCategory.Content = category.Content;

                findCategory.MetaDescription = category.MetaDescription;
                findCategory.MetaTitle = category.MetaTitle;
                findCategory.MetaKeywords = category.MetaKeywords;

                await manager.Categories.UpdateCategoryAsync(findCategory);

                return RedirectToRoute("CategoryRoute", new { categoryId = categoryId });
            }

            return View(category);
        }


        [HttpGet("/admin/posts/{postId:int}/edit", Name = "PostEditRoute"), Authorize]
        public async Task<IActionResult> EditPostAsync([FromRoute] int postId)
        {
            ViewBag.ListCategory = await manager.Categories.GetAllCategoriesAsync();

            var findPost = await manager.Posts.GetPostByIdAsync(postId);

            if (findPost is null) return NotFound();

            return View(findPost);
        }

        [HttpPost("/admin/posts/{postId:int}/edit", Name = "PostEditRoute"), Authorize]
        public async Task<IActionResult> EditPostAsync([FromRoute] int postId, [FromForm] Post? post)
        {
            if (post is not null && ModelState.IsValid)
            {
                var findPost = await manager.Posts.GetPostByIdAsync(postId);

                if (findPost is null) return NotFound();

                findPost.Title = post.Title;
                findPost.Slug = post.Slug;
                findPost.ShortContent = post.ShortContent;
                findPost.Content = post.Content;
                findPost.DateAddPost = post.DateAddPost;
                findPost.IsPublished = post.IsPublished;
                findPost.Category = await manager.Categories.GetCategoryByIdAsync(post.Category.Id);

                findPost.MetaTitle = post.MetaTitle;
                findPost.MetaDescription = post.MetaDescription;
                findPost.MetaKeywords = post.MetaKeywords;

                await manager.Posts.UpdatePostAsync(findPost);

                return RedirectToRoute("PostRoute", new { postId = postId });
            }

            return View(post);
        }

        [HttpGet("/admin/comments/{commentId:int}/edit", Name = "CommentEditRoute"), Authorize]
        public async Task<IActionResult> EditCommentAsync([FromRoute] int commentId)
        {
            var findComment = await manager.Comments.GetCommentByIdAsync(commentId);

            if (findComment is null) return NotFound();

            return View(findComment);
        }

        [HttpPost("/admin/comments/{commentId:int}/edit", Name = "CommentEditRoute"), Authorize]
        public async Task<IActionResult> EditCommentAsync([FromRoute] int commentId, [FromForm] Comment? comment)
        {
            if (comment is not null && ModelState.IsValid)
            {
                var findComment = await manager.Comments.GetCommentByIdAsync(commentId);

                if (findComment is null) return NotFound();

                findComment.Nickname = comment.Nickname;
                findComment.Email = comment.Email;
                findComment.Content = comment.Content;
                findComment.DateAdd = comment.DateAdd;

                await manager.Comments.UpdateCommentAsync(findComment);

                return RedirectToRoute("ListCommentsRoute");
            }

            return View(comment);
        }

        #endregion

        #region Add
        [HttpGet("/admin/categories/add", Name = "AddCategoryRoute"), Authorize]
        public IActionResult AddCategory()
        {
            return View();
        }

        [HttpPost("/admin/categories/add", Name = "AddCategoryRoute"), Authorize]
        public async Task<IActionResult> AddCategoryAsync([FromForm] Category? category)
        {
            if (category is null) return View();

            if (ModelState.IsValid)
            {
                await manager.Categories.AddCategoryAsync(category);

                return RedirectToRoute("ListCategoriesRoute");
            }

            return View(category);
        }


        [HttpGet("/admin/posts/add", Name = "AddPostRoute"), Authorize]
        public async Task<IActionResult> AddPostAsync()
        {
            ViewBag.ListCategory = await manager.Categories.GetAllCategoriesAsync();

            return View();
        }

        [HttpPost("/admin/posts/add", Name = "AddPostRoute"), Authorize]
        public async Task<IActionResult> AddPostAsync([FromForm] Post? post)
        {
            if (post is not null && ModelState.IsValid)
            {
                post.Category = await manager.Categories.GetCategoryByIdAsync(post.Category.Id);

                await manager.Posts.AddPostAsync(post);

                return RedirectToRoute("ListPostsRoute");
            }

            return View(post);
        }

        #endregion

        #region Remove
        [HttpGet("/admin/categories/{categoryId:int}/delete", Name = "DeleteCategoryRoute"), Authorize]
        public async Task<IActionResult> DeleteCategoryAsync([FromRoute] int categoryId)
        {

            var findCategory = await manager.Categories.GetCategoryByIdAsync(categoryId);

            if (findCategory is null) return NotFound();

            await manager.Categories.DeleteCategoryAsync(findCategory);

            return RedirectToRoute("ListCategoriesRoute");

        }

        [HttpGet("/admin/posts/{postId:int}/delete", Name = "DeletePostRoute"), Authorize]
        public async Task<IActionResult> DeletePostAsync([FromRoute] int postId)
        {
            var findPost = await manager.Posts.GetPostByIdAsync(postId);

            if (findPost is null) return NotFound();

            await manager.Posts.DeletePostAsync(findPost);

            return RedirectToRoute("ListPostsRoute");
        }

        [HttpGet("/admin/comments/{commentId:int}/delete", Name = "DeleteCommentRoute"), Authorize]
        public async Task<IActionResult> DeleteCommentAsync([FromRoute] int commentId)
        {
            var findComment = await manager.Comments.GetCommentByIdAsync(commentId);

            if (findComment is null) return NotFound();

            await manager.Comments.DeleteCommentAsync(findComment);

            return RedirectToRoute("ListCommentsRoute");
        }

        #endregion
    }
}
