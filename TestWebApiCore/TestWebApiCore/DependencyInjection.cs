using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using System;
using TestWebApiCore.BLL.IServices;
using TestWebApiCore.BLL.Services;
using TestWebApiCore.DAL.Interfaces;
using TestWebApiCore.DAL.Repository;

namespace TestWebApiCore
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDependencyInjection(this IServiceCollection services)
        {
            services.AddScoped<INoteService, NoteService>();

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddScoped<IAppRepository, AppRepository>();

            return services;
        }
    }
}
