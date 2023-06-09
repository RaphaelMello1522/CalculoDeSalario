namespace Domain.Entities
{
    public class Vagas
    {
        public Guid Id { get; set; }
        public string NomeVaga { get; set; }
        public string? DescricaoVaga { get; set; }
        public decimal? Salario { get; set; }
        public string? CardImgUrl { get; set; }
        public People? CandidatosVaga { get; set; }
    }
}
