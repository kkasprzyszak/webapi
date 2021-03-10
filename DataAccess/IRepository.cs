using System.Collections.Generic;

namespace DataAccess
{
    public interface IRepository<T>
    {
        T Get(int id);

        List<T> Get();

        void Add(T entity);

        void Update(T entity);

        void Remove(T entity);
    }
}
