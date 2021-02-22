using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProAgil.WebAPI.Dtos
{
    public class EventoDto
    {
      public int Id { get; set; }
      [Required(ErrorMessage = "Campo Obrigatório")]
      [StringLength(100, MinimumLength =3, ErrorMessage = "Local deve ter entre 3 e 100 Caracters")]
        public string Local { get; set; }
        public string DataEvento { get; set; }
        [Required(ErrorMessage = "O Tema de ser preenchido")]
        public string Tema { get; set; }
        [Range(2,120000, ErrorMessage = "Quantidade de Pessoas deve ser entre 2 e 120000")]
        public int QtdPessoas { get; set; }
        public string ImagemURL { get; set; }
        [Phone]
        public string Telefone { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public string Descricao { get; set; }
        public List<LoteDto> Lotes { get; set; }
        public List<RedeSocialDto> RedesSociais { get; set; }
        public List<UserDto> Users { get; set; }  
    }
}