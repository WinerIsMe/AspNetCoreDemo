using Demo.Application.Interfaces;
using Demo.Application.Services;
using Demo.Application.Services.Christ3D.Application.Services;
using Demo.Domain.CommandHandlers;
using Demo.Domain.Commands;
using Demo.Domain.Core.Bus;
using Demo.Domain.Core.Events;
using Demo.Domain.Core.Notifications;
using Demo.Domain.EventHandlers;
using Demo.Domain.Events;
using Demo.Domain.Interfaces;
using Demo.Infrastruct.Data.Bus;
using Demo.Infrastruct.Data.Context;
using Demo.Infrastruct.Data.Repository;
using Demo.Infrastruct.Data.Repository.EventSourcing;
using Demo.Infrastruct.Data.UoW;
using MediatR;
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
            // Domain - Commands
            services.AddScoped<IRequestHandler<RegisterStudentCommand, bool>, StudentCommandHandler>();
            services.AddScoped<IRequestHandler<UpdateStudentCommand, bool>, StudentCommandHandler>();
            services.AddScoped<IRequestHandler<RemoveStudentCommand, bool>, StudentCommandHandler>();
            // Domain - Events
            // 将事件模型和事件处理程序匹配注入
            services.AddScoped<INotificationHandler<StudentRegisteredEvent>, StudentEventHandler>();
            services.AddScoped<INotificationHandler<StudentUpdatedEvent>, StudentEventHandler>();
            services.AddScoped<INotificationHandler<StudentRemovedEvent>, StudentEventHandler>();
            // 将事件模型和事件处理程序匹配注入
            services.AddScoped<INotificationHandler<DomainNotification>, DomainNotificationHandler>();
            // 领域层 - Memory
            services.AddSingleton<IMemoryCache>(factory =>
            {
                var cache = new MemoryCache(new MemoryCacheOptions());
                return cache;
            });

            // 注入 基础设施层 - 事件溯源
            services.AddScoped<IEventStoreRepository, EventStoreSQLRepository>();
            services.AddScoped<IEventStoreService, SqlEventStoreService>();
            services.AddScoped<EventStoreSQLContext>();


            // 注入 基础设施层 - 数据层
            services.AddScoped<IStudentRepository, StudentRepository>();
            services.AddScoped<StudyContext>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();



        }
    }
}
