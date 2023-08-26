using BlogsAssignment.Data;
using BlogsAssignment.Models;
using BlogsAssignment.Repository.Implementation;
using BlogsAssignment.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects.DataClasses;
using System.Linq;
using System.Web.Mvc;

namespace BlogsAssignment.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        IAccounts accounts = new Accounts(new ApplicationDbContext());
        IBlog blog = new Blog(new ApplicationDbContext());
        public ActionResult Login()
        {
            return View();
        }
        public ActionResult Register()
        {          
            return View();
        }
        [HttpPost]
        public ActionResult RegisterPost(RegisterModel obj)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (obj != null)
                    {
                        var userNames = accounts.GetAllUserNames();
                        if (userNames.Contains(obj.UserName))
                        {
                            TempData["ErrorMessage"] = "Username is already taken. Please choose a different one.";
                            return RedirectToAction("Register");
                        }
                        obj.Id = Guid.NewGuid();
                        accounts.Add(obj);
                    }
                    return Redirect("Login");
                }
                return Redirect("Register");
            }
            catch (Exception ex)
            {
                return RedirectToAction("Register", "Account"); 
            }
        }
        [HttpPost]
        public ActionResult LoginPost(LoginModel obj)
        {
            try
            {
                if (obj != null)
                {
                    RegisterModel entity = accounts.GetUserByUserName(obj.UserName);
                    if (entity == null)
                    {
                        TempData["ErrorMessage"] = "Invalid UserName!!!";
                        return Redirect("Login");
                    }
                    if (entity.Password != obj.Password)
                    {
                        TempData["ErrorMessage"] = "Invalid Password for user!!!";
                        return Redirect("Login");
                    }
                    CustomUser user = new CustomUser
                    {
                        UserId = entity.Id,
                        Name = entity.FirstName + " " + entity.LastName,
                    };
                    Session["CurrentUser"] = user;
                }

                List<BlogsProxy> allPublishedBlogs = blog.GetAllPublishedBlogs().Select(p => new BlogsProxy
                {
                    Id = p.Id,
                    Title = p.Title,
                    Author = p.Author,
                    PublishedDate = DateTime.Parse(p.PublishedOn.ToString()).ToString("d-MMM-yy")
                }).ToList();
                return View(allPublishedBlogs);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Register", "Account");
            }
        }
        public ActionResult toHome()
        {
            try {
                List<BlogsProxy> allPublishedBlogs = blog.GetAllPublishedBlogs().Select(p => new BlogsProxy
                {
                    Id = p.Id,
                    Title = p.Title,
                    Author = p.Author,
                    PublishedDate = DateTime.Parse(p.PublishedOn.ToString()).ToString("d-MMM-yy")
                }).ToList();
                return View("LoginPost", allPublishedBlogs);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Register", "Account");
            }
        }
        }
}