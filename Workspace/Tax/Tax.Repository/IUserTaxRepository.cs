using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tax.Data;

namespace Tax.Repository
{
    public interface IUserTaxRepository
    {
        Task AddUserTax(UserTax userTax);
        Task<UserTax> GetUserTax(string userId, int year);
        Task<IEnumerable<UserTax>> GetAllUserTax(string userId);
    }
}