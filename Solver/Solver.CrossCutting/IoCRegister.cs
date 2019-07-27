namespace Solver.CrossCutting
{
    using System;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using Solver.BusinessLayer.Services;
    using Solver.DataAccessLayer;

    public static class IoCRegister
    {
        public static IServiceCollection AddRepository(IServiceCollection services)
        {
            //services.AddScoped<IProgramRepository,ProgramRepository>();
          

            return services;
        }
        public static IServiceCollection AddServices(IServiceCollection services)
        {
            //services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();


            //services.AddScoped<IProgramService, ProgramService>();
            
           



            return services;
        }
        public static IServiceCollection AddDbContext(IServiceCollection services, string DefaultConnection)
        {
            
            services.AddDbContext<ApplicationDataContext>(options =>
            options.UseSqlServer(DefaultConnection, b => b.MigrationsAssembly("Solver.API")), ServiceLifetime.Transient);

            return services;
        }

        public static IServiceCollection AddTransientRepository(IServiceCollection services)
        {
            #region IAccountRespository

            //services.AddTransient<Func<string, IAccountRepository>>(serviceProvider => provider =>
            // {
            //     if (provider != null)
            //     {
            //         return (IAccountRepository)serviceProvider.GetService(Type.GetType(string.Format(provider, "AccountRepository")));
            //     }
            //     else
            //     {
            //         return serviceProvider.GetService<AccountRepository>();
            //     }
            // });
            #endregion

            return services;
        }

        public static IServiceCollection AddTransientServices(IServiceCollection services)
        {

            #region IAccountService
            services.AddTransient<IUploadServices, BusinessLayer.Providers.TechnicalTest.UploadServices>();
            //services.AddTransient<Func<string, IAccountService>>(serviceProvider => provider =>
            //{
            //    if (!string.IsNullOrEmpty(provider))
            //    {
            //        string providerCast = string.Format(provider, "AccountService");
            //        return (IAccountService)serviceProvider.GetService(Type.GetType(providerCast));
            //    }
            //    else
            //    {
            //        return serviceProvider.GetService<AccountService>();
            //    }
            //});
            #endregion

          
            return services;
        }

    }
}