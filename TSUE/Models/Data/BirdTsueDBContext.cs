using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace TSUE.Models.Data
{
    public partial class BirdTsueDBContext : DbContext
    {
        public BirdTsueDBContext()
        {
        }

        public BirdTsueDBContext(DbContextOptions<BirdTsueDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AnalyticType> AnalyticTypes { get; set; }
        public virtual DbSet<AnalyticTypeResponse> AnalyticTypeResponses { get; set; }
        public virtual DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public virtual DbSet<ApplicationUserRole> ApplicationUserRoles { get; set; }
        public virtual DbSet<Country> Countries { get; set; }
        public virtual DbSet<DocumentType> DocumentTypes { get; set; }
        public virtual DbSet<Language> Languages { get; set; }
        public virtual DbSet<Project> Projects { get; set; }
        public virtual DbSet<ProjectComment> ProjectComments { get; set; }
        public virtual DbSet<ProjectCountry> ProjectCountries { get; set; }
        public virtual DbSet<ProjectDocument> ProjectDocuments { get; set; }
        public virtual DbSet<ProjectLanguage> ProjectLanguages { get; set; }
        public virtual DbSet<ProjectSector> ProjectSectors { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Sector> Sectors { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
          
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<AnalyticType>(entity =>
            {
                entity.HasKey(e => e.AnalyticTypeId)
                    .IsClustered(false);

                entity.ToTable("AnalyticType");

                entity.Property(e => e.AnalyticTypeName)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.CreatedBy).HasMaxLength(255);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            });

            modelBuilder.Entity<AnalyticTypeResponse>(entity =>
            {
                entity.HasKey(e => e.AnalyticTypeResponseId)
                    .IsClustered(false);

                entity.ToTable("AnalyticTypeResponse");

                entity.Property(e => e.Country).HasMaxLength(255);

                entity.Property(e => e.CreatedBy).HasMaxLength(255);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.StateOrCity).HasMaxLength(255);

                entity.HasOne(d => d.AnalyticType)
                    .WithMany(p => p.AnalyticTypeResponses)
                    .HasForeignKey(d => d.AnalyticTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("PK_AnalyticType_AnalyticTypeResponse");
            });

            modelBuilder.Entity<ApplicationUser>(entity =>
            {
                entity.ToTable("ApplicationUser");

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.UpdatedBy).HasMaxLength(150);

                entity.Property(e => e.UpdatedOn).HasColumnType("datetime");

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasMaxLength(150);
            });

            modelBuilder.Entity<ApplicationUserRole>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("ApplicationUserRole");

                entity.HasOne(d => d.ApplicationUser)
                    .WithMany()
                    .HasForeignKey(d => d.ApplicationUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ApplicationUserRole_User");

                entity.HasOne(d => d.Role)
                    .WithMany()
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ApplicationUserRole_Role");
            });

            modelBuilder.Entity<Country>(entity =>
            {
                entity.HasKey(e => e.CountryId)
                    .IsClustered(false);

                entity.ToTable("Country");

                entity.Property(e => e.CountryName)
                    .IsRequired()
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<DocumentType>(entity =>
            {
                entity.HasKey(e => e.DocumentTypeId)
                    .IsClustered(false);

                entity.ToTable("DocumentType");

                entity.Property(e => e.DocumentTypeName)
                    .IsRequired()
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<Language>(entity =>
            {
                entity.HasKey(e => e.LanguageId)
                    .IsClustered(false);

                entity.ToTable("Language");

                entity.Property(e => e.LanguageName)
                    .IsRequired()
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<Project>(entity =>
            {
                entity.HasKey(e => e.ProjectId)
                    .IsClustered(false);

                entity.ToTable("Project");

                entity.Property(e => e.Authors)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.CreatedBy).HasMaxLength(255);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.StudyTitle).HasMaxLength(255);

                entity.Property(e => e.UpdatedBy).HasMaxLength(10);

                entity.Property(e => e.UpdatedOn).HasColumnType("datetime");

                entity.Property(e => e.YearOfPublication).HasColumnType("date");

                entity.HasOne(d => d.DocumentType)
                    .WithMany(p => p.Projects)
                    .HasForeignKey(d => d.DocumentTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("PK_DocumentType_Project");
            });

            modelBuilder.Entity<ProjectComment>(entity =>
            {
                entity.HasKey(e => e.ProjectCommentId)
                    .IsClustered(false);

                entity.ToTable("ProjectComment");

                entity.Property(e => e.CreatedBy).HasMaxLength(266);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.Email).HasMaxLength(255);

                entity.Property(e => e.FullName).HasMaxLength(255);

                entity.Property(e => e.Message).IsRequired();

                entity.HasOne(d => d.Project)
                    .WithMany(p => p.ProjectComments)
                    .HasForeignKey(d => d.ProjectId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("PK_Project_ProjectComment");
            });

            modelBuilder.Entity<ProjectCountry>(entity =>
            {
                entity.HasKey(e => e.ProjectCountryId)
                    .IsClustered(false);

                entity.ToTable("ProjectCountry");

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.ProjectCountries)
                    .HasForeignKey(d => d.CountryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("PK_Country_ProjectCountry");

                entity.HasOne(d => d.Project)
                    .WithMany(p => p.ProjectCountries)
                    .HasForeignKey(d => d.ProjectId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("PK_Project_ProjectCountry");
            });

            modelBuilder.Entity<ProjectDocument>(entity =>
            {
                entity.HasKey(e => e.ProjectDocumentId)
                    .IsClustered(false);

                entity.ToTable("ProjectDocument");

                entity.Property(e => e.CreatedBy).HasMaxLength(255);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.DocumentName).HasMaxLength(255);

                entity.Property(e => e.UpdatedBy).HasMaxLength(255);

                entity.Property(e => e.UpdatedOn).HasColumnType("datetime");

                entity.HasOne(d => d.Project)
                    .WithMany(p => p.ProjectDocuments)
                    .HasForeignKey(d => d.ProjectId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("PK_Project_ProjectDocument");
            });

            modelBuilder.Entity<ProjectLanguage>(entity =>
            {
                entity.HasKey(e => e.ProjectLanguageId)
                    .IsClustered(false);

                entity.ToTable("ProjectLanguage");

                entity.HasOne(d => d.Language)
                    .WithMany(p => p.ProjectLanguages)
                    .HasForeignKey(d => d.LanguageId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("PK_Language_ProjectLanguage");

                entity.HasOne(d => d.Project)
                    .WithMany(p => p.ProjectLanguages)
                    .HasForeignKey(d => d.ProjectId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("PK_Project_ProjectLanguage");
            });

            modelBuilder.Entity<ProjectSector>(entity =>
            {
                entity.HasKey(e => e.ProjectSectorId)
                    .IsClustered(false);

                entity.ToTable("ProjectSector");

                entity.HasOne(d => d.Project)
                    .WithMany(p => p.ProjectSectors)
                    .HasForeignKey(d => d.ProjectId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("PK_Project_ProjectSector");

                entity.HasOne(d => d.Sector)
                    .WithMany(p => p.ProjectSectors)
                    .HasForeignKey(d => d.SectorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("PK_Sector_ProjectSector");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("Role");

                entity.Property(e => e.RoleName)
                    .IsRequired()
                    .HasMaxLength(150);
            });

            modelBuilder.Entity<Sector>(entity =>
            {
                entity.HasKey(e => e.SectorId)
                    .IsClustered(false);

                entity.ToTable("Sector");

                entity.Property(e => e.SectorName)
                    .IsRequired()
                    .HasMaxLength(255);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
