using BlogsAssignment.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;


namespace BlogsAssignment.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext() : base("DefaultConnection")
        {
            // Your constructor logic here, if needed
        }

        public DbSet<RegisterModel> UserDetails { get; set; }
        public DbSet<Posts> Post { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // Your model configuration here, if needed
        }
    }
}