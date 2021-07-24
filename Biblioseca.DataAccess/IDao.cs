using System.Collections.Generic;

namespace Biblioseca.DataAccess
{
    public interface IDao<T>
    {
        void Save(T entity);
        void Delete(T entity);
        T Get(int id);
        IEnumerable<T> GetAll();
    }
}