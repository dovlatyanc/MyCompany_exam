using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MyCompany.Domain;
using MyCompany.Domain.Repositories.Abstract;
using MyCompany.Domain.Repositories.EntityFramework;
using MyCompany.Infrastructure;

namespace MyCompany
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            WebApplicationBuilder builder = WebApplication.CreateBuilder(args);


            //Подключаем в конфигурацию файл аппсеттингс джейсон
            IConfigurationBuilder configBuild = new ConfigurationBuilder()
                .SetBasePath(builder.Environment.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables();

            //Оборачиваем секцию проджект в объектную форму для улдобства
            IConfiguration configuration = configBuild.Build();

            AppConfig config = configuration.GetSection("Project").Get<AppConfig>()!;
            var connectionString = builder.Configuration["Project:Database:ConnectionString"];
            //подключаем контекст БД
            builder.Services.AddDbContext<AppDbContext>(x => x.UseSqlServer(connectionString));


            builder.Services.AddTransient<IServiceCategoriesRepository, EFServiceCategoriesRepository>();
            builder.Services.AddTransient<IServicesRepository, EFServicesRepository>();
            builder.Services.AddTransient<DataManager>();
            //настройка идентити
            builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
            {
                options.User.RequireUniqueEmail = true;
                options.Password.RequiredLength = 6;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireDigit = false;

            }).AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();


            //настройки Auth Cookie

            builder.Services.ConfigureApplicationCookie(options =>
            {
                options.Cookie.Name = "myCompanyAuth";
                options.Cookie.HttpOnly = true;
                options.LoginPath = "/admin/login";
                options.AccessDeniedPath = "/admin/accesdenied";
                options.SlidingExpiration = true;

            });
            //add controllers


            builder.Services.AddControllersWithViews();


            WebApplication app = builder.Build();

            //подключаем использование статичных  файлов
            app.UseStaticFiles();
            //маршрутизация
            app.UseRouting();
            //авторизация и аутентификация
            app.UseCookiePolicy();
            app.UseAuthentication();
            app.UseAuthorization();

            //регистрируем маршруты
            app.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");

            await app.RunAsync();
        }
    }
}
