using Auction.Enum;
using System.ComponentModel.DataAnnotations;

namespace Auction.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public User Salesman { get; set; }
        public User? Buyer { get; set; }
        public Category Category { get; set; }
        public string? PhotoUrl { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime FinishDate { get; set; }
        public double StartPrice { get; set; }
        public double Step { get; set; }
        public double? FinishPrice { get; set; }
        public string Status { get; set; } = StatusEnum.New.ToString();
    }
}
