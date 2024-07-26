using HomeWork.Models;
using Microsoft.EntityFrameworkCore;

namespace HomeWork.Context
{
    public class MemberDbContext :DbContext
    {
        public MemberDbContext(DbContextOptions<MemberDbContext> options):base(options) { }
        public DbSet<MemberModel> memberModels { get; set; }
    }
}
