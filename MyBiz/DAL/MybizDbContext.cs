using Microsoft.EntityFrameworkCore;
using MyBiz.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyBiz.DAL
{
    public class MybizDbContext:DbContext
    {
        public MybizDbContext(DbContextOptions<MybizDbContext> options):base(options)
        {

        }

        public DbSet<Slider> Sliders { get; set; }
    }
}
