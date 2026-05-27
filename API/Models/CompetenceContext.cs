using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace API.Models;

public partial class CompetenceContext : DbContext
{
    public CompetenceContext()
    {
    }

    public CompetenceContext(DbContextOptions<CompetenceContext> options)
        : base(options)
    {
    }

    public virtual DbSet<ApiLog> ApiLogs { get; set; }

    public virtual DbSet<Confirmation> Confirmations { get; set; }

    public virtual DbSet<ConfirmationStatus> ConfirmationStatuses { get; set; }

    public virtual DbSet<Education> Educations { get; set; }

    public virtual DbSet<EducationType> EducationTypes { get; set; }

    public virtual DbSet<Experience> Experiences { get; set; }

    public virtual DbSet<Institution> Institutions { get; set; }

    public virtual DbSet<Organization> Organizations { get; set; }

    public virtual DbSet<RatingHistory> RatingHistories { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<RoleHistory> RoleHistories { get; set; }

    public virtual DbSet<ShortList> ShortLists { get; set; }

    public virtual DbSet<Skill> Skills { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserRating> UserRatings { get; set; }

    public virtual DbSet<UserSkill> UserSkills { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Server=127.0.0.1:40001;Username=xaliks;Password=coolPaSsw0rd;Database=competence");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ApiLog>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("ApiLogs_pkey");

            entity.Property(e => e.Date)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone");

            entity.HasOne(d => d.User).WithMany(p => p.ApiLogs)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("ApiLogs_UserId_fkey");
        });

        modelBuilder.Entity<Confirmation>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Confirmations_pkey");

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone");

            entity.HasOne(d => d.Requestor).WithMany(p => p.ConfirmationRequestors)
                .HasForeignKey(d => d.RequestorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Confirmations_RequestorId_fkey");

            entity.HasOne(d => d.Skill).WithMany(p => p.Confirmations)
                .HasForeignKey(d => d.SkillId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Confirmations_SkillId_fkey");

            entity.HasOne(d => d.Status).WithMany(p => p.Confirmations)
                .HasForeignKey(d => d.StatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Confirmations_StatusId_fkey");

            entity.HasOne(d => d.Target).WithMany(p => p.ConfirmationTargets)
                .HasForeignKey(d => d.TargetId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Confirmations_TargetId_fkey");
        });

        modelBuilder.Entity<ConfirmationStatus>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("ConfirmationStatuses_pkey");
        });

        modelBuilder.Entity<Education>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Education_pkey");

            entity.ToTable("Education");

            entity.Property(e => e.EndDate).HasColumnType("timestamp without time zone");
            entity.Property(e => e.Order).HasDefaultValue(0);
            entity.Property(e => e.StartDate).HasColumnType("timestamp without time zone");

            entity.HasOne(d => d.EducationType).WithMany(p => p.Educations)
                .HasForeignKey(d => d.EducationTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Education_EducationTypeId_fkey");

            entity.HasOne(d => d.Institution).WithMany(p => p.Educations)
                .HasForeignKey(d => d.InstitutionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Education_InstitutionId_fkey");

            entity.HasOne(d => d.User).WithMany(p => p.Educations)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Education_UserId_fkey");
        });

        modelBuilder.Entity<EducationType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("EducationType_pkey");

            entity.ToTable("EducationType");
        });

        modelBuilder.Entity<Experience>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Experience_pkey");

            entity.ToTable("Experience");

            entity.Property(e => e.EndDate).HasColumnType("timestamp without time zone");
            entity.Property(e => e.StartDate).HasColumnType("timestamp without time zone");

            entity.HasOne(d => d.Company).WithMany(p => p.Experiences)
                .HasForeignKey(d => d.CompanyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Experience_CompanyId_fkey");

            entity.HasOne(d => d.User).WithMany(p => p.Experiences)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Experience_UserId_fkey");
        });

        modelBuilder.Entity<Institution>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Institutions_pkey");
        });

        modelBuilder.Entity<Organization>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Organizations_pkey");

            entity.HasIndex(e => e.Name, "Organizations_Name_key").IsUnique();
        });

        modelBuilder.Entity<RatingHistory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("RatingHistory_pkey");

            entity.ToTable("RatingHistory");

            entity.Property(e => e.Date)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone");
            entity.Property(e => e.Value).HasPrecision(5, 2);

            entity.HasOne(d => d.User).WithMany(p => p.RatingHistories)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("RatingHistory_UserId_fkey");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Roles_pkey");

            entity.HasIndex(e => e.Name, "Roles_Name_key").IsUnique();
        });

        modelBuilder.Entity<RoleHistory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("RoleHistory_pkey");

            entity.ToTable("RoleHistory");

            entity.Property(e => e.Date)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone");

            entity.HasOne(d => d.NewRole).WithMany(p => p.RoleHistoryNewRoles)
                .HasForeignKey(d => d.NewRoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("RoleHistory_NewRoleId_fkey");

            entity.HasOne(d => d.OldRole).WithMany(p => p.RoleHistoryOldRoles)
                .HasForeignKey(d => d.OldRoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("RoleHistory_OldRoleId_fkey");

            entity.HasOne(d => d.User).WithMany(p => p.RoleHistories)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("RoleHistory_UserId_fkey");
        });

        modelBuilder.Entity<ShortList>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("ShortLists_pkey");

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone");
            entity.Property(e => e.Hrid).HasColumnName("HRId");

            entity.HasOne(d => d.Hr).WithMany(p => p.ShortLists)
                .HasForeignKey(d => d.Hrid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("ShortLists_HRId_fkey");

            entity.HasMany(d => d.Users).WithMany(p => p.ShortListsNavigation)
                .UsingEntity<Dictionary<string, object>>(
                    "ShortListCandidate",
                    r => r.HasOne<User>().WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("ShortListCandidates_UserId_fkey"),
                    l => l.HasOne<ShortList>().WithMany()
                        .HasForeignKey("ShortListId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("ShortListCandidates_ShortListId_fkey"),
                    j =>
                    {
                        j.HasKey("ShortListId", "UserId").HasName("ShortListCandidates_pkey");
                        j.ToTable("ShortListCandidates");
                    });
        });

        modelBuilder.Entity<Skill>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Skills_pkey");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Users_pkey");

            entity.HasIndex(e => e.Email, "Users_Email_key").IsUnique();

            entity.HasIndex(e => e.Phone, "Users_Phone_key").IsUnique();

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone");

            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Users_RoleId_fkey");
        });

        modelBuilder.Entity<UserRating>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("UserRatings_pkey");

            entity.Property(e => e.UserId).ValueGeneratedNever();
            entity.Property(e => e.CommunityTrust).HasPrecision(5, 2);
            entity.Property(e => e.CompetenceIndex).HasPrecision(5, 2);
            entity.Property(e => e.LastUpdated)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone");

            entity.HasOne(d => d.User).WithOne(p => p.UserRating)
                .HasForeignKey<UserRating>(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("UserRatings_UserId_fkey");
        });

        modelBuilder.Entity<UserSkill>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("UserSkills_pkey");

            entity.HasIndex(e => new { e.UserId, e.SkillId }, "UserSkills_UserId_SkillId_key").IsUnique();

            entity.Property(e => e.ConfirmationsCount).HasDefaultValue(0);

            entity.HasOne(d => d.Skill).WithMany(p => p.UserSkills)
                .HasForeignKey(d => d.SkillId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("UserSkills_SkillId_fkey");

            entity.HasOne(d => d.User).WithMany(p => p.UserSkills)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("UserSkills_UserId_fkey");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
