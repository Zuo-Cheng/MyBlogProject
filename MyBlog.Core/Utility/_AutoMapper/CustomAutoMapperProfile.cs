using AutoMapper;
using MyBlog.DTO;
using MyBlog.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.Core.Utility._AutoMapper
{
    public class CustomAutoMapperProfile:Profile
    {
        public CustomAutoMapperProfile()
        {
            base.CreateMap<AuthorInfo, AuthorInfoDTO>();
            base.CreateMap<BlogNews, BlogNewsDTO>()
                .ForMember(t=>t.AuthorName,source=> source.MapFrom(t=>t.AuthorInfo.Name))
                .ForMember(t=>t.TypeName,source=>source.MapFrom(t=>t.TypeInfo.TypeName));
        }
    }
}
