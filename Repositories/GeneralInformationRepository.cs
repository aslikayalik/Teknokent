using Teknokent.Data.EntityFramework;
using Teknokent.Data;
using Teknokent.Interfaces;
using Teknokent.Models;

namespace Teknokent.Repositories
{
    public class GeneralInformationRepository : EfEntityRepository<GeneralInformation, ApplicationDbContext>, IGeneralInformationRepository
    {
        public GeneralInformationRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
