using Hospital.Models;
using Hospital.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
namespace Hospital.Repositories.Implementation
{
    public class GenericRepository<T> : IDisposable ,IGenericRepository<T> where T : class
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<T> dbset;
         
        public GenericRepository(ApplicationDbContext context)
        {
            _context = context;
            dbset = _context.Set<T>(); 
        }

        // Add a new entity
        public void Add(T entity)
        {
            dbset.Add(entity);
        }
        // Asynchronously add a new entity
        public async Task<T> AddAsync(T entity)
        {
            dbset.Add(entity);
            return entity;
        }
        // Delete an entity
        public void Delete(T entity)
        {
            if (_context.Entry(entity).State == EntityState.Detached)
            {
                dbset.Attach(entity);
            }
            dbset.Remove(entity);
        }
        // Asynchronously delete an entity
        public async Task<T> DeleteAsync(T entity)
        {
            if (_context.Entry(entity).State == EntityState.Detached)
            {
                dbset.Attach(entity); 
            }
            dbset.Remove(entity);
            return entity;
        }
        // Get all entities, optionally filtered, ordered, and with included properties
        public IEnumerable<T> GetAll(Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = "")
        {
            IQueryable<T> query = dbset;

            if (filter != null)
            {
                query = query.Where(filter);
            }
            foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }
            if (orderBy != null)
            {
                return orderBy(query).ToList();
            }
            else
            {
                return query.ToList();
            }
        } 
        // Get an entity by ID
        public T GetById(object id)
        {
            return dbset.Find(id);
        }
        // Asynchronously get an entity by ID
        public async Task<T> GetByIdAsync(object id)
        {
            return await dbset.FindAsync(id);
        }
        // Update an entity
        public void Update(T entity)
        {
            dbset.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }
        // Asynchronously update an entity
        public async Task<T> UpdateAsync(T entity)
        {
            dbset.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
            return entity;
        }
        // Dispose pattern implementation
        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        public IEnumerable<T> GetAll(Expression<Func<T, bool>> filter = null, string includeProperties = "")
        {
            {
                IQueryable<T> query = _context.Set<T>();

                if (filter != null)
                {
                    query = query.Where(filter);
                }

                foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }

                return query.ToList();
            }
        }
        public void update(T entity)
        {
            throw new NotImplementedException();
        }
        Task IGenericRepository<T>.DeleteAsync(T entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(HospitalInfo model)
        {
            throw new NotImplementedException();
        }
    }
}
