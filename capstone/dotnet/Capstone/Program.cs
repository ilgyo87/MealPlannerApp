using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;


namespace Capstone
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
            //mealPlannerApp mp = new mealPlannerApp("http//themealdb.com/api/json/v2/9973533");
            //mp.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}

