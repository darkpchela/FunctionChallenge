using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
namespace FunctionChallenge.Models
{
    public class ChartDBContext:DbContext
    {
        public DbSet<PointDBModel> Points { get; set; }
        public DbSet<UserData> UserDatas { get; set; }
        public ChartDBContext(DbContextOptions<ChartDBContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
