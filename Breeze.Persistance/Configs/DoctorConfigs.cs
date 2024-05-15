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
    public class DoctorConfigs : IEntityTypeConfiguration<Doctors>
    {
        public void Configure(EntityTypeBuilder<Doctors> builder)
        {
            builder.Property(x => x.Name).IsRequired().HasMaxLength(30);
            builder.Property(x => x.SurName).IsRequired().HasMaxLength(30);
            builder.Property(x => x.Specialization).IsRequired().HasMaxLength(35);
            builder.Property(x => x.Email).IsRequired().HasMaxLength(50);
        }
    }
}
