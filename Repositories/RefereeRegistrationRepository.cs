using Teknokent.Data.EntityFramework;
using Teknokent.Data;
using Teknokent.Interfaces;
using Teknokent.Models;

namespace Teknokent.Repositories
{
    public class RefereeRegistrationRepository : EfEntityRepository<RefereeRegistration, ApplicationDbContext>, IRefereeRegistrationRepository
    {
        public RefereeRegistrationRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
