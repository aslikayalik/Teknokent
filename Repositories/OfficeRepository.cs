using Teknokent.Data.EntityFramework;
using Teknokent.Data;
using Teknokent.Interfaces;
using Teknokent.Models;

namespace Teknokent.Repositories
{
    public class OfficeRepository : EfEntityRepository<Office, ApplicationDbContext>, IOfficeRepository
    {
        public OfficeRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
