using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using StudentEducationBoardService.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace StudentEducationBoardService.Data
{
   public class StudentEducationBoardDbContext :DbContext
    {

        public DbSet<School> Schools { get; set; }


        public StudentEducationBoardDbContext(DbContextOptions<StudentEducationBoardDbContext> options)
           : base(options)
        {
        }

        //public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
        //{
        //    //ChangeTracker.ApplyValueGenerationOnUpdate();
        //    return base.SaveChangesAsync(cancellationToken);
        //}

        //public override int SaveChanges()
        //{
        //    //ChangeTracker.ApplyValueGenerationOnUpdate();
        //    return base.SaveChanges();
        //}

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    if (modelBuilder == null)
        //    {
        //        throw new ArgumentNullException(nameof(modelBuilder));
        //    }

        //    base.OnModelCreating(modelBuilder);
        //    modelBuilder.ApplyConfigurationsFromAssembly(typeof(EmployeeConfiguration).Assembly);
        //    modelBuilder.ApplyBaseEntityConfiguration(); // This should be called after calling the devied entity configurations
        //}

        //public void ApplyValueGenerationOnUpdate(this ChangeTracker changeTracker)
        //{
        //    if (changeTracker == null)
        //    {
        //        throw new ArgumentNullException(nameof(changeTracker));
        //    }

        //    var modifiedEntries = changeTracker.Entries<BaseEntity>().Where(e => e.State == EntityState.Modified).ToList();

        //    if (modifiedEntries.Any())
        //    {
        //        foreach (EntityEntry<BaseEntity> entityEntry in modifiedEntries)
        //        {
        //            bool hasAnyPropertyValueModified = entityEntry.Properties.Any(p => p.IsModified);

        //            if (hasAnyPropertyValueModified)
        //            {
        //                bool isMenuallySet = entityEntry.Property(p => p.LastModifiedAtUtc).IsModified;
        //                if (isMenuallySet)
        //                {
        //                    throw new ApplicationException("You can't set the value for LastModifiedAtUtc property. The value of this property will be set by system.");
        //                }

        //                entityEntry.Property(p => p.LastModifiedAtUtc).CurrentValue = DateTime.UtcNow;
        //            }
        //        }
        //    }
        //}
    }
}
