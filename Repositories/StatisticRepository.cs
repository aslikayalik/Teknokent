using Teknokent.Data;
using Teknokent.Data.EntityFramework;
using Teknokent.Interfaces;
using Teknokent.Models;

namespace Teknokent.Repositories
{
    public class StatisticRepository : EfEntityRepository<Statistic, ApplicationDbContext>, IStatisticRepository
    {
        public StatisticRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
