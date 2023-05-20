using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace fullstackCsharp.Models;

public partial class ManagerContext : DbContext
{
    public ManagerContext()
    {
    }

    public ManagerContext(DbContextOptions<ManagerContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Allowance> Allowances { get; set; }

    public virtual DbSet<Attendane> Attendanes { get; set; }

    public virtual DbSet<Department> Departments { get; set; }

    public virtual DbSet<Payoff> Payoffs { get; set; }

    public virtual DbSet<Position> Positions { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Salary> Salaries { get; set; }

    public virtual DbSet<T> Ts { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserRole> UserRoles { get; set; }

/*    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=AdminDXQ;Initial Catalog=Manager;User ID=sa;Password=ailaai21;Integrated Security=True;TrustServerCertificate=True");
*/
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Allowance>(entity =>
        {
            entity.HasKey(e => e.IdAllowance);

            entity.ToTable("Allowance");

            entity.Property(e => e.IdAllowance).HasColumnName("Id_allowance");
            entity.Property(e => e.AllowanceAmount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.CreateMonth).HasColumnType("date");
            entity.Property(e => e.IdU).HasColumnName("Id_u");

            entity.HasOne(d => d.IdUNavigation).WithMany(p => p.Allowances)
                .HasForeignKey(d => d.IdU)
                .HasConstraintName("FK_Allowance_Users");
        });

        modelBuilder.Entity<Attendane>(entity =>
        {
            entity.HasKey(e => e.IdA);

            entity.ToTable("Attendane");

            entity.Property(e => e.IdA).HasColumnName("Id_a");
            entity.Property(e => e.AttendaneDate).HasColumnType("date");
            entity.Property(e => e.IdU).HasColumnName("Id_u");

            entity.HasOne(d => d.IdUNavigation).WithMany(p => p.Attendanes)
                .HasForeignKey(d => d.IdU)
                .HasConstraintName("FK_Attendane_Users");
        });

        modelBuilder.Entity<Department>(entity =>
        {
            entity.HasKey(e => e.IdD);

            entity.Property(e => e.IdD).HasColumnName("Id_d");
            entity.Property(e => e.Department1)
                .HasMaxLength(100)
                .HasColumnName("Department");
        });

        modelBuilder.Entity<Payoff>(entity =>
        {
            entity.HasKey(e => e.IdPay);

            entity.Property(e => e.IdPay).HasColumnName("Id_pay");
            entity.Property(e => e.Description).HasMaxLength(200);
            entity.Property(e => e.IdU).HasColumnName("Id_u");
            entity.Property(e => e.Payoff1)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("Payoff");
            entity.Property(e => e.PayoffDate).HasColumnType("date");

            entity.HasOne(d => d.IdUNavigation).WithMany(p => p.Payoffs)
                .HasForeignKey(d => d.IdU)
                .HasConstraintName("FK_Payoffs_Users");
        });

        modelBuilder.Entity<Position>(entity =>
        {
            entity.HasKey(e => e.IdPosition);

            entity.ToTable("Position");

            entity.Property(e => e.IdPosition).HasColumnName("Id_position");
            entity.Property(e => e.Coefficient)
                .HasColumnType("decimal(4, 2)")
                .HasColumnName("coefficient");
            entity.Property(e => e.Position1)
                .HasMaxLength(100)
                .HasColumnName("Position");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.IdR);

            entity.Property(e => e.IdR).HasColumnName("Id_r");
            entity.Property(e => e.RoleName).HasMaxLength(100);
        });

        modelBuilder.Entity<Salary>(entity =>
        {
            entity.HasKey(e => e.IdSalary);

            entity.ToTable("Salary");

            entity.Property(e => e.IdSalary).HasColumnName("Id_salary");
            entity.Property(e => e.BasicSalary).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.ChangeDate).HasColumnType("date");
            entity.Property(e => e.Describe)
                .HasMaxLength(500)
                .HasColumnName("describe");
            entity.Property(e => e.IdU).HasColumnName("Id_u");

            entity.HasOne(d => d.IdUNavigation).WithMany(p => p.Salaries)
                .HasForeignKey(d => d.IdU)
                .HasConstraintName("FK_Salary_Users");
        });

        modelBuilder.Entity<T>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("t");

            entity.Property(e => e.AttendaneDate).HasColumnType("date");
            entity.Property(e => e.FullName).HasMaxLength(100);
            entity.Property(e => e.IdA).HasColumnName("Id_a");
            entity.Property(e => e.IdU).HasColumnName("Id_u");
            entity.Property(e => e.Totalwork)
                .HasColumnType("decimal(22, 6)")
                .HasColumnName("totalwork");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.IdU);

            entity.Property(e => e.IdU).HasColumnName("Id_u");
            entity.Property(e => e.Adress).HasMaxLength(500);
            entity.Property(e => e.Birthday).HasColumnType("date");
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.FullName).HasMaxLength(100);
            entity.Property(e => e.Gender).HasMaxLength(50);
            entity.Property(e => e.IdCard).HasColumnType("decimal(15, 0)");
            entity.Property(e => e.IdD).HasColumnName("Id_d");
            entity.Property(e => e.IdPosition).HasColumnName("Id_position");
            entity.Property(e => e.PassWord).HasMaxLength(50);
            entity.Property(e => e.StartDate).HasColumnType("date");
            entity.Property(e => e.UserName).HasMaxLength(50);

            entity.HasOne(d => d.IdDNavigation).WithMany(p => p.Users)
                .HasForeignKey(d => d.IdD)
                .HasConstraintName("FK_Users_Departments");

            entity.HasOne(d => d.IdPositionNavigation).WithMany(p => p.Users)
                .HasForeignKey(d => d.IdPosition)
                .HasConstraintName("FK_Users_Position");
        });

        modelBuilder.Entity<UserRole>(entity =>
        {
            entity.HasKey(e => e.IdUr).HasName("PK__UserRole__014848A494720CE4");

            entity.ToTable("UserRole");

            entity.Property(e => e.IdUr).HasColumnName("id_ur");
            entity.Property(e => e.IdR).HasColumnName("Id_r");
            entity.Property(e => e.IdU).HasColumnName("Id_u");

            entity.HasOne(d => d.IdRNavigation).WithMany(p => p.UserRoles)
                .HasForeignKey(d => d.IdR)
                .HasConstraintName("FK__UserRole__Id_r__46E78A0C");

            entity.HasOne(d => d.IdUNavigation).WithMany(p => p.UserRoles)
                .HasForeignKey(d => d.IdU)
                .HasConstraintName("FK__UserRole__Id_u__45F365D3");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
