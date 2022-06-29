namespace MiniBlog
{
    public interface ISettingsTable
    {
        Settings Settings { get; }
        Task UpdateSettingAsync(Settings? settings);
    }
}