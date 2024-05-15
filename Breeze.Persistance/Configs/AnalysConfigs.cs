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
    internal class AnalysConfigs : IEntityTypeConfiguration<Analyses>
    {
        public void Configure(EntityTypeBuilder<Analyses> builder)
        {
            builder.Property(x => x.Name).IsRequired().HasMaxLength(30);
            builder.Property(x => x.PatientId).IsRequired();
            builder.Property(x => x.Date).IsRequired();
        }
    }
}
