using Microsoft.EntityFrameworkCore; // VITAL para FirstOrDefaultAsync
using Microservicios.Alojamiento.DataAccess.Context;
using Microservicios.Alojamiento.DataAccess.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Microservicios.Alojamiento.DataAccess.Common
{
    public class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected readonly AlojamientoDbContext _context;
        protected readonly DbSet<T> _dbSet;

        public RepositoryBase(AlojamientoDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        // --- EL MÉTODO QUE FALTA ---
        public async Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.FirstOrDefaultAsync(predicate);
        }

        // Implementación de FindAsync (que ya tenías en la interfaz)
        public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.Where(predicate).ToListAsync();
        }

        public async Task<T?> GetByIdAsync(Guid id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public void Update(T entity)
        {
            _dbSet.Update(entity);
        }

        public void Delete(T entity)
        {
            _dbSet.Remove(entity);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync()) > 0;
        }
    }
}