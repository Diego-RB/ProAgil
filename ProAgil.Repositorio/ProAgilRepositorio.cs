using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Dapper.Contrib.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ProAgil.Dominio;
using ProAgil.Dominio.Identity;


namespace ProAgil.Repositorio
{
    public class ProAgilRepositorio : IProAgilRepositorio
    {
        private readonly string _conectionString;
        private readonly ProAgilContext _context;

        public ProAgilRepositorio(IConfiguration configuration, ProAgilContext context)
        {
            _conectionString = configuration.GetConnectionString("ProAgil");
            _context = context;
        }

     
        


        //Evento
        public List<Evento> GetAllEventoAsync(bool IncludeUsers)
        {
            using (SqlConnection connection = new SqlConnection(_conectionString))
            {
                var listaEventos = connection.Query<Evento>("select * from Eventos");
                return listaEventos.ToList();
            }
        }

        public List<Evento> GetAllEventoAsyncByTema(string tema, bool IncludeUsers)
        {
            using (SqlConnection connection = new SqlConnection(_conectionString))
            {
                var listaEventos = connection.Query<Evento>("select * from Eventos where Tema like '%'+@tema+'%'", new { @tema = tema } );
                return listaEventos.ToList();
            }
        }

        public async Task<Evento> GetEventoAsyncById(int EventoId, bool IncludeUsers)
        {
            using (SqlConnection connection = new SqlConnection(_conectionString))
            {
                
                var dados = 
                    
                    @"SELECT Id, Local, DataEvento, Tema, QtdPessoas, ImagemURL, Telefone, Email, Descricao       
                        From Eventos
                        where Id = @Id

                        SELECT Id, Tipo, Preco, DataInicio, DataFim, quantidade
                        From Lotes 
                        where EventoId = @Id

                        SELECT Id, Nome, URL
                        From RedesSociais
                        where EventoId = @Id
                    ";

                var result = connection.QueryMultiple(dados, new { @Id = EventoId });
                
                    var eventos = result.ReadSingle<Evento>();
                        eventos.Lotes = result.Read<Lote>().ToList();
                        eventos.RedesSociais = result.Read<RedeSocial>().ToList(); 

                        return eventos;
            }
        }
        
    /*public async Task<Evento> GetEventoAsyncById(int EventoId, bool IncludeUsers)
        {
        IQueryable<Evento> query = _context.Eventos
            .Include(c => c.Lotes)
            .Include(c => c.RedesSociais);

        if(IncludeUsers)
        {
            query = query
                    .Include(ue => ue.UsersEventos)
                    .ThenInclude(u => u.User);
        }

        query = query.AsNoTracking()
        .OrderBy(c => c.Id)
            .Where(c => c.Id == EventoId);

        return await query.FirstOrDefaultAsync();
        }*/

        //User
        public Task<User> GetAllUserAsync(int UserId, bool IncludeEventos)
        {
            throw new System.NotImplementedException();
        }

        public Task<User[]> GetAllUsersAsyncByUserName(string userName, bool IncludeEventos)
        {
            throw new System.NotImplementedException();
        }



        
        /*private readonly ProAgilContext _context;

        public ProAgilRepositorio(ProAgilContext context)
        {
        _context = context;
        _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }*/

        //Geral
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
        public void DeleteRange<T>(T[] entityArray) where T : class
        {
        _context.RemoveRange(entityArray);
        }
        public async Task<bool> SaveChangesAsync()
        {
        return (await _context.SaveChangesAsync()) > 0;
        }
        /*
        //Evento
        public async Task<Evento[]> GetAllEventoAsync(bool IncludeUsers = false)
        {
        IQueryable<Evento> query = _context.Eventos
            .Include(c => c.Lotes)
            .Include(c => c.RedesSociais);

        if (IncludeUsers)
        {
            query = query
                .Include(ue => ue.UsersEventos)
                .ThenInclude(u => u.User);
        }

        query = query.AsNoTracking()
                    .OrderBy(c => c.Id);

        return await query.ToArrayAsync();
        }

        public async Task<Evento> GetEventoAsyncById(int EventoId, bool IncludeUsers)
        {
        IQueryable<Evento> query = _context.Eventos
            .Include(c => c.Lotes)
            .Include(c => c.RedesSociais);

        if(IncludeUsers)
        {
            query = query
                    .Include(ue => ue.UsersEventos)
                    .ThenInclude(u => u.User);
        }

        query = query.AsNoTracking()
        .OrderBy(c => c.Id)
            .Where(c => c.Id == EventoId);

        return await query.FirstOrDefaultAsync();
        }

        public async Task<Evento[]> GetAllEventoAsyncByTema(string tema, bool includeUsers)
        {
        IQueryable<Evento> query = _context.Eventos
            .Include(c => c.Lotes)
            .Include(c => c.RedesSociais);

        if (includeUsers)
        {
            query = query
                .Include(ue => ue.UsersEventos)
                .ThenInclude(p => p.User);
        }

        query = query.AsNoTracking()
                    .OrderByDescending(c => c.DataEvento)
                    .Where(c => c.Tema.ToLower().Contains(tema.ToLower()));

        return await query.ToArrayAsync();
        }

        //Palestrante
        public async Task<User> GetAllUserAsync(int UserId, bool IncludeEventos = false)
        {
        IQueryable<User> query = _context.Users
            .Include(c => c.RedesSociais);

        if(IncludeEventos)
        {
            query = query
                    .Include(ue => ue.UsersEventos)
                    .ThenInclude(e => e.Evento);
        }

        query = query.AsNoTracking()
        .OrderBy(u => u.UserName)
            .Where(u => u.Id == UserId);


        return await query.FirstOrDefaultAsync();
        }

        public async Task<User[]> GetAllUsersAsyncByUserName(string userName, bool IncludeEventos = false)
        {
        IQueryable<User> query = _context.Users
            .Include(c => c.RedesSociais);

        if(IncludeEventos)
        {
            query = query
                    .Include(pe => pe.UsersEventos)
                    .ThenInclude(e => e.Evento);
        }

        query = query.OrderBy(u => u.UserName.ToLower().Contains(userName.ToLower()));



        return await query.ToArrayAsync();
        }*/
    }
}