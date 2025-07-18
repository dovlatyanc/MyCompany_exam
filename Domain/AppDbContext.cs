using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;
using MyCompany.Domain.Entities;

namespace MyCompany.Domain;

public class AppDbContext : IdentityDbContext<IdentityUser>
{
    public DbSet<ServiceCategory> ServiceCategories { get; set; } = null!;
    public DbSet<Service> Services { get; set; } = null!;

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        string adminName = "admin";
        string roleAdminId = "7c6e3d5b-8f2a-4d6f-9e1c-2a8f3d5e1c2a";
        string userAdminId = "3a2f1e9b-4c7d-4e8f-9a1b-2c3d4e5f6a7b";

        //add role admin

        builder.Entity<IdentityRole>().HasData(new IdentityRole()
        {

            Id = roleAdminId,
            Name = adminName,
            NormalizedName = adminName.ToUpper()

        });
        //добавляем нового идентити юзер  в качестве админа 
        builder.Entity<IdentityUser>().HasData(new IdentityUser()
        {

            Id = userAdminId,
            UserName = adminName,
            NormalizedUserName = adminName.ToUpper(),
            Email = "admin@admin.com",
            NormalizedEmail = "admin@admin.com",
            EmailConfirmed = true,
            PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(new IdentityUser(), adminName),//admin admin!
            SecurityStamp = string.Empty,
            PhoneNumberConfirmed = true,

        });
        //определяем админу соотв роль

        builder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>()
        {
            RoleId = roleAdminId,
            UserId = userAdminId,
        });
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=MyCompanyDb;Trusted_Connection=True;MultipleActiveResultSets=true")
            .ConfigureWarnings(warnings => warnings.Ignore(RelationalEventId.PendingModelChangesWarning));
    }

}
