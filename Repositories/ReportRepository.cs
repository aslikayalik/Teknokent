using Teknokent.Data.EntityFramework;
using Teknokent.Data;
using Teknokent.Interfaces;
using Teknokent.Models;

namespace Teknokent.Repositories
{
    public class ReportRepository : EfEntityRepository<Report, ApplicationDbContext>, IReportRepository
    {
        public ReportRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
