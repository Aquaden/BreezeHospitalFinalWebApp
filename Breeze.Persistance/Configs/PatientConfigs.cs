using Breeze.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Breeze.Persistance.Configs
{
    public class PatientConfigs : IEntityTypeConfiguration<Patients>
    {
        public void Configure(EntityTypeBuilder<Patients> builder)
        {
            builder.Property(x => x.Name).IsRequired().HasMaxLength(30);
            builder.Property(x => x.SurName).IsRequired().HasMaxLength(30);
            builder.Property(x => x.AdmissionDate).IsRequired();
            builder.Property(x => x.Number).IsRequired().HasMaxLength(15);
        }
    }
}
