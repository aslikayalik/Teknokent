using Teknokent.Data.EntityFramework;
using Teknokent.Data;
using Teknokent.Interfaces;
using Teknokent.Models;

namespace Teknokent.Repositories
{
    public class CvRepository : EfEntityRepository<Cv, ApplicationDbContext>, ICvRepository
    {
        public CvRepository(ApplicationDbContext context) : base(context)
        {
        }


    }
}
