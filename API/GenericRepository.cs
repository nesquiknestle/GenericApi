using API.Interfaces;
using API.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly db_a7e6f8_hotelContext _context;
        private readonly DbSet<T> _dbSet;

        public GenericRepository(db_a7e6f8_hotelContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public async Task<T> GetById(int id) => await _dbSet.FindAsync(id);
        public async Task<IEnumerable<T>> GetAll() => await _dbSet.ToListAsync();
        public async Task Add(T entity) => await _dbSet.AddAsync(entity);
        public void Update(T entity) => _dbSet.Update(entity);
        public void Delete(T entity) => _dbSet.Remove(entity);
        public async Task SaveChangesAsync() => await _context.SaveChangesAsync();
    }
}
