using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace SimpleAPI.Models
{

        public class SQLiteDBContext : DbContext
        {
            public DbSet<User> Users { get; set; }
            public SQLiteDBContext(DbContextOptions<SQLiteDBContext> options) : base(options)
            {
            }
        }

}
