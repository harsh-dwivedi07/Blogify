using BlogsAssignment.Data;
using BlogsAssignment.Models;
using BlogsAssignment.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlogsAssignment.Repository.Implementation
{
    public class Accounts : IAccounts
    {
        private readonly ApplicationDbContext _context;
        public Accounts(ApplicationDbContext context)
        {
            _context=context;
        }
        public bool Add(RegisterModel obj)
        {
            try
            {              
                _context.UserDetails.Add(obj);
                _context.SaveChanges();
                return true;
            }
            catch { return false; }
            
        }
        public List<String> GetAllUserNames()
        {
            var usernames=_context.UserDetails.Select(x=>x.UserName).ToList();

            return usernames;
        }
        public RegisterModel GetUserByUserName(String userName)
        {
            return _context.UserDetails.FirstOrDefault(x => x.UserName == userName);
        }

    }
}