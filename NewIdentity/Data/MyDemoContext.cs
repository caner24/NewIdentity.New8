using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using NewIdentity.Entity;

namespace NewIdentity.Data
{
    public class MyDemoContext : IdentityDbContext<User>
    {
        public MyDemoContext(DbContextOptions<MyDemoContext> options) : base(options)
        {

        }

        public DbSet<User> User { get; set; }
    }
}
