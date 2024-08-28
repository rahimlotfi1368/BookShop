using Microsoft.EntityFrameworkCore;
using Presentation.Shared.Domain.Entities;
using Presentation.Shared.Domain.Entities.Common;
using Presentation.Shared.Extensions;

namespace Presentation.Shared.Infrastracture.Persistence;

public class AppDbContext:DbContext
{
    public DbSet<User> Users { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options):base(options) { }
   
    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        SetTimestamps();
        return base.SaveChangesAsync(cancellationToken);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("TbUser");
            
            entity.HasKey(e => e.Id);
            
            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd();

            entity.Property(e => e.CreateAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP");
            
            entity.Property(e => e.UpdateAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP");
            
            entity.Property(e => e.Username)
                .IsRequired()
                .HasMaxLength(50);

            entity.Property(e => e.PasswordHash)
                .IsRequired();

            entity.Property(e => e.Role)
                .IsRequired()
                .HasMaxLength(30);

            entity.HasIndex(e => e.Username)
                .IsUnique();
            
            entity.HasIndex(e => e.PasswordHash)
                .IsUnique();

        });
        modelBuilder.Entity<User>().HasData(
            new User{Id = 1,Username = "admin",PasswordHash ="admin123".ComputeSha256Hash(),Role = "Admin"},
            new User{Id = 2,Username = "user",PasswordHash ="user123".ComputeSha256Hash(),Role = "User"}
            );
    }
    
    private void SetTimestamps()
    {
        var entities = ChangeTracker.Entries<BaseEntity>();

        foreach (var entry in entities)
        {
            if (entry.State == EntityState.Added)
            {
                entry.Entity.CreateAt = DateTime.UtcNow;
                entry.Entity.UpdateAt = DateTime.UtcNow;
            }
            
            if (entry.State == EntityState.Modified)
            {
                entry.Entity.UpdateAt = DateTime.UtcNow;
            }
        }
    }
}