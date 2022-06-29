namespace MiniBlog.Extension
{
    public static class ControllerExtension
    {
        public static void MetaEdit(this Controller controller, string title, string description, string keywords)
        {
            controller.ViewBag.Title = NullOrValue(title);
            controller.ViewBag.Description = NullOrValue(description);
            controller.ViewBag.Keywords = NullOrValue(keywords);
        }
        public static void Pagenavigation(this Controller controller, string path, int countElements, int pageId, int size)
        {
            controller.ViewBag.PreviewPage = pageId > 0 
                ? $"{path}/{pageId - 1}" 
                : null;

            controller.ViewBag.NextPage = countElements == size
                ? $"{path}/{(pageId <= 1 ? $"{pageId + 1}": string.Empty)}" 
                : null;
        }

       static string? NullOrValue(string value)
             => string.IsNullOrEmpty(value) ? null : value;
    }
}
