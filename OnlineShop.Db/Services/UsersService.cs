using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Db.Interfaces;
using OnlineShop.Db.Models;
using Microsoft.AspNetCore.Identity;
using OnlineShop.Db;
using OnlineShop.Db.Models;
using OnlineShopProject.Models;


namespace OnlineShop.Db.Services
{
    public class UsersService
    {
        private readonly IdentityContext _context;
        
        public UsersService(IdentityContext context)
        {
            _context = context;
        }
        
    }
}
