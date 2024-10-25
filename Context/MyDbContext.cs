using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using training.Models;

namespace training.Context
{
    public class MyDbContext : IdentityDbContext<UserModel>
    {
        public MyDbContext(DbContextOptions<MyDbContext>options): base(options) { }
        public DbSet<UserModel> users { get; set; }
        public DbSet<PostModel> posts { get; set; }
    }
}
