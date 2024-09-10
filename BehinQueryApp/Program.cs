using _0_FrameWork.FW.Application;
using _01_FrameWork.QueryCommand;
using AccountManagement.Infrastrure;
using BehinQueryApp;
using BehinQueryApp.Db;
using BehinQueryApp.Model.QueryCommandAgg;
using BehinQueryApp.Service;
using BehinQueryApp.Service.QueyCommand;
using BehinQueryApp.Service.QueyCommandLog;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddRazorPages();

        builder.Services.AddDbContext<BehinQueryContext>(options =>
        {
            options.UseSqlServer("Data Source=DESKTOP-SD68BLV;Initial Catalog=BehinQueryDb;Integrated Security=True;TrustServerCertificate=True");
        });
		builder.Services.AddDbContext<AccountContext>(options =>
		{
			options.UseSqlServer("Data Source=DESKTOP-SD68BLV;Initial Catalog=BehinQueryDb;Integrated Security=True;TrustServerCertificate=True");
		});

		builder.Services.AddHttpContextAccessor();

        builder.Services.AddSession();
        builder.Services.AddScoped<IQueryCommandRepository, QueryCommandRepository>();
        builder.Services.AddScoped<IQueryCommandAdoRepository, QueryCommandAdoRepository>();
        builder.Services.AddScoped<IQueryCommandSession, QueryCommandSession>();
        builder.Services.AddTransient<IQueryCmdAdo, QueryCmdAdo>();
		builder.Services.AddTransient<IAuthHelper, AuthHelper>();
		builder.Services.AddTransient<IPasswordHasher, PasswordHasher>();
		builder.Services.AddTransient<IQueryCommandLogRepository, QueryCommandLogRepository>();


		#region Cookie
		builder.Services.AddHttpContextAccessor();

		builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
			.AddCookie(options =>
			{
				options.LoginPath = "/Accounts/Signing";
				options.LogoutPath = "/Accounts/signup";
				options.ExpireTimeSpan = TimeSpan.FromDays(30);
			});


		#endregion
		AccountConfigBootstrapper.Configure(builder.Services);

		var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

		app.UseAuthentication();
		app.UseAuthorization();

		app.UseSession();

        app.MapRazorPages();

        app.Run();
    }
}