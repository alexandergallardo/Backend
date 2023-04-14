using Backend.Converters;
using Backend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Backend.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            
        }

        protected override void ConfigureConventions(ModelConfigurationBuilder builder)
        {
            /*builder.Properties<DateOnly>()
                    .HaveConversion<DateOnlyConverter, DateOnlyComparer>()
                    .HaveColumnType("date");*/
            
            builder.Properties<DateOnly>()
                .HaveConversion<DateOnlyConverter>()
                .HaveColumnType("date");
            base.ConfigureConventions(builder);
        }  

        public DbSet<Producto> Productos { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<OrdenCompra> OrdenCompras { get; set; }
        public DbSet<OrdenCompraItem> OrdenCompraItems { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }

    }

   /* public class DateOnlyConverter : ValueConverter<DateOnly, DateTime>
    {
        public DateOnlyConverter() : base(
                dateOnly => dateOnly.ToDateTime(TimeOnly.MinValue),
                dateTime => DateOnly.FromDateTime(dateTime))
        {
        }
    }

    public class DateOnlyComparer : ValueComparer<DateOnly>
    {
        public DateOnlyComparer() : base(
            (d1, d2) => d1.DayNumber == d2.DayNumber,
            d => d.GetHashCode())
        {
        }
    }*/
}