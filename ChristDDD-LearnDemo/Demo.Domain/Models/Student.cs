using Demo.Domain.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Domain.Models
{

    /// <summary>
    /// 定义领域对象 Customer
    /// </summary>
    public class Student : Entity
    {
        protected Student() { }
        public Student(Guid id, string name, string email, string phone, DateTime birthDate, Address address)
        {
            Id = id;
            Name = name;
            Email = email;
            Phone = phone;
            BirthDate = birthDate;
            Address = address;
        }
        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; private set; }
        /// <summary>
        /// 邮箱
        /// </summary>
        public string Email { get; private set; }
        /// <summary>
        /// 手机
        /// </summary>
        public string Phone { get; private set; }
        /// <summary>
        /// 生日
        /// </summary>
        public DateTime BirthDate { get; private set; }

        /// <summary>
        /// 户籍
        /// </summary>
        public Address Address { get; private set; }
    }
}
