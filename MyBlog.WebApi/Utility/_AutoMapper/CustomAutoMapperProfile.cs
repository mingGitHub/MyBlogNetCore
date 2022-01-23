using AutoMapper;
using MyBlog.Model;
using MyBlog.Model.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyBlog.WebApi.Utility._AutoMapper
{
    public class CustomAutoMapperProfile : Profile
    {
        public CustomAutoMapperProfile()
        {
            base.CreateMap<WriterInfo, WriterDTO>();
            base.CreateMap<BlogNews,BlogNewsDTO>()
                .ForMember(dest=>dest.TypeName,source=>source.MapFrom(src=>src.TypeInfo.Name))
                .ForMember(dest => dest.WriterName, source => source.MapFrom(src => src.WriteInfo.Name));
        }
    }
}
