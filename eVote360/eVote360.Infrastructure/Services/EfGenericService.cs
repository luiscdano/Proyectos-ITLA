using eVote360.Application.Interfaces;
using eVote360.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace eVote360.Infrastructure.Services
{
    public class EfGenericService<T> : IGenericService<T> where T : class
    {
        private readonly AppDbContext _db;

        public EfGenericService(AppDbContext db)
        {
            _db = db;
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await _db.Set<T>()
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<T?> GetByIdAsync(int id)
        {
            return await _db.Set<T>().FindAsync(id);
        }

        public async Task AddAsync(T entity)
        {
            _db.Set<T>().Add(entity);
            await _db.SaveChangesAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            _db.Set<T>().Update(entity);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteAsync(T entity)
        {
            _db.Set<T>().Remove(entity);
            await _db.SaveChangesAsync();
        }
    }
}