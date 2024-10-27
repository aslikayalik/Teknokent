using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Security.Principal;


namespace Teknokent.Data.EntityFramework
{
    public class EfEntityRepository<T, TContext> where T : class, new() where TContext : IdentityDbContext, new()
    {
        private TContext _context;
        public EfEntityRepository(TContext context)
        {
            _context = context;
        }
       


        public bool Add(T entity)
        {
            _context.Add(entity);
            return Save();
        }

        public bool Delete(T entity)
        {
            _context.Remove(entity);
            return Save();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            
            return await _context.Set<T>().FindAsync(id);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool Update(T entity)
        {
            _context.Update(entity);
            return Save();
        }


        public int GetTotalCount()
        {
            return _context.Set<T>().Count();
        }
        public List<T> GetAll(int page, int pageSize)
        {
            if (page < 1)
            {
                page = 1; 
            }

            return _context.Set<T>()
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();
        }

        /*
        private bool IfExists(int id)
        {
            return _context.Set<T>().Any(e => e.Id == id);


        }
        */


    }
}
