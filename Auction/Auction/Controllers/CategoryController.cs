using Auction.Dto;
using Auction.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Auction.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly AuctionContext _context;

        public CategoryController(AuctionContext context)
        {
            _context = context;
        }
        [HttpGet("api/getCategories")]
        public ResponseDto<List<Category>> GetCategories() 
        {
            try
            {
                List<Category> categories = _context.Category.ToList();
                return new ResponseDto<List<Category>>(0, "Success", categories);

            }
            catch (Exception ex)
            {
                return new ResponseDto<List<Category>>(-1, ex.Message, null);
            }
        }
        [HttpPost("api/createCategory")]
        public ResponseDto createCategory(Category category) 
        {
            try
            {
                _context.Add(category);
                _context.SaveChanges();
                return new ResponseDto(0, "Success"); ;
            }
            catch (Exception ex) { return new ResponseDto(-1, ex.Message); }
        }
        [HttpPost("api/updateCategory")]
        public ResponseDto updateCategory(Category categoryUpdate)
        {
            try
            {
                Category category = _context.Category.FirstOrDefault(x => x.Id == categoryUpdate.Id);
                category.Name = categoryUpdate.Name;
                _context.SaveChanges();
                return new ResponseDto(0, "Success"); ;
            }
            catch (Exception ex) { return new ResponseDto(-1, ex.Message); }
        }
        [HttpPost("api/deleteCategory")]
        public ResponseDto deleteCategory(int categoryId)
        {
            try
            {
                Category category = _context.Category.FirstOrDefault(x => x.Id == categoryId);
                _context.Remove(category);
                _context.SaveChanges();
                return new ResponseDto(0, "Success"); ;
            }
            catch (Exception ex) { return new ResponseDto(-1, ex.Message); }
        }
    }
}
