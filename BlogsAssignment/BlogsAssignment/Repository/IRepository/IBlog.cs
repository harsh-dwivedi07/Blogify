using BlogsAssignment.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace BlogsAssignment.Repository.IRepository
{
    public interface IBlog
    {
        Boolean Add(Posts post);
        List<Posts> GetAllPublishedBlogs();
        Posts GetBlogById(Guid? id);
        List<Posts> GetAllDraftBlogsForUser(Guid userId);
    }
}
