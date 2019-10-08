using Microsoft.AspNetCore.Blazor.Hosting;

namespace MediaCalender.Client
{
    public class Program
    {
        public static CsClasses.CsClasses Classes;
        public static void Main(string[] args)
        {
            Classes = new CsClasses.CsClasses();
            CreateHostBuilder(args).Build().Run();
        }

        public static IWebAssemblyHostBuilder CreateHostBuilder(string[] args) =>
            BlazorWebAssemblyHost.CreateDefaultBuilder()
                .UseBlazorStartup<Startup>();
    }
}
