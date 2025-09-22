using Microsoft.EntityFrameworkCore;
using TodoCheckerApp.Dto;

namespace TodoCheckerApp.Contexts
{
    public class MenuContext : DbContext
    {
        public MenuContext(DbContextOptions<MenuContext> options) : base(options){}

        public DbSet<MenuRowDto> Walkman { get; set;  }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MenuRowDto>().ToTable("Walkman"); // 既存テーブル名を指定
        }

    }
    //public class MenuContext : DbContext
    //{

    //    public MenuContext(DbContextOptions<MenuContext> options) : base(options) { }

    //    public DbSet<MenuRowDto> WalkmanMain { get; set; }

    //    protected override void OnModelCreating(ModelBuilder modelBuilder)
    //    {
    //        modelBuilder.Entity<MenuRowDto>().ToTable("WalkmanMain"); // テーブル名を明示
    //    }
    //}
}
