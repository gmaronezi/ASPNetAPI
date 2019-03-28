using System.Threading.Tasks;
using ProAgil.Domain;

namespace ProAgil.Repository
{
    public interface IProAgilRepository
    {
        //geral
         void Add<T>(T entity) where T : class;
        
         void Update<T>(T entity) where T : class;

         void Delete<T>(T entity) where T : class;

         Task<bool> SaveChangesAsync();

         //eventos
         Task<Evento[]> GetAllEventoAsyncByTema(string tema, bool includePalestrantes);

         Task<Evento[]> GetAllEventoAsync(bool includePalestrantes);

         Task<Evento> GetEventoAsyncById(int EventoId, bool includePalestrantes);

         Task<Palestrante[]> GetAllPalestrantesAsyncByName(string nome, bool includeEventos);

         Task<Palestrante> GetPalestranteAsync(int PalestranteId, bool includeEventos);
    }
}