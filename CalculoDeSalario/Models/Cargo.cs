namespace CalculoDeSalario.Models
{
    public class Cargo
    {
        public Guid Id { get; set; }
        public string NomeCargo { get; set; }
        public string? DescricaoCargo { get; set; }
        public string? ValueHour { get; set; }
    }
}
