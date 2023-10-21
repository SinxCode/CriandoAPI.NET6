using AutoMapper;
using Azure;
using FilmesAPI.Data;
using FilmesAPI.Data.Dtos;
using FilmesAPI.Models;
using Microsoft.AspNetCore.JsonPatch;
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
            return CreatedAtAction(nameof(BuscarFilmeId), new { id = filme.Id }, filme); //Retorna pro usuário como ficou o dado que ele inseriu, de modo que facilite ele a buscar por esse dado nos método get por id

        }

        //Busca todos os dados de uma vez, ou de forma intervalada pelo skip e take.
        [HttpGet]
        public IEnumerable<ReadFilmeDto> BuscarFilmes([FromQuery] int skip = 0, [FromQuery] int take = 10) //Aqui diz que se o usuário n passar parametros, carregará 50 filmes. Se quiser recarregar tudo, basta tirar o skip e o take
        {
            return _mapper.Map<List<ReadFilmeDto>>(_context.Filmes.Skip(skip).Take(take));
        }

        //Busca um dado por ID esepecífico
        [HttpGet("{id}")]
        public IActionResult BuscarFilmeId(int id)
        {
            var filme = _context.Filmes.FirstOrDefault(filme => filme.Id == id);
            if (filme == null) return NotFound();
            var filmeDto = _mapper.Map<ReadFilmeDto>(filme);
            return Ok(filmeDto);
        }

        //Atualizando todo o registro de um id especifico.
        [HttpPut("{id}")]
        public IActionResult AtualizaFilme(int id, [FromBody] UpdateFilmeDto filmeDto)
        {

            var filme = _context.Filmes.FirstOrDefault(filme => filme.Id == id);
            if (filme == null) return NotFound();
            _mapper.Map(filmeDto, filme);
            _context.SaveChanges();

            return NoContent(); //Normalmente para funções de atualização de banco ou projeto, utilizamos o NoContent como retorno
        }

        //Atualização Parcial (um ou mais campos)
        [HttpPatch("{id}")]
        public IActionResult AtualizaFilmeParcial(int id, JsonPatchDocument<UpdateFilmeDto> patch)
        {

            var filme = _context.Filmes.FirstOrDefault(filme => filme.Id == id);
            if (filme == null) return NotFound();

            var filmeParaAtualizar = _mapper.Map<UpdateFilmeDto>(filme);
            patch.ApplyTo(filmeParaAtualizar, ModelState);

            if (!TryValidateModel(filmeParaAtualizar))
            {
                return ValidationProblem(ModelState);
            }

            _mapper.Map(filmeParaAtualizar, filme);
            _context.SaveChanges();

            return NoContent(); //Normalmente para funções de atualização de banco ou projeto, utilizamos o NoContent como retorno
        }

        //Deletando um filme
        [HttpDelete("{id}")]
        public IActionResult DeletaFilme(int id)
        {
            var filme = _context.Filmes.FirstOrDefault(filme => filme.Id == id);
            if (filme == null) return NotFound();
            _context.Remove(filme);
            _context.SaveChanges();
            return NoContent();
        }


    }
}
