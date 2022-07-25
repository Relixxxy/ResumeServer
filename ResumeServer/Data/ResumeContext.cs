using Microsoft.EntityFrameworkCore;
using ResumeServer.Models;

namespace ResumeServer.Data
{
    public class ResumeContext : DbContext
    {
        public ResumeContext(DbContextOptions<ResumeContext> options) : base(options)
        {

        }
        public virtual DbSet<User> Users { get; set; } = default;
        public virtual DbSet<UserProject> UserProjects { get; set; } = default;
        public virtual DbSet<Area> Areas { get; set; } = default;
        public virtual DbSet<Section> Sections { get; set; } = default;
        public virtual DbSet<MainSkill> MainSkills { get; set; } = default;
        public virtual DbSet<AdditionSkill> AdditionSkills { get; set; } = default;
        public virtual DbSet<Credentials> Credentials { get; set; } = default;

    }
}
