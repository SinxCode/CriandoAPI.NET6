using FilmesAPI.Models;
using Microsoft.AspNetCore.Mvc;


namespace FilmesAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FilmeController : ControllerBase
    {
        private static List<Filme> filmes = new List<Filme>();
        private static int id = 1;

        //Adiciona um dado, como um insert
        [HttpPost]
        public IActionResult AdicionaFilme([FromBody] Filme filme)
        {
            filme.Id = id++;
            filmes.Add(filme);
           return CreatedAtAction(nameof(GetFilmeId), new {id = filme.Id }, filme); //Retorna pro usuário como ficou o dado que ele inseriu, de modo que facilite ele a buscar por esse dado nos método get por id
     
        }

        //Busca todos os dados de uma vez, ou de forma intervalada pelo skip e take.
        [HttpGet]
        public IEnumerable<Filme> GetFilmes([FromQuery] int skip =0, [FromQuery] int take =10) //Aqui diz que se o usuário n passar parametros, carregará 50 filmes. Se quiser recarregar tudo, basta tirar o skip e o take
        {
            return filmes.Skip(skip).Take(take);
        }

        //Busca um dado por ID esepecífico
        [HttpGet("{id}")]
        public IActionResult GetFilmeId(int id)
        {
            var filme = filmes.FirstOrDefault(filme => filme.Id == id);
            if(filme == null) return NotFound();
            return Ok(filme);
        }

    }
}
