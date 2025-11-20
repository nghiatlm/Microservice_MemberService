
using MemberService.Service.Services;
using Microsoft.Extensions.DependencyInjection;

namespace MemberService.Service.Configuration
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddServiceDI(this IServiceCollection services)
        {
            services.AddScoped<IPackageTypeService, PackageTypeService>();
            services.AddScoped<IPackageService, PackageService>();
            return services;
        }
    }
}