using System.Collections.Generic;

namespace ChatApp.Domain.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        void Delete(object id);
        IEnumerable<T> GetAll();
        T GetById(object id);
        void Insert(T obj);
        void Save();
        void Update(T obj);
    }
}