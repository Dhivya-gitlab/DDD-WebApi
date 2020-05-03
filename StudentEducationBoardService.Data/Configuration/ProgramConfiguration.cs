using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StudentEducationBoardService.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudentEducationBoardService.Data.Configuration
{
    public class ProgramConfiguration : IEntityTypeConfiguration<Program>
    {
        public void Configure(EntityTypeBuilder<Program> builder)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            builder.ToTable("Program");
            builder.HasKey(p => p.ProgramID);
            builder.Property(p => p.ProgramID).UseIdentityColumn(1, 1);
            builder.Property(p => p.ProgramName).IsRequired();
        }
    }
}
