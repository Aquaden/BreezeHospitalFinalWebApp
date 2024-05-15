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
    public class DoctorOperationsConfigs : IEntityTypeConfiguration<DoctorOperations>
    {
        public void Configure(EntityTypeBuilder<DoctorOperations> builder)
        {
            
            builder.Property(x => x.DoctorId).IsRequired();
            builder.Property(x => x.OperationId).IsRequired();
            

        }
    }
}
