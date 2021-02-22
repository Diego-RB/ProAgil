using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ProAgil.Dominio;
using ProAgil.Dominio.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProAgil.Repositorio
{
    public class ProAgilContext : IdentityDbContext<User, Role, int, IdentityUserClaim<int>, 
                                    UserRole, IdentityUserLogin<int>, IdentityRoleClaim<int>,
                                    IdentityUserToken<int>>
    {
        public ProAgilContext(DbContextOptions<ProAgilContext> options) : base(options) { }
        
        
            public DbSet<Evento> Eventos { get; set; }
            public DbSet<RedeSocial> RedesSociais { get; set; }
            public DbSet<Lote> Lotes { get; set; }
            public DbSet<UserEvento> UserEvento { get; set; }

            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                base.OnModelCreating(modelBuilder);

                modelBuilder.Entity<UserRole>(userRole => 
                {
                    userRole.HasKey(ur => new {ur.UserId, ur.RoleId});
                    userRole.HasOne(ur => ur.Role)
                                .WithMany(r => r.UserRoles)
                                .HasForeignKey(ur => ur.RoleId)
                                .IsRequired();

                    userRole.HasOne(ur => ur.User)
                                .WithMany(r => r.UserRoles)
                                .HasForeignKey(ur => ur.UserId)
                                .IsRequired();
                });


                modelBuilder.Entity<UserEvento>()
                    .HasKey(UE => new {UE.UserId, UE.EventoId});
                
            }
    }
}
