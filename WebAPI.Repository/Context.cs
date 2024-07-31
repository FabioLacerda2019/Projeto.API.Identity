using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using WebAPI.Domain;

namespace WebAPI.Repository;

public class Context : IdentityDbContext<User>
{
    public Context(DbContextOptions<Context> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<UserRole>(userRole =>
            {
                userRole.HasKey(ur => new { ur.UserId, ur.RoleId });
                 
                userRole.HasOne(ur => ur.Role)
                        .WitMany(r => r.UserRoles)
                        .HasForeignKey(ur => ur.RoleId)
                        .IsRequired();

                userRole.HasOne(ur => ur.User)
                        .WitMany(r => r.UserRoles)
                        .HasForeignKey(ur => ur.UserId)
                        .IsRequired();
            });

            builder.Entity<Organizacao>(org =>
                 {
                     org.ToTable("Organizacoes");
                     org.HasKey(x => x.Id);

                     org.HasMany<User>()
                     .WithOne()
                     .HasForeignKey(x => x.OrgId)
                     .IsRequired(false);
                 }
                 );
        }
    
}
