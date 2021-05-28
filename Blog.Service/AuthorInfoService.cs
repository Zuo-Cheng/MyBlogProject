using MyBlog.IRepository;
using MyBlog.IService;
using MyBlog.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Service
{
    public class AuthorInfoService:BaseService<AuthorInfo>,IAuthorInfoService
    {
        private readonly IAuthorInfoRepository _authorInfoRepository;
        public AuthorInfoService(IAuthorInfoRepository authorInfoRepository)
        {
            base.iBaseRepository = authorInfoRepository;
            _authorInfoRepository = authorInfoRepository;
        }
    }
}
