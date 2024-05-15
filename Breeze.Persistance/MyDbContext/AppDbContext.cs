using Breeze.Domain.Entities;
using Breeze.Domain.Entities.Identities;
using Breeze.Persistance.Configs;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Breeze.Persistance.MyDbContext
{
    public class AppDbContext : IdentityDbContext<AppUser,AppRole,string>
    {
        public DbSet<Doctors> Doctors { get; set; }
        public DbSet<Patients> Patients { get; set; }
        public DbSet<Operations> Operations { get; set; }
        public DbSet<Analyses> Analyses { get; set; }
        public DbSet<DoctorOperations> DoctorOperations { get; set; }
        public DbSet<DoctorPatients> DoctorPatients { get; set; }
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(typeof(DoctorConfigs).Assembly);
            builder.ApplyConfigurationsFromAssembly(typeof(PatientConfigs).Assembly);
            //Operation ve Patient arasinda one-to-many elaqe
            builder.Entity<Operations>()
            .HasOne(o => o.Patient)
            .WithMany(p => p.Operation)
            .HasForeignKey(o => o.PatientId)
            .IsRequired();

            

            builder.Entity<DoctorPatients>()
                .HasOne(dp => dp.Doctor)
                .WithMany(d => d.DoctorsPatients)
            .HasForeignKey(dp => dp.DoctorId);

            builder.Entity<DoctorPatients>()
                .HasOne(dp => dp.Patient)
                .WithMany(p => p.DoctorsPatients)
                .HasForeignKey(dp => dp.PatientId);
            //-------------------------

            builder.Entity<Analyses>()
            .HasOne(a => a.Patient)
            .WithMany(p => p.PatientAnalyses)
            .HasForeignKey(a => a.PatientId);
            //---------------------------

            

            builder.Entity<DoctorOperations>()
                .HasOne(dp => dp.Doctor)
                .WithMany(d => d.DoctorsOperations)
            .HasForeignKey(dp => dp.DoctorId);

            builder.Entity<DoctorOperations>()
                .HasOne(dp => dp.Operation)
                .WithMany(p => p.DoctorsOperations)
                .HasForeignKey(dp => dp.OperationId);
        }
    }
}


