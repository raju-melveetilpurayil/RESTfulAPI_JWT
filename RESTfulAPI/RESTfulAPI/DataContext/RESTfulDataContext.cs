using Microsoft.EntityFrameworkCore;
using RESTfulAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RESTfulAPI.DataContext
{
    public class RESTfulDataContext:DbContext
    {
        public RESTfulDataContext(DbContextOptions<RESTfulDataContext> options):base(options)
        {

        }
        public DbSet<User> Users { get; set; }
    }
}
