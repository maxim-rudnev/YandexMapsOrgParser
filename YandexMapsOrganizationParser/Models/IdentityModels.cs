using System.Collections.Generic;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace YandexMapsOrganizationParser.Models
{

    public class CustomIdentityUser: IdentityUser
    {
        public CustomIdentityUser()
        {
            Payments = new List<PaymentNotification>();
        }

        // Количество запросов, которое осталось у пользователя
        public int RequestsLeft { get; set; } = Globals.DefaultRequestLeft;

        // Выполненные платежи
        public virtual ICollection<PaymentNotification> Payments { get; set; }
        
    }

    public class ApplicationUser : CustomIdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);

            userIdentity.AddClaim(new Claim("WebApplication.Models.RegisterViewModel.Email", Email));

            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }


        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }


        public DbSet<AnonUser> AnonUsers { get; set; }

        public DbSet<PaymentNotification> Payments { get; set; }

        public DbSet<Order> Orders { get; set; }
    }
}