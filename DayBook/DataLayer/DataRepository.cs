using DayBook.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace DayBook.DataLayer
{
    public class DataRepository<TEntity> : IDisposable,IDataRepository<TEntity> where TEntity:class,IDisposable
    {
        private ApplicationDbContext _db;
        private DbSet<TEntity> _dbSet;
        public DataRepository()
        {
            _db = new ApplicationDbContext();
            _dbSet = _db.Set<TEntity>();
        }
        public Task CreateRecord(TEntity record)
        {
            return Task.Run(async ()=> 
            {
                _dbSet.Add(record);
                await _db.SaveChangesAsync();
            });
        }

        public Task DeleteRecord(int id)
        {
            return Task.Run(async () =>
            {
                var item = await _dbSet.FindAsync(id);
                _dbSet.Remove(item);
                await _db.SaveChangesAsync();
            });
        }

        public async Task<IEnumerable<TEntity>> GetRecords()
        {
            return await _dbSet.ToListAsync();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_db != null) _db.Dispose();
            }
        }
    }
}