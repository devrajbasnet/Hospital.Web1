using Hospital.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Hospital.Repositories
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        // Define DbSets for  models
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Bill> Bills { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<HospitalInfo> HospitalInfos { get; set; }
        public DbSet<Insurance> Insurances { get; set; }
        public DbSet<Lab> Labs { get; set; }
        public DbSet<Medicine> Medicines { get; set; }
        public DbSet<MedicineReport> MedicineReports { get; set; }
        public DbSet<Payroll> Payrolls { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<PrescribeMedicine> PrescribeMedicines { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<TestPrice> TestPrices { get; set; }

        public DbSet<PatientReport> PatientReport {  get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuring decimal properties with precision and scale
            modelBuilder.Entity<Bill>()
                .Property(b => b.Advance)
                .HasColumnType("decimal(18, 2)");

            modelBuilder.Entity<Bill>()
                .Property(b => b.MedicineCharge)
                .HasColumnType("decimal(18, 2)");

            modelBuilder.Entity<Bill>()
                .Property(b => b.OperationCharge)
                .HasColumnType("decimal(18, 2)");

            modelBuilder.Entity<Bill>()
                .Property(b => b.RoomCharge)
                .HasColumnType("decimal(18, 2)");

            modelBuilder.Entity<Bill>()
                .Property(b => b.TotalBill)
                .HasColumnType("decimal(18, 2)");

            modelBuilder.Entity<Medicine>()
                .Property(m => m.Cost)
                .HasColumnType("decimal(18, 2)");

            modelBuilder.Entity<Payroll>()
                .Property(p => p.BonusSalary)
                .HasColumnType("decimal(18, 2)");

            modelBuilder.Entity<Payroll>()
                .Property(p => p.Compensation)
                .HasColumnType("decimal(18, 2)");

            modelBuilder.Entity<Payroll>()
                .Property(p => p.HourlySalary)
                .HasColumnType("decimal(18, 2)");

            modelBuilder.Entity<Payroll>()
                .Property(p => p.NetSalary)
                .HasColumnType("decimal(18, 2)");

            modelBuilder.Entity<Payroll>()
                .Property(p => p.Salary)
                .HasColumnType("decimal(18, 2)");

            modelBuilder.Entity<TestPrice>()
                .Property(t => t.Price)
                .HasColumnType("decimal(18, 2)");


       modelBuilder.Entity<Appointment>()
     .HasOne(a => a.Patient)
     .WithMany(u => u.Appointments) // Assuming you have this collection in ApplicationUser
     .HasForeignKey(a => a.PatientId)
     .OnDelete(DeleteBehavior.Restrict); // Adjust the deletion behavior as needed

            // Configure the DoctorName relationship with restricted delete behavior
            //modelBuilder.Entity<PatientReport>()
            //    .HasOne(pr => pr.DoctorName)
            //    .WithMany()
            //    .HasForeignKey(pr => pr.DoctorId)
            //    .OnDelete(DeleteBehavior.Restrict);
        }
    }
}

