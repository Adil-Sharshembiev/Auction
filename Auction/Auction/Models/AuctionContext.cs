using Microsoft.EntityFrameworkCore;
using static System.Net.Mime.MediaTypeNames;
using System.Net;

namespace Auction.Models
{
    public class AuctionContext: DbContext
    {
        public AuctionContext(DbContextOptions<AuctionContext> options)
       : base(options)
        {

        }
        public DbSet<Role> Role { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<Bet> Bet { get; set; }
    }
}
