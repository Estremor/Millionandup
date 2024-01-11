using Microsoft.EntityFrameworkCore;
using Millionandup.PropertyManagement.Infrastructure.Base;
using Millionandup.PropertyManagement.Infrastructure.DataPersistence.EntitiesConfig;

namespace Millionandup.PropertyManagement.Infrastructure.DataPersistence
{

    public partial class MillionPropertyDbContext : DbContextBase
    {
        #region C'tor
        /// <summary>
        /// Inicia el contexto de Datos
        /// </summary>
        /// <param name="dbSettings"></param>
        public MillionPropertyDbContext(DbSettings dbSettings) : base(dbSettings)
        {
            DbSettings.ConnectionString = dbSettings.ConnectionString;
            Database.EnsureCreated();
        }
        #endregion

        #region Methods
        /// <summary>
        /// Se usa la configuracion 
        /// </summary>
        /// <param name="dbContextOptionsBuilder"></param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(DbSettings.ConnectionString);
            }
        }

        /// <summary>
        /// Aplica la configuracion
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.ApplyConfiguration(new OwnerConfiguration());
            modelBuilder.ApplyConfiguration(new PropertyConfiguration());
            modelBuilder.ApplyConfiguration(new PropertyImageConfiguration());
            modelBuilder.ApplyConfiguration(new PropertyTraceConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());

            base.OnModelCreating(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
        #endregion
    }
}
