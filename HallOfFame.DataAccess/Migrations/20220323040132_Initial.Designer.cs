// <auto-generated />
using HallOfFame.DataAccess.DbContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace HallOfFame.DataAccess.Migrations
{
    [DbContext(typeof(HallOfFameDbContext))]
    [Migration("20220323040132_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("HallOfFame.DataAccess.Models.PersonModel", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"), 1L, 1);

                    b.Property<string>("DisplayName")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(60)
                        .HasColumnType("nvarchar(60)");

                    b.HasKey("Id");

                    b.ToTable("Persons", (string)null);
                });

            modelBuilder.Entity("HallOfFame.DataAccess.Models.SkillModel", b =>
                {
                    b.Property<long>("PersonId")
                        .HasColumnType("bigint");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<byte>("Level")
                        .HasColumnType("tinyint");

                    b.HasKey("PersonId", "Name");

                    b.ToTable("Skills", (string)null);

                    b.HasCheckConstraint("CK_PersonSkills_Level", "Level >= 1 AND Level <= 10");
                });

            modelBuilder.Entity("HallOfFame.DataAccess.Models.SkillModel", b =>
                {
                    b.HasOne("HallOfFame.DataAccess.Models.PersonModel", null)
                        .WithMany("Skills")
                        .HasForeignKey("PersonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("HallOfFame.DataAccess.Models.PersonModel", b =>
                {
                    b.Navigation("Skills");
                });
#pragma warning restore 612, 618
        }
    }
}
