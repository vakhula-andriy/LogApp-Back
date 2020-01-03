using Microsoft.EntityFrameworkCore;
using LogApp.Core.Models;

namespace LogApp.DAL
{
    public class LogAppContext : DbContext
    {
        public DbSet<Record> Records { get; set; }
        public LogAppContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {
        }
    }
}
