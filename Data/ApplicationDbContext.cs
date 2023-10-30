using System.Linq.Expressions;

namespace ASP.NET_Custom_Identity_Starter.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            RegisterField<ApplicationUser>(builder, "FirstName", "_firstName");
            RegisterField<ApplicationUser>(builder, "LastName", "_lastName");
            RegisterField<ApplicationUser>(builder, "DateOfBirth", "_dateOfBirth");
            RegisterField<ApplicationUser>(builder, "ProfilePictureUrl", "_profilePictureUrl");
            RegisterField<ApplicationUser>(builder, "Gender", "_gender");
            RegisterField<ApplicationUser>(builder, "Address", "_address");
            RegisterField<ApplicationUser>(builder, "City", "_city");
            RegisterField<ApplicationUser>(builder, "State", "_state");
            RegisterField<ApplicationUser>(builder, "Country", "_country");
            RegisterField<ApplicationUser>(builder, "PostalCode", "_postalCode");
            RegisterField<ApplicationUser>(builder, "PreferredLanguage", "_preferredLanguage");
            RegisterField<ApplicationUser>(builder, "TimeZone", "_timeZone");
            RegisterField<ApplicationUser>(builder, "Theme", "_theme");

        }

        private static void RegisterField<TEntity>(
            ModelBuilder builder,
            string propName,
            string fieldName) where TEntity : class
        {
            var parameter = Expression.Parameter(typeof(TEntity), "e");
            var property = Expression.Property(parameter, propName);

            // Convert value types to object
            var converted = Expression.Convert(property, typeof(object));

            var lambda = Expression.Lambda<Func<TEntity, object>>(converted, parameter);

            builder.Entity<TEntity>()
                   .Property(lambda)
                   .HasField(fieldName);
        }
    }
}