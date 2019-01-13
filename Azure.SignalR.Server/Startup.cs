using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Azure.SignalR.Server.Hubs;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication;
using Azure.SignalR.Server.Authentication;

namespace Azure.SignalR.Server
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthentication().AddScheme<AuthenticationSchemeOptions, SignalRAuthenticationHandler>("SignalR","", options => { });
            services.AddCors();
            services.AddSignalR().AddAzureSignalR("{CONNECTION_STRING}");
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseCors(policy =>
            {
                policy.AllowAnyHeader();
                policy.WithOrigins(new [] { "http://localhost:4200"});
                policy.AllowAnyMethod();
                policy.AllowCredentials();
            });
            app.UseAzureSignalR(routes =>
            {
                routes.MapHub<Chat>("/chat");
            });
        }
    }
}
