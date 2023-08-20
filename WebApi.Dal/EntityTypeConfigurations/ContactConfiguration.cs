using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApi.Dal.Entities;

namespace WebApi.Dal.EntityTypeConfigurations
{
    class ContactConfiguration : IEntityTypeConfiguration<Contact>
    {
        public void Configure(EntityTypeBuilder<Contact> builder)
        {
            builder
               .ToTable("Contact", "dbo");

            builder
               .HasKey(e => e.ContactID);

            builder
                .HasIndex(e => e.Email)
                .IsUnique();

            builder
              .HasOne<Account>()
              .WithMany(e => e.Contacts)
              .HasForeignKey(e => e.ContactID)
              .HasConstraintName("FK__Account__Contact__4316F928");
        }
    }
}