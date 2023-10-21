using AutoMapper;
using FilmesAPI.Data;
using FilmesAPI.Data.Dtos;
using FilmesAPI.Models;
using Microsoft.AspNetCore.Mvc;


namespace FilmesAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FilmeController : ControllerBase
    {
        private FilmeContext _context;
        private IMapper _mapper;

        public FilmeController(FilmeContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        //Adiciona um dado, como um insert
        [HttpPost]
        public IActionResult AdicionaFilme([FromBody] CreateFilmeDto filmeDto)
        {
            
            //Convertendo o Filme do Models para o Filme do DTO
            Filme filme = _mapper.Map<Filme>(filmeDto);
            //O .Add e .SaveChanges vem do DbSet criado no FilmeContext
            _context.Filmes.Add(filme);
            _context.SaveChanges();
           return CreatedAtAction(nameof(GetFilmeId), new {id = filme.Id }, filme); //Retorna pro usuário como ficou o dado que ele inseriu, de modo que facilite ele a buscar por esse dado nos método get por id
     
        }

        //Busca todos os dados de uma vez, ou de forma intervalada pelo skip e take.
        [HttpGet]
        public IEnumerable<Filme> GetFilmes([FromQuery] int skip =0, [FromQuery] int take =10) //Aqui diz que se o usuário n passar parametros, carregará 50 filmes. Se quiser recarregar tudo, basta tirar o skip e o take
        {
            return _context.Filmes.Skip(skip).Take(take);
        }

        //Busca um dado por ID esepecífico
        [HttpGet("{id}")]
        public IActionResult GetFilmeId(int id)
        {
            var filme = _context.Filmes.FirstOrDefault(filme => filme.Id == id);
            if(filme == null) return NotFound();
            return Ok(filme);
        }

    }
}
