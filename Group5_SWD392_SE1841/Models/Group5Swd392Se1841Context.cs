using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Group5_SWD392_SE1841.Models;

public partial class Group5Swd392Se1841Context : DbContext
{
    public Group5Swd392Se1841Context()
    {
    }

    public Group5Swd392Se1841Context(DbContextOptions<Group5Swd392Se1841Context> options)
        : base(options)
    {
    }

    public virtual DbSet<Account> Accounts { get; set; }

    public virtual DbSet<Department> Departments { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<Master> Masters { get; set; }

    public virtual DbSet<Project> Projects { get; set; }

    public virtual DbSet<ProjectEmployee> ProjectEmployees { get; set; }

    public virtual DbSet<Task> Tasks { get; set; }

    public virtual DbSet<Timesheet> Timesheets { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=(local);Database=Group5_SWD392_SE1841;User Id=sa;Password=123;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Account>(entity =>
        {
            entity.HasKey(e => e.AccountId).HasName("PK__Account__46A222CD14A17B4C");

            entity.ToTable("Account");

            entity.HasIndex(e => e.UserName, "UQ__Account__7C9273C47B3819E7").IsUnique();

            entity.Property(e => e.AccountId).HasColumnName("account_id");
            entity.Property(e => e.AccountRoleId).HasColumnName("account_role_id");
            entity.Property(e => e.AccountStatusId).HasColumnName("account_status_id");
            entity.Property(e => e.CreatedId).HasColumnName("created_id");
            entity.Property(e => e.CreatedTime)
                .HasColumnType("datetime")
                .HasColumnName("created_time");
            entity.Property(e => e.DeleteFlg).HasColumnName("delete_flg");
            entity.Property(e => e.EmployeeId).HasColumnName("employee_id");
            entity.Property(e => e.PasswordHash)
                .HasMaxLength(60)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("password_hash");
            entity.Property(e => e.RecordNo)
                .IsRowVersion()
                .IsConcurrencyToken()
                .HasColumnName("record_no");
            entity.Property(e => e.UpdateTime)
                .HasColumnType("datetime")
                .HasColumnName("update_time");
            entity.Property(e => e.UpdatesId).HasColumnName("updates_id");
            entity.Property(e => e.UserName)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("user_name");

            entity.HasOne(d => d.Employee).WithMany(p => p.Accounts)
                .HasForeignKey(d => d.EmployeeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Account__employe__44FF419A");
        });

        modelBuilder.Entity<Department>(entity =>
        {
            entity.HasKey(e => e.DepartmentId).HasName("PK__Departme__C2232422B6C02280");

            entity.ToTable("Department");

            entity.HasIndex(e => e.DepartmentName, "UQ__Departme__226ED1571C09F9D7").IsUnique();

            entity.Property(e => e.DepartmentId).HasColumnName("department_id");
            entity.Property(e => e.CreatedId).HasColumnName("created_id");
            entity.Property(e => e.CreatedTime)
                .HasColumnType("datetime")
                .HasColumnName("created_time");
            entity.Property(e => e.DeleteFlg).HasColumnName("delete_flg");
            entity.Property(e => e.DepartmentName)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("department_name");
            entity.Property(e => e.DepartmentStatus).HasColumnName("department_status");
            entity.Property(e => e.RecordNo)
                .IsRowVersion()
                .IsConcurrencyToken()
                .HasColumnName("record_no");
            entity.Property(e => e.UpdateTime)
                .HasColumnType("datetime")
                .HasColumnName("update_time");
            entity.Property(e => e.UpdatesId).HasColumnName("updates_id");
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.EmployeeId).HasName("PK__Employee__C52E0BA89D4B904B");

            entity.ToTable("Employee");

            entity.HasIndex(e => e.EmployeeEmail, "UQ__Employee__0A874BCFAD981764").IsUnique();

            entity.Property(e => e.EmployeeId).HasColumnName("employee_id");
            entity.Property(e => e.CreatedId).HasColumnName("created_id");
            entity.Property(e => e.CreatedTime)
                .HasColumnType("datetime")
                .HasColumnName("created_time");
            entity.Property(e => e.DeleteFlg).HasColumnName("delete_flg");
            entity.Property(e => e.DepartmentId).HasColumnName("department_id");
            entity.Property(e => e.EmployeeEmail)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("employee_email");
            entity.Property(e => e.EmployeeName)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("employee_name");
            entity.Property(e => e.EmployeeStatusId).HasColumnName("employee_status_id");
            entity.Property(e => e.ManagerId).HasColumnName("manager_id");
            entity.Property(e => e.RecordNo)
                .IsRowVersion()
                .IsConcurrencyToken()
                .HasColumnName("record_no");
            entity.Property(e => e.UpdateTime)
                .HasColumnType("datetime")
                .HasColumnName("update_time");
            entity.Property(e => e.UpdatesId).HasColumnName("updates_id");

            entity.HasOne(d => d.Department).WithMany(p => p.Employees)
                .HasForeignKey(d => d.DepartmentId)
                .HasConstraintName("FK__Employee__depart__412EB0B6");

            entity.HasOne(d => d.Manager).WithMany(p => p.InverseManager)
                .HasForeignKey(d => d.ManagerId)
                .HasConstraintName("FK__Employee__manage__403A8C7D");
        });

        modelBuilder.Entity<Master>(entity =>
        {
            entity.HasKey(e => new { e.TypeName, e.TypeKey }).HasName("PK__Master__EFD3E573258274DB");

            entity.ToTable("Master");

            entity.Property(e => e.TypeName)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("type_name");
            entity.Property(e => e.TypeKey).HasColumnName("type_key");
            entity.Property(e => e.CreatedId).HasColumnName("created_id");
            entity.Property(e => e.CreatedTime)
                .HasColumnType("datetime")
                .HasColumnName("created_time");
            entity.Property(e => e.DeleteFlg).HasColumnName("delete_flg");
            entity.Property(e => e.RecordNo)
                .IsRowVersion()
                .IsConcurrencyToken()
                .HasColumnName("record_no");
            entity.Property(e => e.TypeValue)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("type_value");
            entity.Property(e => e.UpdateTime)
                .HasColumnType("datetime")
                .HasColumnName("update_time");
            entity.Property(e => e.UpdatesId).HasColumnName("updates_id");
        });

        modelBuilder.Entity<Project>(entity =>
        {
            entity.HasKey(e => e.ProjectId).HasName("PK__Project__BC799E1FDAF07FC0");

            entity.ToTable("Project");

            entity.HasIndex(e => e.ProjectName, "UQ__Project__4A0B0D6918715EA3").IsUnique();

            entity.Property(e => e.ProjectId).HasColumnName("project_id");
            entity.Property(e => e.CreatedId).HasColumnName("created_id");
            entity.Property(e => e.CreatedTime)
                .HasColumnType("datetime")
                .HasColumnName("created_time");
            entity.Property(e => e.DeleteFlg).HasColumnName("delete_flg");
            entity.Property(e => e.ProjectName)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("project_name");
            entity.Property(e => e.ProjectStatus).HasColumnName("project_status");
            entity.Property(e => e.RecordNo)
                .IsRowVersion()
                .IsConcurrencyToken()
                .HasColumnName("record_no");
            entity.Property(e => e.UpdateTime)
                .HasColumnType("datetime")
                .HasColumnName("update_time");
            entity.Property(e => e.UpdatesId).HasColumnName("updates_id");
        });

        modelBuilder.Entity<ProjectEmployee>(entity =>
        {
            entity.HasKey(e => new { e.EmployeeId, e.ProjectId, e.StartDate }).HasName("PK__ProjectE__1E4F6428BC46CC6D");

            entity.ToTable("ProjectEmployee");

            entity.Property(e => e.EmployeeId).HasColumnName("employee_id");
            entity.Property(e => e.ProjectId).HasColumnName("project_id");
            entity.Property(e => e.StartDate)
                .HasColumnType("datetime")
                .HasColumnName("start_date");
            entity.Property(e => e.CreatedId).HasColumnName("created_id");
            entity.Property(e => e.CreatedTime)
                .HasColumnType("datetime")
                .HasColumnName("created_time");
            entity.Property(e => e.DeleteFlg).HasColumnName("delete_flg");
            entity.Property(e => e.EndDate)
                .HasColumnType("datetime")
                .HasColumnName("end_date");
            entity.Property(e => e.RecordNo)
                .IsRowVersion()
                .IsConcurrencyToken()
                .HasColumnName("record_no");
            entity.Property(e => e.UpdateTime)
                .HasColumnType("datetime")
                .HasColumnName("update_time");
            entity.Property(e => e.UpdatesId).HasColumnName("updates_id");

            entity.HasOne(d => d.Employee).WithMany(p => p.ProjectEmployees)
                .HasForeignKey(d => d.EmployeeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ProjectEm__emplo__47DBAE45");

            entity.HasOne(d => d.Project).WithMany(p => p.ProjectEmployees)
                .HasForeignKey(d => d.ProjectId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ProjectEm__proje__48CFD27E");
        });

        modelBuilder.Entity<Task>(entity =>
        {
            entity.HasKey(e => e.TaskId).HasName("PK__Task__0492148D89682A13");

            entity.ToTable("Task");

            entity.HasIndex(e => e.TaskName, "UQ__Task__699065967E7EBA83").IsUnique();

            entity.Property(e => e.TaskId).HasColumnName("task_id");
            entity.Property(e => e.CreatedId).HasColumnName("created_id");
            entity.Property(e => e.CreatedTime)
                .HasColumnType("datetime")
                .HasColumnName("created_time");
            entity.Property(e => e.DeleteFlg).HasColumnName("delete_flg");
            entity.Property(e => e.ProjectId).HasColumnName("project_id");
            entity.Property(e => e.RecordNo)
                .IsRowVersion()
                .IsConcurrencyToken()
                .HasColumnName("record_no");
            entity.Property(e => e.TaskName)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("task_name");
            entity.Property(e => e.TaskStatusId).HasColumnName("task_status_id");
            entity.Property(e => e.UpdateTime)
                .HasColumnType("datetime")
                .HasColumnName("update_time");
            entity.Property(e => e.UpdatesId).HasColumnName("updates_id");

            entity.HasOne(d => d.Project).WithMany(p => p.Tasks)
                .HasForeignKey(d => d.ProjectId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Task__project_id__4CA06362");
        });

        modelBuilder.Entity<Timesheet>(entity =>
        {
            entity.HasKey(e => e.TimeSheetId).HasName("PK__Timeshee__708A743BF40334E0");

            entity.ToTable("Timesheet");

            entity.Property(e => e.TimeSheetId).HasColumnName("time_sheet_id");
            entity.Property(e => e.CreatedId).HasColumnName("created_id");
            entity.Property(e => e.CreatedTime)
                .HasColumnType("datetime")
                .HasColumnName("created_time");
            entity.Property(e => e.DeleteFlg).HasColumnName("delete_flg");
            entity.Property(e => e.EmployeeId).HasColumnName("employee_id");
            entity.Property(e => e.EndTime)
                .HasColumnType("datetime")
                .HasColumnName("end_time");
            entity.Property(e => e.Notes)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("notes");
            entity.Property(e => e.RecordNo)
                .IsRowVersion()
                .IsConcurrencyToken()
                .HasColumnName("record_no");
            entity.Property(e => e.StartTime)
                .HasColumnType("datetime")
                .HasColumnName("start_time");
            entity.Property(e => e.TaskId).HasColumnName("task_id");
            entity.Property(e => e.UpdateTime)
                .HasColumnType("datetime")
                .HasColumnName("update_time");
            entity.Property(e => e.UpdatesId).HasColumnName("updates_id");
            entity.Property(e => e.WorkStatusId).HasColumnName("work_status_id");

            entity.HasOne(d => d.Employee).WithMany(p => p.Timesheets)
                .HasForeignKey(d => d.EmployeeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Timesheet__emplo__4F7CD00D");

            entity.HasOne(d => d.Task).WithMany(p => p.Timesheets)
                .HasForeignKey(d => d.TaskId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Timesheet__task___5070F446");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
