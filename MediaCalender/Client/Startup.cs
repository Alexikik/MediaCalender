using Microsoft.AspNetCore.Components.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace MediaCalender.Client
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            //System.ComponentModel.ReferenceConverter;
        }

        public void Configure(IComponentsApplicationBuilder app)
        {
            app.AddComponent<App>("app");
        }
    }
}
