using System;
using System.Linq;
using AutoMapper;
using ProAgil.Dominio;
using ProAgil.Dominio.Identity;
using ProAgil.WebAPI.Dtos;

namespace ProAgil.WebAPI.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Evento, EventoDto>()
                .ReverseMap();
            CreateMap<User, UserDto>()
                .ForMember(dest => dest.Eventos, opt => {
                    opt.MapFrom(src => src.UsersEventos.Select(x => x.Evento).ToList());
                }).ReverseMap();
            //CreateMap<Evento, EventoDto>().ReverseMap();
            CreateMap<Lote, LoteDto>().ReverseMap();
            CreateMap<RedeSocial, RedeSocialDto>().ReverseMap();

            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<User, UserLoginDto>().ReverseMap();
        }

    }
}