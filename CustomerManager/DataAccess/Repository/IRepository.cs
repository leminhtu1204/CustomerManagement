using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomerManager.Model;
using DataAccess.Entity;

namespace DataAccess.Repository
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetEntities();
        T GetEntityById(int id);
        OperationStatus InsertEntity(T entity);
        OperationStatus DeleteEntity(int id);
        OperationStatus UpdateEntity(T entity);
        void Save();
        UnitOfWork.UnitOfWork BeginUnitOfWork();
    }
}
