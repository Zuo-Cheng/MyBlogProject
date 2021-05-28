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
    public class TypeInfoService:BaseService<TypeInfo>,ITypeInfoService
    {


        private ITypeInfoRepository _typeInfoRepository;

        public TypeInfoService(ITypeInfoRepository typeInfoRepository)
        {
            base.iBaseRepository = typeInfoRepository;
            _typeInfoRepository = typeInfoRepository;
        }
    }
}
