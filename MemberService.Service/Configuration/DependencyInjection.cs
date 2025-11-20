
using System.Net.Http.Headers;
using MemberService.Service.Services;
using Microsoft.Extensions.DependencyInjection;

namespace MemberService.Service.Configuration
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddServiceDI(this IServiceCollection services)
        {
            var authServiceUrl = Environment.GetEnvironmentVariable("AUTH_SERVICE_URL") ?? "https://localhost:7053";

            services.AddHttpClient<IAuthClient, AuthClient>(client =>
            {
                client.BaseAddress = new Uri(authServiceUrl);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            });

            services.AddScoped<IPackageTypeService, PackageTypeService>();
            services.AddScoped<IPackageService, PackageService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IPayosService, PayosService>();
            return services;
        }
    }
}