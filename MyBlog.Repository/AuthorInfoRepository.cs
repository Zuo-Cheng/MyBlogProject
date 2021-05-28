using MyBlog.IRepository;
using MyBlog.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.Repository
{
    public class AuthorInfoRepository:BaseRepository<AuthorInfo>,IAuthorInfoRepository
    {

    }
}
