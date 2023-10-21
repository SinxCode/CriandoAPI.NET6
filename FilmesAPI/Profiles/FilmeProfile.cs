using AutoMapper;
using FilmesAPI.Data.Dtos;
using FilmesAPI.Models;

namespace FilmesAPI.Profiles
{
    public class FilmeProfile : Profile
    {
        //Convertendo o FilmeDTO para Filme
        public FilmeProfile()
        {
            CreateMap<CreateFilmeDto, Filme>();
            CreateMap<UpdateFilmeDto, Filme>();
            CreateMap<Filme,UpdateFilmeDto>();
            CreateMap<Filme, ReadFilmeDto>();

        }

     
    }
}
