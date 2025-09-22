using Microsoft.EntityFrameworkCore;
using TodoCheckerApp.Dto;

namespace TodoCheckerApp.Contexts.WLK
{
    public class WalkmanContext : DbContext
    {
        public WalkmanContext(DbContextOptions<WalkmanContext> options) : base(options) { }

        public DbSet<Walkman> Walkman { get; set; }  // ← DTO ではなく DB エンティティを指定

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Walkman>().ToTable("Walkman"); // 既存テーブル名
        }
    }
}
