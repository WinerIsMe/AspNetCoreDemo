using Demo.Application.Interfaces;
using Demo.Domain.Models;
using Demo.Application.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.Service.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StudentController: ControllerBase
    {
        //还是构造函数注入
        private readonly IStudentAppService _studentAppService;

        public StudentController(IStudentAppService studentAppService)
        {
            _studentAppService = studentAppService;
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
        public ActionResult Create(StudentViewModel studentViewModel)
        {
            try
            {
                // 视图模型验证
                if (!ModelState.IsValid)
                    return BadRequest(studentViewModel);
                // 执行添加方法
                _studentAppService.Register(studentViewModel);

                return Ok(studentViewModel);
            }
            catch (Exception e)
            {
                return NotFound(e);
            }
        }
    }
}
