var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

//Inclue database service manager
var connect = builder.Configuration.GetConnectionString("ConnectSqlite");
builder.Services.AddSqlite<BlogDB>(connect);
builder.Services.AddTransient<ICategoryTable, CategoryTable>();
builder.Services.AddTransient<IPostTable, PostTable>();
builder.Services.AddTransient<ICommentTable, CommentTable>();
builder.Services.AddTransient<IUserTable, UserTable>();
builder.Services.AddTransient<ISettingsTable,SettingsTable>();
builder.Services.AddTransient<ManagerData>();


//Verification User
builder.Services.AddTransient<VerificationUser>();


builder.Services.AddResponseCaching();
builder.Services.AddAuthentication();
builder.Services.AddAuthorization();


// Cookie authentication.
builder.Services
    .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(
        options =>
        {
            options.LoginPath = "/login";
            options.LogoutPath = "/logout";
        });

var app = builder.Build();


app.UseHostFiltering();

// Configure the HTTP request pipeline.

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseStatusCodePagesWithReExecute("/Error");

app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(
    endpoints =>
    {
        //endpoints.MapControllerRoute("default", "/{controller=Blog}/{action=Index}/");
        endpoints.MapControllers();
    });

app.Run();