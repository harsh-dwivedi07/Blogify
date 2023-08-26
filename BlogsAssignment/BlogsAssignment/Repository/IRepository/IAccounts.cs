using BlogsAssignment.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogsAssignment.Repository.IRepository
{
    public interface IAccounts
    {
         Boolean Add(RegisterModel obj);
        List<String> GetAllUserNames();
        RegisterModel GetUserByUserName(String userName);
    }
}
