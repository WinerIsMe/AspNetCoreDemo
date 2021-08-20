using Demo.Application.Interfaces;
using Demo.Domain.Models;
using Demo.Application.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Demo.Domain.Core.Notifications;
using MediatR;

namespace Demo.Service.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StudentController: ControllerBase
    {
        //还是构造函数注入
        private readonly IStudentAppService _studentAppService;
        // 缓存注入，为了收录信息（错误方法，以后会用通知，通过领域事件来替换）
        // private IMemoryCache _cache;
        // 领域通知处理程序
        private readonly DomainNotificationHandler _notifications;

        public StudentController(IStudentAppService studentAppService, INotificationHandler<DomainNotification> notifications)
        {
            _studentAppService = studentAppService;
            _notifications = (DomainNotificationHandler)notifications;
        }

        // GET: Student
        [HttpGet]
        public IEnumerable<StudentViewModel> Get()
        {
            return _studentAppService.GetAll();
        }

        // POST: Student/Create
        // 方法
        [HttpPost]
        [Route("[action]")]
        public ActionResult Create(StudentViewModel studentViewModel)
        {
            try
            {
                // 视图模型验证
                if (!ModelState.IsValid)
                    return BadRequest(studentViewModel);
                // 执行添加方法
                _studentAppService.Register(studentViewModel);

                if (_notifications.HasNotifications())
                {
                    return BadRequest(_notifications.GetNotifications());
                }

                return Ok(studentViewModel);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        // POST: Student/Create
        // 方法
        [HttpPost]
        [Route("[action]")]
        public ActionResult Update(StudentViewModel studentViewModel)
        {
            try
            {
                // 视图模型验证
                if (!ModelState.IsValid)
                    return BadRequest(studentViewModel);
                // 执行添加方法
                _studentAppService.Update(studentViewModel);

                return Ok(studentViewModel);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
    }
}
