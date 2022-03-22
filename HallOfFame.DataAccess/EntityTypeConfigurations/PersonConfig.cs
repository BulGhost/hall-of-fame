using HallOfFame.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HallOfFame.DataAccess.EntityTypeConfigurations;

internal class PersonConfig : IEntityTypeConfiguration<PersonModel>
{
    public void Configure(EntityTypeBuilder<PersonModel> builder)
    {
        builder.ToTable("Persons");
        builder.HasKey(person => person.Id);
        builder.Property(person => person.Id).ValueGeneratedOnAdd();
        builder.Property(person => person.Name).IsRequired().HasMaxLength(60);
        builder.Property(person => person.DisplayName).IsRequired().HasMaxLength(30);

        builder.HasMany(p => p.Skills)
            .WithOne()
            .HasForeignKey(s => s.PersonId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}