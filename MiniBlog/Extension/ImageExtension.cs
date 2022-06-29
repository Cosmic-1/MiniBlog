namespace MiniBlog.Extension
{
    public static class ImageExtension
    {
        public static async Task<string> SaveImageAsync(this IFormFile form, string path = "\\images\\post\\", string? nameFile = null)
        {
            string IsNullOrEmpty(string fileType)
                => string.IsNullOrEmpty(nameFile) ? $"{Guid.NewGuid()}.{fileType}" : nameFile.CreateSlug();

            switch (form.ContentType)
            {
                case "image/gif":
                    path += IsNullOrEmpty("gif");
                    break;
                case "image/jpeg":
                    path += IsNullOrEmpty("jpg");
                    break;
                case "image/png":
                    path += IsNullOrEmpty("png");
                    break;
                default:
                    path += form.FileName.CreateSlug();
                    break;
            }

            //Open Stream File and read bytes
            using var file = form.OpenReadStream();
            var byteArray = new byte[file.Length];
            await file.ReadAsync(byteArray);

            var pathCreate = $"{Environment.CurrentDirectory}\\wwwroot{path}";

            //Create File and save in folder
            using var createFile = File.Create(pathCreate);
            await createFile.WriteAsync(byteArray);

            return path;

        }

        public static void RemoveImage(string? pathImage)
        {
            if (pathImage is null) return;

            var pathDelete = $"{Environment.CurrentDirectory}\\wwwroot{pathImage}";

            File.Delete(pathDelete);
        }
    }
}
