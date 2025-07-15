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
            //add controllers
            builder.Services.AddControllersWithViews();


            WebApplication app = builder.Build();
          
            //подключаем использование статичных  файлов
            app.UseStaticFiles();
            //маршрутизация
            app.UseRouting();

            //регистрируем маршруты
            app.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");

            await app.RunAsync();
        }
    }
}
