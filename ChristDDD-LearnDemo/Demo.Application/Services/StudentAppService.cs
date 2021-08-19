using AutoMapper;
using Demo.Application.Interfaces;
using Demo.Application.ViewModels;
using Demo.Domain.Commands;
using Demo.Domain.Core.Bus;
using Demo.Domain.Interfaces;
using Demo.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Application.Services
{
    namespace Christ3D.Application.Services
    {
        /// <summary>
        /// StudentAppService 服务接口实现类,继承 服务接口
        /// 通过 DTO 实现视图模型和领域模型的关系处理
        /// 作为调度者，协调领域层和基础层，
        /// 这里只是做一个面向用户用例的服务接口,不包含业务规则或者知识
        /// </summary>
        public class StudentAppService : IStudentAppService
        {
            //注意这里是要IoC依赖注入的，还没有实现
            private readonly IStudentRepository _StudentRepository;
            //用来进行DTO
            private readonly IMapper _mapper;
            //中介者 总线
            private readonly IMediatorHandler Bus;

            public StudentAppService(
                IStudentRepository StudentRepository,
                IMediatorHandler bus,
                IMapper mapper
                )
            {
                _StudentRepository = StudentRepository;
                _mapper = mapper;
                Bus = bus;
            }

            public IEnumerable<StudentViewModel> GetAll()
            {
                //第一种写法 Map      
                var aa = _StudentRepository.GetAll();
                var bb = _mapper.Map<IEnumerable<StudentViewModel>>(aa);
                return _mapper.Map<IEnumerable<StudentViewModel>>(_StudentRepository.GetAll());
                //第二种写法 ProjectTo           
                //return (_StudentRepository.GetAll()).ProjectTo<StudentViewModel>(_mapper.ConfigurationProvider);
            }

            public StudentViewModel GetById(Guid id)
            {
                return _mapper.Map<StudentViewModel>(_StudentRepository.GetById(id));
            }

            public void Register(StudentViewModel StudentViewModel)
            {
                //判断是否为空等等 还没有实现
                //_StudentRepository.Add(_mapper.Map<Student>(StudentViewModel));
                //_StudentRepository.SaveChanges();

                // 使用中介者模式
                var registerCommand = _mapper.Map<RegisterStudentCommand>(StudentViewModel);
                Bus.SendCommand(registerCommand);
            }

            public void Update(StudentViewModel StudentViewModel)
            {
                _StudentRepository.Update(_mapper.Map<Student>(StudentViewModel));

            }

            public void Remove(Guid id)
            {
                _StudentRepository.Remove(id);

            }

            public void Dispose()
            {
                GC.SuppressFinalize(this);
            }
        }
    }
}
