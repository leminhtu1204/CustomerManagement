using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomerManager.Model;
using CustomerManager.Repository;
using DataAccess.Entity;

namespace DataAccess.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected DbContext _Context;
        protected DbSet<T> dbSet;
        protected UnitOfWork.UnitOfWork UnitOfWork;

        public UnitOfWork.UnitOfWork BeginUnitOfWork()
        {
            return this.UnitOfWork ?? (this.UnitOfWork = new UnitOfWork.UnitOfWork(_Context));
        }

        public Repository()
        {
            this._Context = new CustomerManagerContext();
            this.dbSet = this._Context.Set<T>();
        }

        public IEnumerable<T> GetEntities()
        {
            return dbSet.ToList();
        }

        public T GetEntityById(int id)
        {
            return dbSet.Find(id);
        }

        public OperationStatus InsertEntity(T entity)
        {
            var opStatus = new OperationStatus { Status = true };
            try
            {
                dbSet.Add(entity);
                _Context.SaveChanges();
            }
            catch (Exception exp)
            {
                opStatus.Status = false;
                opStatus.ExceptionMessage = exp.Message;
            }
            return opStatus;
        }

        public OperationStatus DeleteEntity(int id)
        {
            var opStatus = new OperationStatus { Status = true };
            try
            {
                T entity = dbSet.Find(id);
                if (entity != null)
                {
                    dbSet.Remove(entity);
                }
                else
                {
                    opStatus.Status = false;
                    opStatus.ExceptionMessage = "Customer not found";
                }
            }
            catch (Exception exp)
            {
                opStatus.Status = false;
                opStatus.ExceptionMessage = exp.Message;
            }
            return opStatus;
        }

        public OperationStatus UpdateEntity(T entity)
        {
            var opStatus = new OperationStatus { Status = true };
            try
            {
                dbSet.Attach(entity);
                _Context.Entry(entity).State = EntityState.Modified;
            }
            catch (Exception exp)
            {
                opStatus.Status = false;
                opStatus.ExceptionMessage = exp.Message;
            }
            return opStatus;
        }

        public void Save()
        {
            UnitOfWork.SaveChanges();
        }
    }
}
