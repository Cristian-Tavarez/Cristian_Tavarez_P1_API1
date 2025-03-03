using Cristian_Tavarez_P1_API1.DAL;
using Cristian_Tavarez_P1_API1.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Cristian_Tavarez_P1_API1.Services
{
    public class AporteService
    {
        private readonly IDbContextFactory<Contexto> _dbFactory;

        public AporteService(IDbContextFactory<Contexto> dbFactory)
        {
            _dbFactory = dbFactory;
        }

        public async Task<bool> Guardar(Aporte aporte)
        {
            return await Existe(aporte.AporteId) ? await Modificar(aporte) : await Insert(aporte);
        }

        public async Task<bool> Existe(int id)
        {
            await using var contexto = await _dbFactory.CreateDbContextAsync();
            return await contexto.Aporte.AnyAsync(i => i.AporteId == id);
        }

        public async Task<bool> Insert(Aporte modelo)
        {
            await using var contexto = await _dbFactory.CreateDbContextAsync();
            contexto.Aporte.Add(modelo);
            return await contexto.SaveChangesAsync() > 0;
        }

        public async Task<bool> Modificar(Aporte modelo)
        {
            await using var contexto = await _dbFactory.CreateDbContextAsync();
            contexto.Entry(modelo).State = EntityState.Modified;
            return await contexto.SaveChangesAsync() > 0;
        }

        public async Task<Aporte> Buscar(int id)
        {
            await using var contexto = await _dbFactory.CreateDbContextAsync();
            return await contexto.Aporte.FirstOrDefaultAsync(i => i.AporteId == id) ?? new Aporte();
        }

        public async Task<bool> Eliminar(int id)
        {
            await using var contexto = await _dbFactory.CreateDbContextAsync();
            var entity = await contexto.Aporte.FindAsync(id);
            if (entity != null)
            {
                contexto.Aporte.Remove(entity);
                return await contexto.SaveChangesAsync() > 0;
            }
            return false;
        }

        public async Task<List<Aporte>> Listar(Expression<Func<Aporte, bool>>? criterio = null)
        {
            await using var contexto = await _dbFactory.CreateDbContextAsync();
            IQueryable<Aporte> query = contexto.Aporte.AsNoTracking();

            if (criterio != null)
            {
                query = query.Where(criterio);
            }

            return await query.ToListAsync();
        }
    }
}
