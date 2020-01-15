using Api.Domain.Entities;
using Api.Domain.Interfaces;
using Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Api.Data.Repository
{
    public class BaseRepository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly MyContext _context;
        private DbSet<T> _dataset;
        public BaseRepository(MyContext context)
        {
            _context = context;
            _dataset = context.Set<T>();
        }
        public async Task<bool> DeleteAsync(Guid id)
        {
            try
            {
                var result = await _dataset.SingleOrDefaultAsync(p => p.Id.Equals(id));
                if (result == null)
                {
                    throw new Exception("Any data to delete");
                }
                _dataset.Remove(result);

                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw e;
            }
            return true;
        }

        public async Task<T> InsertAsync(T item)
        {
            try
            {
                if (item.Id == Guid.Empty)
                {
                    item.Id = Guid.NewGuid();
                }
                item.CreatAt = DateTime.UtcNow;
                _dataset.Add(item);

                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw e;
            }
            return item;
        }

        public async Task<T> SelectAsync(Guid id)
        {
            try
            {
               var result = await _dataset.SingleOrDefaultAsync(p => p.Id.Equals(id));
                return result;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<IEnumerable<T>> SelectAsync()
        {
            try
            {
                return await _dataset.ToListAsync();
            }
            catch (Exception e)
            {
                throw e;
            }

        }

        public async Task<bool> ExistAsync(Guid id)
        {
            return await _dataset.AnyAsync(p => p.Id.Equals(id));
        }

        public async Task<T> UpdateAsync(T item)
        {
            try
            {
                var result = await _dataset.SingleOrDefaultAsync(p => p.Id.Equals(item.Id));
                if (result == null)
                {
                    throw new Exception("Any data to update");
                }
                item.UpdateAt = DateTime.UtcNow;
                item.CreatAt = result.CreatAt;

                _context.Entry(result).CurrentValues.SetValues(item);
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw e;
            }
            return item;
        }
    }
}
