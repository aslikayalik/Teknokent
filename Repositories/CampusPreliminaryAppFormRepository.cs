using Teknokent.Data.EntityFramework;
using Teknokent.Data;
using Teknokent.Interfaces;
using Teknokent.Models;

namespace Teknokent.Repositories
{
    public class CampusPreliminaryAppFormRepository : EfEntityRepository<CampusPreliminaryAppForm, ApplicationDbContext>, ICampusPreliminaryAppFormRepository
    {
        public CampusPreliminaryAppFormRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
