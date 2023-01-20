
using backend_tasks.Models;
using Microsoft.EntityFrameworkCore;

namespace backend_tasks.Context
{
    public class AplicationDbContext : DbContext
    {

        public AplicationDbContext(DbContextOptions<AplicationDbContext> options): base(options){

        }

        public DbSet<Tasks> Task { get; set; }
    }
}
