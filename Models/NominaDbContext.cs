using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Nómina.Models;

public partial class NominaDbContext : DbContext
{
    public NominaDbContext()
    {
    }

    public NominaDbContext(DbContextOptions<NominaDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Department> Departments { get; set; }

    public virtual DbSet<DeptEmp> DeptEmps { get; set; }

    public virtual DbSet<DeptManager> DeptManagers { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<LogAuditoriaSalario> LogAuditoriaSalarios { get; set; }

    public virtual DbSet<Salary> Salaries { get; set; }

    public virtual DbSet<Title> Titles { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=localhost;Database=NominaDB;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Department>(entity =>
        {
            entity.HasKey(e => e.DeptNo).HasName("PK__departme__DCA63FA6C6393056");

            entity.ToTable("departments");

            entity.Property(e => e.DeptNo)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("dept_no");
            entity.Property(e => e.Activo)
                .HasDefaultValue(true)
                .HasColumnName("activo");
            entity.Property(e => e.DeptName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("dept_name");
        });

        modelBuilder.Entity<DeptEmp>(entity =>
        {
            entity.HasKey(e => new { e.EmpNo, e.DeptNo, e.FromDate }).HasName("PK__dept_emp__F58C769A504BC37D");

            entity.ToTable("dept_emp");

            entity.Property(e => e.EmpNo).HasColumnName("emp_no");
            entity.Property(e => e.DeptNo)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("dept_no");
            entity.Property(e => e.FromDate).HasColumnName("from_date");
            entity.Property(e => e.ToDate).HasColumnName("to_date");

            entity.HasOne(d => d.DeptNoNavigation).WithMany(p => p.DeptEmps)
                .HasForeignKey(d => d.DeptNo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__dept_emp__dept_n__412EB0B6");

            entity.HasOne(d => d.EmpNoNavigation).WithMany(p => p.DeptEmps)
                .HasForeignKey(d => d.EmpNo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__dept_emp__emp_no__403A8C7D");
        });

        modelBuilder.Entity<DeptManager>(entity =>
        {
            entity.HasKey(e => new { e.EmpNo, e.DeptNo, e.FromDate }).HasName("PK__dept_man__F58C769A469F3961");

            entity.ToTable("dept_manager");

            entity.Property(e => e.EmpNo).HasColumnName("emp_no");
            entity.Property(e => e.DeptNo)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("dept_no");
            entity.Property(e => e.FromDate).HasColumnName("from_date");
            entity.Property(e => e.ToDate).HasColumnName("to_date");

            entity.HasOne(d => d.DeptNoNavigation).WithMany(p => p.DeptManagers)
                .HasForeignKey(d => d.DeptNo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__dept_mana__dept___44FF419A");

            entity.HasOne(d => d.EmpNoNavigation).WithMany(p => p.DeptManagers)
                .HasForeignKey(d => d.EmpNo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__dept_mana__emp_n__440B1D61");
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.EmpNo).HasName("PK__employee__129850FA25E841CD");

            entity.ToTable("employees");

            entity.HasIndex(e => e.Correo, "UQ__employee__2A586E0B8F03675F").IsUnique();

            entity.HasIndex(e => e.Ci, "UQ__employee__32136662B1C1714E").IsUnique();

            entity.Property(e => e.EmpNo)
                .ValueGeneratedNever()
                .HasColumnName("emp_no");
            entity.Property(e => e.Activo)
                .HasDefaultValue(true)
                .HasColumnName("activo");
            entity.Property(e => e.BirthDate).HasColumnName("birth_date");
            entity.Property(e => e.Ci)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("ci");
            entity.Property(e => e.Correo)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("correo");
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("first_name");
            entity.Property(e => e.Gender)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("gender");
            entity.Property(e => e.HireDate).HasColumnName("hire_date");
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("last_name");
        });

        modelBuilder.Entity<LogAuditoriaSalario>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Log_Audi__3213E83F65CBC87A");

            entity.ToTable("Log_AuditoriaSalarios");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.DetalleCambio)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.EmpNo).HasColumnName("emp_no");
            entity.Property(e => e.FechaActualizacion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fechaActualizacion");
            entity.Property(e => e.Salario)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("salario");
            entity.Property(e => e.Usuario)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("usuario");

            entity.HasOne(d => d.EmpNoNavigation).WithMany(p => p.LogAuditoriaSalarios)
                .HasForeignKey(d => d.EmpNo)
                .HasConstraintName("FK__Log_Audit__emp_n__5165187F");
        });

        modelBuilder.Entity<Salary>(entity =>
        {
            entity.HasKey(e => new { e.EmpNo, e.FromDate }).HasName("PK__salaries__BF7C09539391B9F2");

            entity.ToTable("salaries");

            entity.Property(e => e.EmpNo).HasColumnName("emp_no");
            entity.Property(e => e.FromDate).HasColumnName("from_date");
            entity.Property(e => e.Salary1)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("salary");
            entity.Property(e => e.ToDate).HasColumnName("to_date");

            entity.HasOne(d => d.EmpNoNavigation).WithMany(p => p.Salaries)
                .HasForeignKey(d => d.EmpNo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__salaries__emp_no__4AB81AF0");
        });

        modelBuilder.Entity<Title>(entity =>
        {
            entity.HasKey(e => new { e.EmpNo, e.Title1, e.FromDate }).HasName("PK__titles__B614B4DBFFBACABE");

            entity.ToTable("titles");

            entity.Property(e => e.EmpNo).HasColumnName("emp_no");
            entity.Property(e => e.Title1)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("title");
            entity.Property(e => e.FromDate).HasColumnName("from_date");
            entity.Property(e => e.ToDate).HasColumnName("to_date");

            entity.HasOne(d => d.EmpNoNavigation).WithMany(p => p.Titles)
                .HasForeignKey(d => d.EmpNo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__titles__emp_no__47DBAE45");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Usuario).HasName("PK__users__9AFF8FC72FD37375");

            entity.ToTable("users");

            entity.Property(e => e.Usuario)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("usuario");
            entity.Property(e => e.Clave)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("clave");
            entity.Property(e => e.EmpNo).HasColumnName("emp_no");
            entity.Property(e => e.Rol)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("rol");

            entity.HasOne(d => d.EmpNoNavigation).WithMany(p => p.Users)
                .HasForeignKey(d => d.EmpNo)
                .HasConstraintName("FK__users__emp_no__4D94879B");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
