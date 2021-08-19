using AutoMapper;
using Demo.Application.ViewModels;
using Demo.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Application.AutoMapper
{
    public class DomainToViewModelMappingProfile : Profile
    {
        /// <summary>
        /// 配置构造函数，用来创建关系映射
        /// </summary>
        public DomainToViewModelMappingProfile()
        {
            CreateMap<Student, StudentViewModel>()
                .ForMember(d => d.County, o => o.MapFrom(s => s.Address.County))
                .ForMember(d => d.Province, o => o.MapFrom(s => s.Address.Province))
                .ForMember(d => d.City, o => o.MapFrom(s => s.Address.City))
                .ForMember(d => d.Street, o => o.MapFrom(s => s.Address.Street))
                ;
        }
    }
}
