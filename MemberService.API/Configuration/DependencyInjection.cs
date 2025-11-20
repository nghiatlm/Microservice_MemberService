using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MemberService.Repository.Configuration;
using MemberService.Service.Configuration;

namespace MemberService.API.Configuration
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddAppDI(this IServiceCollection services)
        {
            services.AddServiceDI().AddRepoDI().AddSwaggerDependencies();
            return services;
        }
    }
}