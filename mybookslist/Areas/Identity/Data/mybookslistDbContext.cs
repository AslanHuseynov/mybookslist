using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using mybookslist.Areas.Identity.Data;
using mybookslist.Models;

namespace mybookslist.Data
{
    public class mybookslistDbContext : IdentityDbContext<mybookslistUser>
    {
        public mybookslistDbContext(DbContextOptions<mybookslistDbContext> options)
            : base(options)
        {
        }

        public DbSet<BookInfo> BookInfo { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }
    }
}
