using Auction.Dto;
using Auction.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
namespace Auction.Controllers
{
    public class ProductController
    {

        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly AuctionContext _context;
        private readonly IWebHostEnvironment _hostingEnvironment;
        public ProductController(IWebHostEnvironment webHostEnvironment, AuctionContext context)
        {
            _webHostEnvironment = webHostEnvironment;
            _context = context;
        }

        [HttpPost("test")]
        public ResponseDto test() 
        {
            try
            {
                var provider = new PhysicalFileProvider(_webHostEnvironment.WebRootPath);
                var contents = provider.GetDirectoryContents(Path.Combine("Products"));
                var objFiles = contents.OrderBy(m => m.LastModified).ToArray();
                User user = _context.User.FirstOrDefault(x => x.Id == 2);
                Category category = _context.Category.FirstOrDefault(x => x.Id == 1);
                Random rnd = new Random();
                foreach (var photo in objFiles)
                {
                    _context.Add(new Product
                    {
                        Name = photo.Name,
                        Description = $"Продваю машину {photo.Name} в отличном состоянии. Не бит, не крашен!",
                        Salesman = user,
                        Category = category,
                        PhotoUrl = "/Products/" + photo.Name,
                        StartDate = DateTime.Now,
                        FinishDate = DateTime.Now.AddMinutes(30),
                        StartPrice = rnd.Next(1000, 5000),
                        Step = 100
                    });
                }
                _context.SaveChanges();
                return new ResponseDto(0, "Success");
            }
            catch (Exception ex)
            {

                return new ResponseDto(-1, ex.Message);
            }
           
        }
        [HttpPost("api/createAuction")]
        public async Task<ResponseDto> createAuction([FromForm] Product product) 
        {
            try
            {
                Category category = _context.Category.FirstOrDefault(x=>x.Id==product.Category.Id);
                User Salesman = _context.User.Include(x=>x.Role).FirstOrDefault(x=>x.Id==product.Salesman.Id);
                product.Salesman = Salesman;
                product.Category = category;
                product.Buyer = null;
                string fileName = $"{Guid.NewGuid()}.jpg";


                string filepath = "D:/Локальный диск/Адил/УЧЁБА/Универ/4 курс/4 курс/Auction/Auction/wwwroot/Products/" + fileName + ".jpeg";
                var bytess = Convert.FromBase64String(product.PhotoUrl);
                using (var imageFile = new FileStream(filepath, FileMode.Create))
                {
                    imageFile.Write(bytess, 0, bytess.Length);
                    imageFile.Flush();
                }
                string imageUrl =  "/Products/" + fileName;
                product.PhotoUrl = filepath;
                product.Status = "New";
                _context.Product.Add(product);
                _context.SaveChanges();
                return new ResponseDto(0, "Success");
            }
            catch (Exception ex)
            {

                return new ResponseDto(-1, ex.Message);
            }
        }



        [HttpGet("api/getAuctions")]
        public ResponseDto<List<Product>> getAuctions() 
        {
            try
            {
                List<Product> auctions = _context.Product.Include(x=>x.Salesman).Include(x => x.Category).ToList();
                return new ResponseDto<List<Product>>(0, "Success", auctions);
            }
            catch (Exception ex)
            {

                return new ResponseDto<List<Product>>(-1, ex.Message, null);
            }
        }

        [HttpPost("api/createBet")]
        public ResponseDto createBet(Bet bet)
        {
            try
            {
                Product auction = _context.Product.Include(x=>x.Salesman).FirstOrDefault(x => x.Id == bet.Product.Id);
                if (auction.Salesman.Id == bet.Player.Id)
                {
                    return new ResponseDto(2, "Создатель не может участвовать в своём аукционе");
                }
                if (auction.Status != "New")
                {
                    return new ResponseDto(2, "Аукцион ещё не начался");
                }             
                List<Bet> bets = _context.Bet.Where(x => x.Product.Id == bet.Product.Id).ToList();
                if (bets.Last().Price >= bet.Price) 
                {
                    return new ResponseDto(1, "Ставка должна быть больше последней");
                }
                bet.Player = _context.User.FirstOrDefault(x => x.Id == bet.Player.Id);
                bet.Product = auction;
                _context.Bet.Add(bet);
                auction.FinishPrice = bet.Price;
                _context.SaveChanges();
                return new ResponseDto(0, "Success");
            }
            catch (Exception ex)
            {

                return new ResponseDto(-1, ex.Message);
            }
        }

        [HttpPost("api/getBets")]
        public ResponseDto<List<Bet>> getBets(int auctionId) 
        {
            try
            {
                List<Bet> bets = _context.Bet.Where(x=>x.Product.Id==auctionId).ToList();
                return new ResponseDto<List<Bet>>(-1, "Success", bets);
            }
            catch (Exception ex)
            {

                return new ResponseDto<List<Bet>>(-1, ex.Message, null);
            }
        }

    }
}
