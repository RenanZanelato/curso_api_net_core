using Api.Data.Repository;
using Api.Domain.Entities;
using Api.Domain.Repository;
using Data.Context;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Api.Data.Implementation
{
    public class UserImplementation:BaseRepository<UserEntity>,IUserRepository
    {
        private DbSet<UserEntity> _dataset;

        public UserImplementation(MyContext context) : base(context)
        {
            _dataset = context.Set<UserEntity>();

        }

        public async Task<UserEntity> FindByLogin(string email)
        {
            return await _dataset.FirstOrDefaultAsync(u => u.Email.Equals(email));
        }
    }
}
