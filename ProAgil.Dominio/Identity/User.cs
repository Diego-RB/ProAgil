using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace ProAgil.Dominio.Identity
{
    public class User: IdentityUser<int>
    {
        [Column(TypeName = "nvarchar(150)")]
        public string FullName { get; set; }
        public int RedeSocialId { get; set; }
        public string ImagemURL { get; set; }
        public List<UserRole> UserRoles { get; set; }
        public List<UserEvento> UsersEventos { get; set; }
        public List<RedeSocial> RedesSociais { get; set; }
    }
}