using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Teknokent.Models;

namespace Teknokent.Data
{
    public class ApplicationDbContext :IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public ApplicationDbContext() : base()
        {
        }

        public DbSet<ApplicationUser> ApplicationUser { get; set; }
        public DbSet<Article> Article { get; set; }
        public DbSet<BoardOfMember> BoardOfMember { get; set; }
        public DbSet<CareerPosting> CareerPosting { get; set; }
        public DbSet<Company> Company { get; set; }
        public DbSet<Cv> Cv { get; set; }
        public DbSet<Event> Event { get; set; }
        public DbSet<Faq> Faq { get; set; }
        public DbSet<News> News { get; set; }
        public DbSet<Office> Office { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<Project> Project { get; set; }
        public DbSet<Statistic> Statistic { get; set; }


        public DbSet<RefereeRegistration> RefereeRegistration { get; set; }
        public DbSet<Report> Report { get; set; }
        public DbSet<CampusPreliminaryAppForm> CampusPreliminaryAppForm { get; set; }
        public DbSet<Legislation> Legislation { get; set; }
        public DbSet<Tto> Tto { get; set; }
        public DbSet<GeneralInformation> GeneralInformation { get; set; }
        public DbSet<ManagementOffice> ManagementOffice { get; set; }

    }
}
