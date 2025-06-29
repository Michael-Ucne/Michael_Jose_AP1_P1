﻿using Michael_Jose_AP1_P1.DAL;
using Michael_Jose_AP1_P1.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Michael_Jose_AP1_P1.Services
{
    public class AportesServices(IDbContextFactory<Contexto> DbFactory)
    {
        private async Task<bool> Insertar(Aportes aporte)
        {
            await using var contexto = await DbFactory.CreateDbContextAsync();
            contexto.Aportes.Add(aporte);
            return await contexto.SaveChangesAsync() > 0;
        }

        private async Task<bool> Modificar(Aportes aporte)
        {
            await using var contexto = await DbFactory.CreateDbContextAsync();
            contexto.Update(aporte);
            return await contexto.SaveChangesAsync() > 0;
        }

        private async Task<bool> Existe(int aporteId)
        {
            await using var contexto = await DbFactory.CreateDbContextAsync();
            return await contexto.Aportes
                .AnyAsync(t => t.AporteId == aporteId);
        }

        public async Task<bool> Guardar(Aportes aporte)
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

        public async Task<Aportes?> Buscar(int aporteId)
        {
            await using var contexto = await DbFactory.CreateDbContextAsync();
            return await contexto.Aportes
                .FirstOrDefaultAsync(t => t.AporteId == aporteId);
        }

        public async Task<bool> Eliminar(int aporteId)
        {
            await using var contexto = await DbFactory.CreateDbContextAsync();
            return await contexto.Aportes
                .AsNoTracking()
                .Where(t => t.AporteId == aporteId)
                .ExecuteDeleteAsync() > 0;
        }

        public async Task<List<Aportes>> Listar(Expression<Func<Aportes, bool>> criterio)
        {
            await using var contexto = await DbFactory.CreateDbContextAsync();
            return await contexto.Aportes
                .AsNoTracking()
                .Where(criterio)
                .ToListAsync();
        }
    }
}
