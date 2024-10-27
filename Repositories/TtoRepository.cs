using Teknokent.Data.EntityFramework;
using Teknokent.Data;
using Teknokent.Interfaces;
using Teknokent.Models;

namespace Teknokent.Repositories
{
    public class TtoRepository : EfEntityRepository<Tto, ApplicationDbContext>, ITtoRepository
    {
        public TtoRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}

