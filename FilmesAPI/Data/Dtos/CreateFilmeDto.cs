using System.ComponentModel.DataAnnotations;

namespace FilmesAPI.Data.Dtos
{
    public class CreateFilmeDto
    {
        //O DTO SERVE PARA O TRÁGEGO DE DADOS QUE SÃO NECESSÁRIOS PARA QUE O USUÁRIO ENXERGUE.
        //NO MODELS FILME.CS TEMOS BASICAMENTE A TABELA FILME DO MESMO JEITO QUE AQUI
        //A DIFERENÇA É QUE AQUI NO DTO, NÃO PASSAMOS O CAMPO ID, POIS O USUÁRIO NÃO PRECISA VISUALIZA-LO.
        //DEFINIREMOS NO CONSTRUTOR PARA INSERIR OS DADOS QUE SERÃO GERADOS POR ESSA CLASSE INTERMEDIADORA
        //E NÃO MAIS DIRETO DA "TABELA" COMO ESTÁVAMOS FAZENDO UTILIZANDO O MODELS.
   
        [Required(ErrorMessage = "O título do filme é Obrigatório")]
        public string Titulo { get; set; }

        [Required(ErrorMessage = "O Gênero do filme é Obrigatório"),
            StringLength(50, ErrorMessage = "O tamanho do gênero não pode exceder 50 caracteres.")]
        public string Genero { get; set; }

        [Required(ErrorMessage = "A duração do filme é Obrigatória"),
            Range(70, 600, ErrorMessage = "A duração deve ter entre 70 e 600 minutos")]
        public int Duracao { get; set; }

    }
}
