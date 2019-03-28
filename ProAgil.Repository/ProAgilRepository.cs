using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProAgil.Domain;

namespace ProAgil.Repository
{
    public class ProAgilRepository : IProAgilRepository
    {
        private readonly ProAgilContext _context;

        public ProAgilRepository(ProAgilContext context)
        {
            _context = context;
            _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;//não quero que toda vez seja uma mudança rastreavel
        }

        //gerais
        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }

        public void Update<T>(T entity) where T : class
        {
            _context.Update(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync()) > 0;
        }           

        //evento 

        public async Task<Evento[]> GetAllEventoAsync(bool includePalestrantes = false)
        {
            IQueryable<Evento> query = _context.Eventos.Include(c => c.Lotes).Include(c => c.RedesSociais);

            if(includePalestrantes){//se passei valor verdadeiro
                query = query.Include(pe => pe.PalestranteEventos).ThenInclude(p => p.Palestrante);
            } 

            query = query.AsNoTracking().OrderByDescending(c => c.DataEvento);//pega os que foram adicionados por último

            return await query.ToArrayAsync();
        }

        public async Task<Evento[]> GetAllEventoAsyncByTema(string tema, bool includePalestrantes)
        {
            IQueryable<Evento> query = _context.Eventos.Include(c => c.Lotes).Include(c => c.RedesSociais);

            if(includePalestrantes){//se passei valor verdadeiro
                query = query.Include(pe => pe.PalestranteEventos).ThenInclude(p => p.Palestrante);
            } 

            query = query.AsNoTracking().OrderByDescending(c => c.DataEvento).Where(c => c.Tema.ToLower().Contains(tema.ToLower()));//contém o tema que passo por parametro
            
            return await query.ToArrayAsync();
        }

       

        public async Task<Evento> GetEventoAsyncById(int EventoId, bool includePalestrantes)
        {
            IQueryable<Evento> query = _context.Eventos.Include(c => c.Lotes).Include(c => c.RedesSociais);

            if(includePalestrantes){//se passei valor verdadeiro
                query = query.Include(pe => pe.PalestranteEventos).ThenInclude(p => p.Palestrante);
            } 

            query = query.AsNoTracking().OrderByDescending(c => c.DataEvento).Where(c => c.Id == EventoId);//pega os que foram adicionados por último

            return await query.FirstOrDefaultAsync();
        }

        //palestrantes
         public async    Task<Palestrante[]> GetAllPalestrantesAsyncByName(string nome, bool includeEventos = false)
        {
            IQueryable<Palestrante> query = _context.Palestrantes.Include(c => c.RedesSociais);

            if(includeEventos){//se passei valor verdadeiro
                query = query.Include(pe => pe.PalestranteEventos).ThenInclude(e => e.Evento);
            } 

            query = query.AsNoTracking().OrderBy(p => p.Nome).Where(p => p.Nome.ToLower().Contains(nome.ToLower()));

            return await query.ToArrayAsync();
        }

        public async Task<Palestrante> GetPalestranteAsync(int PalestranteId, bool includeEventos = false)
        {
            IQueryable<Palestrante> query = _context.Palestrantes.Include(c => c.RedesSociais);

            if(includeEventos){//se passei valor verdadeiro
                query = query.Include(pe => pe.PalestranteEventos).ThenInclude(e => e.Evento);
            } 

            query = query.AsNoTracking().OrderBy(p => p.Nome).Where(p => p.Id == PalestranteId);//pega os que foram adicionados por último

            return await query.FirstOrDefaultAsync();
        }

        
    }
}