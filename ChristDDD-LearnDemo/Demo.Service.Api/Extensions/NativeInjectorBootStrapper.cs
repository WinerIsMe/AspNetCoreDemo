using Demo.Application.Interfaces;
using Demo.Application.Services.Christ3D.Application.Services;
using Demo.Domain.Core.Bus;
using Demo.Domain.Interfaces;
using Demo.Infrastruct.Data.Bus;
using Demo.Infrastruct.Data.Context;
using Demo.Infrastruct.Data.Repository;
using Demo.Infrastruct.Data.UoW;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.Service.Api.Extensions
{
    public class NativeInjectorBootStrapper
    {
        public static void RegisterServices(IServiceCollection services)
        {


            // 注入 应用层Application
            services.AddScoped<IStudentAppService, StudentAppService>();

            // 命令总线Domain Bus (Mediator)
            services.AddScoped<IMediatorHandler, InMemoryBus>();


            // 注入 基础设施层 - 数据层
            services.AddScoped<IStudentRepository, StudentRepository>();
            services.AddScoped<StudyContext>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();



        }
    }
}
