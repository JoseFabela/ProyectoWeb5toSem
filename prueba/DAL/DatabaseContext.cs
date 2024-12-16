using Microsoft.EntityFrameworkCore;
using prueba.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;

namespace prueba.DAL
{
    public class DatabaseContext : DbContext
    {

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
        }

        public DbSet<descuento> descuentos { get; set; }
        public DbSet<AperturaCaja> AperturaCaja { get; set; }

		public DbSet<Empleado> Empleados { get; set; }
        public DbSet<cliente> clientes { get; set; }
        public DbSet<ClientePotencial> ClientePotencial { get; set; }
        public DbSet<ClienteVip> ClienteVip { get; set; }
        public DbSet<Jugador> Jugador { get; set; } // Agrega esta línea para incluir Jugador
        public DbSet<SalaVip> SalaVip { get; set; } // Agrega esta línea para incluir Jugador

        public DbSet<AlquilerSala> AlquilerSala { get; set; } // Agrega esta línea para incluir Jugador
        public DbSet<CamaraSeguridad> CamaraSeguridad { get; set; } // Agrega esta línea para incluir Jugador
        public DbSet<OfertaPublicitaria> OfertaPublicitaria{ get; set; } // Agrega esta línea para incluir Jugador        public DbSet<CierreCaja> CierreCaja{ get; set; } // Agrega esta línea para incluir Jugador
        public DbSet<CierreCaja> CierreCaja { get; set; } // Agrega esta línea para incluir Jugador        public DbSet<CierreCaja> CierreCaja{ get; set; } // Agrega esta línea para incluir Jugador
        public DbSet<Pago> Pago { get; set; } // Agrega esta línea para incluir Jugador        public DbSet<CierreCaja> CierreCaja{ get; set; } // Agrega esta línea para incluir Jugador
        public DbSet<ProductosBar> ProductosBar { get; set; }
        public DbSet<Equipo> Equipo { get; set; }
        public DbSet<HorarioAtencion> HorarioAtencion { get; set; }

        public DbSet<Licencias> Licencias { get; set; }



    }
}
