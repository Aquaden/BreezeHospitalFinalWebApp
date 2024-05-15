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
    public class DoctorPatientsConfigs : IEntityTypeConfiguration<DoctorPatients>
    {
        public void Configure(EntityTypeBuilder<DoctorPatients> builder)
        {
            builder.Property(x => x.PatientId).IsRequired();
            builder.Property(x => x.DoctorId).IsRequired();

        }
    }
}
