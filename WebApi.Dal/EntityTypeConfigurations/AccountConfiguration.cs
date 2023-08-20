using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApi.Dal.Entities;

namespace WebApi.Dal.EntityTypeConfigurations
{
    class AccountConfiguration : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {
            builder
               .ToTable("Account", "dbo");

            builder
               .HasKey(e => e.AccountID);

            //builder
            //   .Property(e => e.AccountID)
            //   .ValueGeneratedNever();

            builder
                .HasIndex(e => e.Name)
                .IsUnique();


            builder
               .HasOne<Incident>()
               .WithMany(d => d.Accounts)
               .HasForeignKey(w => w.AccountID)
               .HasPrincipalKey(e => e.AccountID);


        }
    }
}