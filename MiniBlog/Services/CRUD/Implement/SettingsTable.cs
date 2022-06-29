namespace MiniBlog;

public class SettingsTable : ISettingsTable
{
    private readonly BlogDB blog;

    public SettingsTable(BlogDB blog)
    {
        this.blog = blog;
    }

    public Settings Settings => blog.Settings.First();

    public async Task UpdateSettingAsync(Settings? settings)
    {
        if (settings is null) return;

        blog.Settings.Update(settings);
        await blog.SaveChangesAsync();
    }

    #region If you want more Settings Add this methods in interface
    public async Task AddSettingsAsync(Settings? settings)
    {
        if (settings is null) return;

        await blog.Settings.AddAsync(settings);
        await blog.SaveChangesAsync();
    }

    public async Task DeleteSettingAsync(int id)
    {
        var findSetting = await blog.Settings.SingleOrDefaultAsync(set => set.Id == id);

        if (findSetting is null) return;

        blog.Settings.Remove(findSetting);
        await blog.SaveChangesAsync();
    }

    public async Task<Settings[]> GetAllSettingsAsync()
        => await blog.Settings.ToArrayAsync();

    public async Task<Settings?> GetSettingAsync(int id)
        => await blog.Settings.SingleOrDefaultAsync(set => set.Id == id);
    
    #endregion
}