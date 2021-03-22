using System.ComponentModel.DataAnnotations;
using ProAgil.Dominio.Identity;

namespace ProAgil.Dominio
{
    public class RedeSocial
    {
        [Key]
        public int Id { get; set; }
        public string Nome { get; set; }
        public string URL { get; set; }
        public int? EventoId { get; set; }
        public Evento Evento { get; }
        public int? UserId { get; set; }
        public User User { get; }
    }
}