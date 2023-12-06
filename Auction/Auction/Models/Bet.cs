using System.ComponentModel.DataAnnotations;

namespace Auction.Models
{
    public class Bet
    {
        [Key]
        public int Id { get; set; }
        public User Player { get; set; }
        public Product Product { get; set; }
        public DateTime Date { get; set; }
        public double Price { get; set; }
    }
}
