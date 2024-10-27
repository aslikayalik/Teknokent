using Teknokent.Data.EntityFramework;
using Teknokent.Data;
using Teknokent.Interfaces;
using Teknokent.Models;

namespace Teknokent.Repositories
{
    public class CareerPostingRepository : EfEntityRepository<CareerPosting, ApplicationDbContext>, ICareerPostingRepository
    {
        public CareerPostingRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
