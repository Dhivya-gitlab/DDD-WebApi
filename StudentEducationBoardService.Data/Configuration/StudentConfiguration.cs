using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using StudentEducationBoardService.Domain.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace StudentEducationBoardService.Data.Configuration
{
    public class StudentConfiguration : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            builder.ToTable("Student");
            builder.HasKey(s => s.StudentID);
            builder.Property(s => s.StudentID).UseIdentityColumn(1, 1);
            builder.Property(s => s.Name).IsRequired();
        }
    }
}
