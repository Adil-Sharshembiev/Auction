using Auction.Dto;
using Auction.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Auction.Controllers
{
    public class UserController : ControllerBase
    {
        private readonly AuctionContext _context;

        public UserController(AuctionContext context)
        {
            _context = context;
        }

        [HttpPost("registration")]
        public async Task<ResponseDto> registration([FromForm] User user) 
        {

            try
            {
                User checkUser = _context.User.FirstOrDefault(x => x.Email == user.Email);
                if (checkUser != null) 
                {
                    return new ResponseDto(1, "Клиент уже существует");
                }
                user.Role = _context.Role.FirstOrDefault(x => x.Name == "user");
                _context.User.Add(user);
                _context.SaveChanges();
                return new ResponseDto(0, "Success"); 
            }
            catch (Exception ex) { return new ResponseDto(-1, ex.Message); }
            return null;
        }

        [HttpPost("auth")]
        public async Task<ResponseDto> auth([FromForm] User user)
        {

            try
            {
                User checkUser = _context.User.FirstOrDefault(x => x.Email == user.Email);
                if (checkUser == null)
                {
                    return new ResponseDto(1, "Неверный логин");
                }
                if (checkUser.Password != user.Password) 
                {
                    return new ResponseDto(2, "Неверный пароль");
                }
                return new ResponseDto(0, "Success");
            }
            catch (Exception ex) { return new ResponseDto(-1, ex.Message); }
            return null;
        }

    }
}
