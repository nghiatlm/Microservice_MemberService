

using Microsoft.Extensions.DependencyInjection;

namespace MemberService.Repository.Configuration
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddRepoDI(this IServiceCollection services)
        {
            services.AddScoped<IPackageTypeRepository, PackageTypeRepository>();
            services.AddScoped<IPackageRepository, PackageRepository>();
            services.AddScoped<IMembershipRepository, MembershipRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IPaymentRepository, PaymentRepository>();

            return services;
        }
    }
}