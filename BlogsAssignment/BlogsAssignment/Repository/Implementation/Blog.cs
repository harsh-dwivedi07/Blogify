using BlogsAssignment.Data;
using BlogsAssignment.Models;
using BlogsAssignment.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;

namespace BlogsAssignment.Repository.Implementation
{
    public class Blog : IBlog
    {
        private readonly ApplicationDbContext _context;
        public Blog(ApplicationDbContext context)
        {
            _context = context;
        }
        public Boolean Add(Posts post)
        {           
            _context.Post.Add(post);  
            _context.SaveChanges();
            return true;
        }

        public List<Posts> GetAllPublishedBlogs()
        {
           var postsList=_context.Post
                                 .Where(x=>x.Status== BlogsAssignment.Utility.Constants.Published)
                                 .ToList();
            return postsList;
        }

        public Posts GetBlogById(Guid? id)
        {
            if(id== null)
                return null;
            return _context.Post.FirstOrDefault(x => x.Id == id);
        }
        public List<Posts> GetAllDraftBlogsForUser(Guid userId)
        {
            var draftedBlogs = _context.Post
                                        .Where(x=>x.AuthorId==userId 
                                            && x.Status== BlogsAssignment.Utility.Constants.Draft)
                                        .ToList();
            return draftedBlogs;
        }
    }

}