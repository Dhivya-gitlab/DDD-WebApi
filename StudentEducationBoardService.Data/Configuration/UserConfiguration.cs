using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StudentEducationBoardService.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudentEducationBoardService.Data.Configuration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            builder.ToTable("User");
            builder.HasKey(u => u.UserID);
            builder.Property(u => u.UserID).UseIdentityColumn(1, 1);
            builder.Property(u => u.UserRole).IsRequired();
        }
    }
}
