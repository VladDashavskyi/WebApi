using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApi.Dal.Entities;

namespace WebApi.Dal.EntityTypeConfigurations
{
    class IncidentConfiguration : IEntityTypeConfiguration<Incident>
    {
        public void Configure(EntityTypeBuilder<Incident> builder)
        {
            builder
               .ToTable("Incident", "dbo");

            builder
               .HasKey(e => e.IncidentName);


            builder
               .Property(e => e.IncidentName)
               .ValueGeneratedNever();

            //builder
            //    .Property(w => w.IncidentName).HasDefaultValueSql("NEWID()");    


        }
    }
}