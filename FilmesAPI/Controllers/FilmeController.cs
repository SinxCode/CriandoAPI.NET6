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

        [HttpPost]
        public void AdicionaFilme([FromBody] Filme filme)
        {
            filme.Id = id++;
            filmes.Add(filme);
            Console.WriteLine(filme.Titulo);
            Console.WriteLine(filme.Duracao);
        }

        [HttpGet]
        public IEnumerable<Filme> GetFilmes([FromQuery] int skip =0, [FromQuery] int take =10) //Aqui diz que se o usuário n passar parametros, carregará 50 filmes. Se quiser recarregar tudo, basta tirar o skip e o take
        {
            return filmes.Skip(skip).Take(take);
        }

        [HttpGet("{id}")]
        public Filme? GetFilmeId(int id)
        {
            return filmes.FirstOrDefault(filme => filme.Id == id);
        }

    }
}
