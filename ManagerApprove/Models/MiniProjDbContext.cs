using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ManagerApprove.Models;

public partial class MiniProjDbContext : DbContext
{
    public MiniProjDbContext()
    {
    }

    public MiniProjDbContext(DbContextOptions<MiniProjDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<DataAudit> DataAudits { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<Timesheet> Timesheets { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("server=127.0.0.1,1455;database=MiniProjDb;uid=sa;pwd=Password#123;TrustServerCertificate=Yes;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<DataAudit>(entity =>
        {
            entity.HasKey(e => e.DataId);

            entity.ToTable("DataAudit");

            entity.Property(e => e.DataId).ValueGeneratedNever();
            entity.Property(e => e.Column).HasMaxLength(255);
            entity.Property(e => e.Created)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.CurrentData).HasMaxLength(255);
            entity.Property(e => e.LastData).HasMaxLength(255);
            entity.Property(e => e.Status).HasMaxLength(255);
            entity.Property(e => e.Table).HasMaxLength(255);
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");
            entity.Property(e => e.Name).HasMaxLength(255);
        });

        modelBuilder.Entity<Timesheet>(entity =>
        {
            entity.ToTable(tb => tb.HasTrigger("trg_Timesheets_Update"));

            entity.HasIndex(e => e.EmployeeId, "IX_Timesheets_EmployeeID");

            entity.Property(e => e.TimesheetId).HasColumnName("TimesheetID");
            entity.Property(e => e.Date).HasColumnType("datetime");
            entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");
            entity.Property(e => e.HoursWorked).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.Status).HasMaxLength(15);

            entity.HasOne(d => d.Employee).WithMany(p => p.Timesheets)
                .HasForeignKey(d => d.EmployeeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Timesheets_Employees");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
