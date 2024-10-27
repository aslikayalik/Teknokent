using System.Security.Principal;

namespace Teknokent.Data.EntityFramework
{
    public interface IEntityRepository<T> where T : class, new()
    {
        Task<T> GetByIdAsync(int id);
        bool Add(T entity);
        bool Update(T entity);
        bool Delete(T entity);
        bool Save();

        int GetTotalCount();
        List<T> GetAll(int page, int pageSize);
       // bool IfExists(int id);
    }
}
