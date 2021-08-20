using Demo.Domain.Commands;
using Demo.Domain.Core.Bus;
using Demo.Domain.Core.Notifications;
using Demo.Domain.Events;
using Demo.Domain.Interfaces;
using Demo.Domain.Models;
using MediatR;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Demo.Domain.CommandHandlers
{
    /// <summary>
    /// Student命令处理程序
    /// 用来处理该Student下的所有命令
    /// 注意必须要继承接口IRequestHandler<,>，这样才能实现各个命令的Handle方法
    /// </summary>
    public class StudentCommandHandler : CommandHandler,
        IRequestHandler<RegisterStudentCommand, bool>,
        IRequestHandler<UpdateStudentCommand, bool>,
        IRequestHandler<RemoveStudentCommand, bool>
    {
        // 注入仓储接口
        private readonly IStudentRepository _studentRepository;
        // 注入总线
        private readonly IMediatorHandler Bus;
        private IMemoryCache Cache;

        /// <summary>
        /// 构造函数注入
        /// </summary>
        /// <param name="studentRepository"></param>
        /// <param name="uow"></param>
        /// <param name="bus"></param>
        /// <param name="cache"></param>
        public StudentCommandHandler(IStudentRepository studentRepository,
                                      IUnitOfWork uow,
                                      IMediatorHandler bus,
                                      IMemoryCache cache
                                      ) : base(uow, bus, cache)
        {
            _studentRepository = studentRepository;
            Bus = bus;
            Cache = cache;
        }

        // RegisterStudentCommand命令的处理程序
        // 整个命令处理程序的核心都在这里
        // 不仅包括命令验证的收集，持久化，还有领域事件和通知的添加
        public Task<bool> Handle(RegisterStudentCommand message, CancellationToken cancellationToken)
        {
            // 命令验证
            if (!message.IsValid())
            {
                // 错误信息收集
                NotifyValidationErrors(message);
                return Task.FromResult(true);
            }

            // 实例化领域模型，这里才真正的用到了领域模型
            // 注意这里是通过构造函数方法实现
            var address = new Address(message.Province, message.City, message.County, message.Street);
            var customer = new Student(Guid.NewGuid(), message.Name, message.Email, message.Phone, message.BirthDate, address);

            // 判断邮箱是否存在
            // 这些业务逻辑，当然要在领域层中（领域命令处理程序中）进行处理
            if (_studentRepository.GetByEmail(customer.Email) != null)
            {
                //引发错误事件
                Bus.RaiseEvent(new DomainNotification("", "该邮箱已经被使用！"));
                return Task.FromResult(true);
            }

            // 持久化
            _studentRepository.Add(customer);

            // 统一提交
            if (Commit())
            {
                // 提交成功后，这里需要发布领域事件
                // 比如欢迎用户注册邮件呀，短信呀等
                Bus.RaiseEvent(new StudentRegisteredEvent(customer));
            }

            return Task.FromResult(true);

        }

        // 同上，UpdateStudentCommand 的处理方法
        public Task<bool> Handle(UpdateStudentCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid())
            {
                NotifyValidationErrors(message);
                return Task.FromResult(false);

            }


            var address = new Address(message.Province, message.City,
            message.County, message.Street);
            var student = new Student(message.Id, message.Name, message.Email, message.Phone, message.BirthDate, address);
            var existingStudent = _studentRepository.GetByEmail(student.Email);

            if (existingStudent != null && existingStudent.Id != student.Id)
            {
                if (!existingStudent.Equals(student))
                {
                    //引发错误事件
                    Bus.RaiseEvent(new DomainNotification("", "该邮箱已经被使用！"));
                    return Task.FromResult(true);
                }
            }

            _studentRepository.Update(student);

            if (Commit())
            {
                // 提交成功后，这里需要发布领域事件
                // 比如欢迎用户注册邮件呀，短信呀等
                Bus.RaiseEvent(new StudentUpdatedEvent(student));
            }

            return Task.FromResult(true);
        }

        // 同上，RemoveStudentCommand 的处理方法
        public Task<bool> Handle(RemoveStudentCommand message, CancellationToken cancellationToken)
        {
            return Task.FromResult(true);
        }

        // 手动回收
        public void Dispose()
        {
            _studentRepository.Dispose();
        }
    }
}
