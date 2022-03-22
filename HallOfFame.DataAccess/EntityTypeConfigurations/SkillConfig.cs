using HallOfFame.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HallOfFame.DataAccess.EntityTypeConfigurations;

internal class SkillConfig : IEntityTypeConfiguration<SkillModel>
{
    public void Configure(EntityTypeBuilder<SkillModel> builder)
    {
        builder.ToTable("Skills");
        builder.HasKey(ps => new { ps.PersonId, ps.Name });
        builder.Property(ps => ps.Level).IsRequired().HasColumnType("tinyint");
        builder.HasCheckConstraint($"CK_PersonSkills_Level", "Level >= 1 AND Level <= 10");
    }
}