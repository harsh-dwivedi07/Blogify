using BlogsAssignment.Data;
using BlogsAssignment.Models;
using BlogsAssignment.Repository.Implementation;
using BlogsAssignment.Repository.IRepository;
using System;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;

namespace BlogsAssignment.Controllers
{
    public class BlogsController : Controller
    {
        // GET: Blogs
        IBlog blog = new Blog(new ApplicationDbContext());
        public ActionResult Index()
        {
            try
            {
                var user = Session["CurrentUser"] as BlogsAssignment.Models.CustomUser;
                var result = blog
                             .GetAllDraftBlogsForUser(user.UserId)
                             .Select(p => new BlogsProxy
                {
                    Id = p.Id,
                    Title = p.Title,
                    Author = p.Author
                }).ToList();
                return View(result);
            }
            catch (Exception ex)
            {
                return Redirect("Account/Register");
            }
        }
        public ActionResult Upsert(Guid? id = null)
        {
            try { 
            if (id != null)
            {
                var blogData = blog.GetBlogById(id);
                if (blogData != null)
                {
                    ViewBag.FormTitle = "Edit Post";
                    ViewBag.ImageUrl = blogData.ImageUrl;
                    ViewBag.ContentTitle = blogData.Title;
                    ViewBag.Content = blogData.Content;
                }
                var model = new Posts
                {
                    Content = ViewBag.Content
                };

                return View(model);
            }
            ViewBag.FormTitle = "Add Post";
            return View();
        }
        catch (Exception ex)
            {
                return Redirect("Account/Register");
    }
}
        [HttpPost]
        public ActionResult Create(Posts post, HttpPostedFileBase image, string action)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (action == "published")
                    {
                        post.Status = BlogsAssignment.Utility.Constants.Published;
                        if(post.Content == null)
                        {
                            TempData["ErrorMessage"] = "Add Content, if you want to publish.";
                            return View("Upsert");
                        }
                    }

                    else
                        post.Status = BlogsAssignment.Utility.Constants.Draft;
                    if (image != null && image.ContentLength > 0)
                    {
                        string imageFileName = Path.GetFileName(image.FileName);
                        string contentFolderPath = HostingEnvironment.MapPath("~/Content"); // Get the physical path of the "Content" folder
                        string imagesFolderPath = Path.Combine(contentFolderPath, "Images"); // Combine with the "Images" folder
                        string imagePath = Path.Combine(imagesFolderPath, imageFileName);
                        if (!Directory.Exists(imagesFolderPath))
                        {
                            Directory.CreateDirectory(imagesFolderPath);
                        }

                        image.SaveAs(imagePath);
                        post.ImageUrl = imagePath;
                        //post.ImageUrl = "~/Content/Images" + imageFileName;
                        byte[] imageBytes;
                        using (BinaryReader reader = new BinaryReader(image.InputStream))
                        {
                            imageBytes = reader.ReadBytes(image.ContentLength);
                        }
                        post.ImageData = imageBytes;
                    }
                    post.Id = Guid.NewGuid();
                    post.Created = DateTime.Now;
                    post.PublishedOn = DateTime.Now;
                    var user = Session["CurrentUser"] as BlogsAssignment.Models.CustomUser;
                    if (user != null)
                    {
                        post.Author = user.Name;
                        post.AuthorId = user.UserId;
                    }
                    blog.Add(post);

                    return action == "published" 
                                      ? RedirectToAction("toHome", "Account") 
                                      : RedirectToAction("Index", "Blogs"); 
                }

                return View(post);
            }
            catch (Exception ex)
            {
                return Redirect("Account/Register");
            }
        }

        [HttpGet]
        public ActionResult Get(Guid id)
        {
            try
            {
                if (id != null)
                {
                    Posts post = blog.GetBlogById(id);
                    if (post != null)
                    {
                        return View(post);
                    }
                }
                return View();
            }
            catch (Exception ex)
            {
                return Redirect("Account/Register");
            }
        }
    }
}