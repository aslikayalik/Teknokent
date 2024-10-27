using Teknokent.Data.EntityFramework;
using Teknokent.Data;
using Teknokent.Interfaces;
using Teknokent.Models;

namespace Teknokent.Repositories
{
    public class LegislationRepository : EfEntityRepository<Legislation, ApplicationDbContext>, ILegislationRepository
    {
        public LegislationRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
