using System.Collections.Generic;
using System.Threading.Tasks;
using ProAgil.Dominio;
using ProAgil.Dominio.Identity;

namespace ProAgil.Repositorio
{
    public interface IProAgilRepositorio
    {
        //Geral
         void Add<T>(T entity) where T: class;
         void Update<T>(T entity) where T: class;
         void Delete<T>(T entity) where T: class;
         void DeleteRange<T>(T[] entity) where T: class;
         Task<bool> SaveChangesAsync();

         //Evento
         List<Evento> GetAllEventoAsyncByTema(string tema, bool IncludeUsers);
         List<Evento> GetAllEventoAsync(bool IncludeUsers);
         Evento GetEventoAsyncById(int EventoId, bool IncludeUsers);
         


         //Palestrante
         Task<User[]> GetAllUsersAsyncByUserName(string userName, bool IncludeEventos);
         Task<User> GetAllUserAsync(int UserId, bool IncludeEventos);
        
    }
}