using Teknokent.Data.EntityFramework;
using Teknokent.Data;
using Teknokent.Interfaces;
using Teknokent.Models;

namespace Teknokent.Repositories
{
    public class FaqRepository : EfEntityRepository<Faq, ApplicationDbContext>, IFaqRepository
    {
        public FaqRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
