using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Md_exercise.Log4net
{
    public class HeroesLogsDbContext : DbContext
    {
        public HeroesLogsDbContext(DbContextOptions<HeroesLogsDbContext> options) : base(options)
        {
        }
        public DbSet<Log> Logs { get; set; }
    }
}
