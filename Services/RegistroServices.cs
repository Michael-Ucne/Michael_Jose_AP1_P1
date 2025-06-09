using Michael_Jose_AP1_P1.DAL;
using Michael_Jose_AP1_P1.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

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

        private async Task<bool> Modificar(Registro aporte)
        {
            await using var contexto = await DbFactory.CreateDbContextAsync();
            contexto.Update(aporte);
            return await contexto.SaveChangesAsync() > 0;
        }

        private async Task<bool> Existe(int aporteId)
        {
            await using var contexto = await DbFactory.CreateDbContextAsync();
            return await contexto.Registros
                .AnyAsync(t => t.AporteId == aporteId);
        }

        public async Task<bool> Guardar(Registro aporte)
        {
            aporte.AporteId = aporte.AporteId;
            if (!await Existe(aporte.AporteId))
            {
                return await Insertar(aporte);
            }
            else
            {
                return await Modificar(aporte);
            }
        }

        public async Task<Registro?> Buscar(int aporteId)
        {
            await using var contexto = await DbFactory.CreateDbContextAsync();
            return await contexto.Registros
                .FirstOrDefaultAsync(t => t.AporteId == aporteId);
        }

        public async Task<bool> Eliminar(int aporteId)
        {
            await using var contexto = await DbFactory.CreateDbContextAsync();
            return await contexto.Registros
                .AsNoTracking()
                .Where(t => t.AporteId == aporteId)
                .ExecuteDeleteAsync() > 0;
        }

        public async Task<List<Registro>> Listar(Expression<Func<Registro, bool>> criterio)
        {
            await using var contexto = await DbFactory.CreateDbContextAsync();
            return await contexto.Registros
                .AsNoTracking()
                .Where(criterio)
                .ToListAsync();
        }
    }
}
