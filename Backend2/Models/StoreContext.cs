using Microsoft.EntityFrameworkCore;

namespace Backend2.Models
{
    /**
     Es el nucleo intermedio entre las clases, modelos y bases de datos
     tiene la cadena de conexión y configuraciones
     Hereda de DbContext, que proviene de EntityFramework
     public StoreContext(DbContextOptions<StoreContext> options) : base(options) -> Se obtiene propiedad para trabajar con las entidades

     Representa al núcleo, es decir que entidades van a la base de datos
     */
    public class StoreContext : DbContext
    {

        public StoreContext(DbContextOptions<StoreContext> options) : base(options) { 
            
        }



        public DbSet<Beer> Beer { get; set; }
        public DbSet<Brand> Brand { get; set; }

    }
}
