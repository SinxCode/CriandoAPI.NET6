using FilmesAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace FilmesAPI.Data
{
    //A CRIAÇÃO DA STRING DE CONEXÃO ESTÁ NO APPSETTINGS.JSON E A CONEXÃO ESTÁ NO ARQUIVO PROGRAM.CS
    public class FilmeContext :DbContext
    {
        public FilmeContext(DbContextOptions<FilmeContext> opts) : base(opts)
        {
            
        }
        public DbSet<Filme> Filmes { get; set; }
    }
}
