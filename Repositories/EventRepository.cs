using Teknokent.Data.EntityFramework;
using Teknokent.Data;
using Teknokent.Interfaces;
using Teknokent.Models;

namespace Teknokent.Repositories
{
    public class EventRepository : EfEntityRepository<Event, ApplicationDbContext>, IEventRepository
    {
        public EventRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
