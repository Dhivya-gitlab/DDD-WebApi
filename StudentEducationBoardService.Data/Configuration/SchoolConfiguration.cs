using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StudentEducationBoardService.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudentEducationBoardService.Data.Configuration
{
    public class SchoolConfiguration : IEntityTypeConfiguration<School>
    {
        public void Configure(EntityTypeBuilder<School> builder)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            builder.ToTable("School");
            builder.HasKey(s => s.SchoolId);
            builder.Property(s => s.SchoolId).UseIdentityColumn(1, 1);
            builder.Property(s => s.SchoolName).IsRequired();
        }
    }
}
