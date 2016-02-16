using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.UnitOfWork
{
    public class UnitOfWork : IDisposable
    {
        /// <summary> 
        /// The DbContext 
        /// </summary> 
        private DbContext _dbContext;

        private DbContextTransaction _dbContextTransaction;

        /// <summary> 
        /// Initializes a new instance of the UnitOfWork class. 
        /// </summary> 
        /// <param name="context">The object context</param> 
        public UnitOfWork(DbContext dbContext)
        {
            this._dbContext = dbContext;
            this._dbContextTransaction = this._dbContext.Database.BeginTransaction();
        }

        /// <summary> 
        /// Saves all pending changes 
        /// </summary> 
        /// <returns>The number of objects in an Added, Modified, or Deleted state</returns> 
        public void SaveChanges()
        {
            this._dbContext.SaveChanges();
            this._dbContextTransaction.Commit();
        }

        /// <summary> 
        /// Disposes the current object 
        /// </summary> 
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary> 
        /// Disposes all external resources. 
        /// </summary> 
        /// <param name="disposing">The dispose indicator.</param> 
        private void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_dbContext != null)
                {
                    _dbContext.Dispose();
                    _dbContext = null;
                }
            }
        }
    }
}
