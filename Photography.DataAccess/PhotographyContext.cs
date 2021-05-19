using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Photography.ApplicationLogic.Models;

namespace Photography.DataAccess
{
    public class PhotographyContext : IdentityDbContext<Account>
    {
        public PhotographyContext(DbContextOptions<PhotographyContext> options)
            : base(options)
        { }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Photo> Photos { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<ContactForm> ContactForms { get; set; }
    }
}
