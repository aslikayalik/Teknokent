using Teknokent.Data.EntityFramework;
using Teknokent.Data;
using Teknokent.Interfaces;
using Teknokent.Models;

namespace Teknokent.Repositories
{
    public class ManagementOfficeRepository : EfEntityRepository<ManagementOffice, ApplicationDbContext>, IManagementOfficeRepository
    {
        public ManagementOfficeRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
