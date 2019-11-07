using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using MediaCalender.Server.CsClasses;


namespace MediaCalender.Server
{
    public class Program
    {
        public static CsClasses.CsClasses Classes;
        public static void Main(string[] args)
        {
            EnsureDbCreated(false);
            //Classes = new CsClasses.CsClasses();

            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseConfiguration(new ConfigurationBuilder()
                    .AddCommandLine(args)
                    .Build())
                .UseStartup<Startup>()
                .Build();

        private static void EnsureDbCreated(bool reset = false)
        {
            using (Database ctx = new Database())
            {
                if (reset)
                    ctx.Database.EnsureDeleted();
                ctx.Database.EnsureCreated();
                //ctx.Database.Migrate();
            }
        }

    }

    
}
