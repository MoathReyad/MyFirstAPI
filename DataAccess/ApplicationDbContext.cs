using Microsoft.EntityFrameworkCore;
using MoathAPI.Models;

namespace MoathAPI.DataAccess
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }

        public DbSet<Student> Students { get; set; }
    }
}
