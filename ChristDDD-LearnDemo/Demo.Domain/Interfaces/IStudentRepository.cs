using Demo.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Domain.Interfaces
{
    /// <summary>
    /// ICustomerRepository 接口
    /// 注意，这里我们用到的业务对象，是领域对象
    /// </summary>
    public interface IStudentRepository : IRepository<Student>
    {
        //一些Customer独有的接口        
        Student GetByEmail(string email);
    }
}
