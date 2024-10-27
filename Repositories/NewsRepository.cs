using Teknokent.Data.EntityFramework;
using Teknokent.Data;
using Teknokent.Interfaces;
using Teknokent.Models;

namespace Teknokent.Repositories
{
    public class NewsRepository : EfEntityRepository<News, ApplicationDbContext>, INewsRepository
    {
        public NewsRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
