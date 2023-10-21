using AutoMapper;
using FilmesAPI.Data.Dtos;
using FilmesAPI.Models;

namespace FilmesAPI.Profiles
{
    public class FilmeProfile : Profile
    {
        //Convertendo o FilmeDTO para Filme e vice versa para as funcionalidades do Controller.
        public FilmeProfile()
        {
            CreateMap<CreateFilmeDto, Filme>();
            CreateMap<UpdateFilmeDto, Filme>();
            CreateMap<Filme,UpdateFilmeDto>();
            CreateMap<Filme, ReadFilmeDto>();

        }

     
    }
}
