using Teknokent.Data.EntityFramework;
using Teknokent.Data;
using Teknokent.Interfaces;
using Teknokent.Models;
using Microsoft.EntityFrameworkCore;

namespace Teknokent.Repositories
{
    public class BoardOfMemberRepository : EfEntityRepository<BoardOfMember, ApplicationDbContext>, IBoardOfMemberRepository
    {
        public BoardOfMemberRepository(ApplicationDbContext context) : base(context)
        {
        }

       
    }
}
