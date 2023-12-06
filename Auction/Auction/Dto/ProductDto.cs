using Auction.Enum;
using Auction.Models;

namespace Auction.Dto
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public UserDto Salesman { get; set; }
        public UserDto? Buyer { get; set; }
        public Category Category { get; set; }
        public string? PhotoUrl { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime FinishDate { get; set; }
        public double StartPrice { get; set; }
        public double Step { get; set; }
        public double? FinishPrice { get; set; }
        public string Status { get; set; }
    }
}
