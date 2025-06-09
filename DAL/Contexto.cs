using Michael_Jose_AP1_P1.Models;
using Microsoft.EntityFrameworkCore;

namespace Michael_Jose_AP1_P1.DAL
{
    public class Contexto(DbContextOptions<Contexto> options) : DbContext(options)
    {
        public DbSet<Registro> Registros { get; set; }
    }
}
