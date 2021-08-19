using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Domain.Commands
{
    /// <summary>
    /// 地址
    /// </summary>
    public class StudentAddress
    {
        public string Province { get; set; }
        public string City { get; set; }
        public string County { get; set; }
        public string Street { get; set; }
    }
}
