using Microsoft.EntityFrameworkCore;
using FamilyArchiveBackend.Models;

namespace FamilyArchiveBackend.Data
{
    public class FamilyArchiveContext : DbContext
    {
        public FamilyArchiveContext(DbContextOptions<FamilyArchiveContext> options)
            : base(options) { }

        public DbSet<User> Users => Set<User>();
        public DbSet<Member> Members => Set<Member>();
        public DbSet<MemberRelationship> MemberRelationships => Set<MemberRelationship>();
        public DbSet<MemberSpouse> MemberSpouses => Set<MemberSpouse>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Parent/Child composite key
            modelBuilder.Entity<MemberRelationship>()
                .HasKey(r => new { r.ParentId, r.ChildId });

            modelBuilder.Entity<MemberRelationship>()
                .HasOne(r => r.Parent)
                .WithMany(m => m.Children)
                .HasForeignKey(r => r.ParentId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<MemberRelationship>()
                .HasOne(r => r.Child)
                .WithMany(m => m.Parents)
                .HasForeignKey(r => r.ChildId)
                .OnDelete(DeleteBehavior.Restrict);

            // Spouse composite key
            modelBuilder.Entity<MemberSpouse>()
                .HasKey(s => new { s.Spouse1Id, s.Spouse2Id });

            modelBuilder.Entity<MemberSpouse>()
                .HasOne(s => s.Spouse1)
                .WithMany(m => m.Spouses1)
                .HasForeignKey(s => s.Spouse1Id)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<MemberSpouse>()
                .HasOne(s => s.Spouse2)
                .WithMany(m => m.Spouses2)
                .HasForeignKey(s => s.Spouse2Id)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}