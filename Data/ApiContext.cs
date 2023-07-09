using Microsoft.EntityFrameworkCore;
using PasswordManager.Models;

namespace PasswordManager.Data
{
    public class ApiContext : DbContext
    {
        public DbSet<Password> Passwords { get; set; }
        public ApiContext(DbContextOptions<ApiContext> options) : base(options) 
        {
            
        }
    }
}
