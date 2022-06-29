namespace MiniBlog.Components
{
    public class MenuCategoryComponent : ViewComponent
    {
        private readonly ManagerData manager;

        public MenuCategoryComponent(ManagerData manager)
        {
            this.manager = manager;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var categories = await manager.Categories.GetAllCategoriesAsync();

            return View("/Views/Shared/Components/_Menu.cshtml", categories);
        }
    }
}
