using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Task9.SqlRepository;

namespace Task9
{
    public class Program
    {
        public static void Main(string[] args)
        {

            CreateHostBuilder(args)
            .Build()
            .MigrateDatabase()
            .Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
