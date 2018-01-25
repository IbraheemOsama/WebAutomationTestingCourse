using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Tax.Data;

namespace Tax.Repository
{
    public class UserTaxRepository : IUserTaxRepository
    {
        private readonly TaxDbContext _context;

        public UserTaxRepository(TaxDbContext context)
        {
            _context = context;
        }

        public async Task AddUserTax(UserTax userTax)
        {
            await _context.UserTaxes.AddAsync(userTax);
            await _context.SaveChangesAsync();
        }

        public Task<UserTax> GetUserTax(string userId, int year)
        {
            return _context.UserTaxes.FirstOrDefaultAsync(i => i.UserId == userId && i.Year == year);
        }

        public async Task<IEnumerable<UserTax>> GetAllUserTax(string userId)
        {
            return await _context.UserTaxes.Where(i => i.UserId == userId).ToListAsync();
        }
    }
}
