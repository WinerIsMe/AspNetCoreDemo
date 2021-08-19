using Demo.Domain.Interfaces;
using Demo.Domain.Models;
using Demo.Infrastruct.Data.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Demo.Infrastruct.Data.Repository
{
    /// <summary>
    /// Customer仓储，操作对象还是领域对象
    /// </summary>
    public class StudentRepository : Repository<Student>, IStudentRepository
    {
        public StudentRepository(StudyContext context)
          : base(context)
        {

        }
        //对特例接口进行实现
        public Student GetByEmail(string email)
        {
            return DbSet.AsNoTracking().FirstOrDefault(c => c.Email == email);
        }
    }
}
