using Michael_Jose_AP1_P1.DAL;
using Michael_Jose_AP1_P1.Models;
using Microsoft.EntityFrameworkCore;

namespace Michael_Jose_AP1_P1.Services
{
    public class RegistroServices(IDbContextFactory<Contexto> DbFactory)
    {
        private async Task<bool> Insertar(Registro aporte)
        {
            await using var contexto = await DbFactory.CreateDbContextAsync();
            contexto.Registros.Add(aporte);
            return await contexto.SaveChangesAsync() > 0;
        }

    }
}
