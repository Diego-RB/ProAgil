using System.Collections.Generic;

namespace ProAgil.WebAPI.Dtos
{
    public class UserDto
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
        public int PhoneNumber { get; set; }
        public string Profissao { get; set; }
        public string ImagemURL { get; set; }
        public List<RedeSocialDto> RedesSociais { get; set; }
        public List<EventoDto> Eventos { get; set; }
    }
}