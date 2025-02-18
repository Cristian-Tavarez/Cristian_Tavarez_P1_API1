using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Cristian_Tavarez_P1_API1.Models;
using Cristian_Tavarez_P1_API1.DAL;
using Cristian_Tavarez_P1_API1.Components.Pages;
namespace Cristian_Tavarez_P1_API1.Services
{
    public class AporteServices(IDbContextFactory<Contexto> Dbfactory)
    {
        public async Task<bool> Guardar(Aporte aporte)
        {
            if (await Existe(aporte.AporteId))
                return await Modificar(aporte);
            else
                return await Insert(aporte);
        }

        public async Task<bool> Existe(int id)
        {
            await using var contexto = await Dbfactory.CreateDbContextAsync();
            return await contexto.Aporte.AnyAsync(i => i.AporteId == id);
        }

        public async Task<bool> Insert(Aporte modelo)
        {
            await using var contexto = await Dbfactory.CreateDbContextAsync();
            contexto.Aporte.Add(modelo);
            return await contexto.SaveChangesAsync() > 0;
        }

        public async Task<bool> Modificar(Aporte modelo)
        {
            await using var contexto = await Dbfactory.CreateDbContextAsync();
            contexto.Entry(modelo).State = EntityState.Modified;
            return await contexto.SaveChangesAsync() > 0;
        }

        public async Task<Aporte?> Buscar(int id)
        {
            await using var contexto = await Dbfactory.CreateDbContextAsync();
            return await contexto.Aporte.FirstOrDefaultAsync(i => i.AporteId == id);
        }

        public async Task<bool> Eliminar(int id)
        {
            await using var contexto = await Dbfactory.CreateDbContextAsync();
            return await contexto.Aporte
                .AsNoTracking()
                .Where(i => i.AporteId == id)
                .ExecuteDeleteAsync() > 0;
        }

        public async Task<List<Aporte>> Listar(Expression<Func<Aporte, bool>> criterio)
        {
            await using var contexto = await Dbfactory.CreateDbContextAsync();
            return await contexto.Aporte
                .Where(criterio)
                .AsNoTracking()
                .ToListAsync();
        }
    }
}

