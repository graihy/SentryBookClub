using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Sentry;
using Sentry.Extensibility;

namespace BookClub.API
{
    public class Program
    {
        public static void Main(string[] args)
        {

            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {

                    //webBuilder.UseSentry(o =>
                    //{
                    //    o.BeforeSend = @event =>
                    //    {
                    //        @event.User.Id = null;
                    //        return @event;
                    //    };
                    //});
                    webBuilder.UseStartup<Startup>();
                });
    }
}
