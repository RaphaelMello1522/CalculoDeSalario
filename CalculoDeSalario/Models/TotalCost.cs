namespace CalculoDeSalario.Models
{
    public class TotalCost
    {
        public Guid Id { get; set; }
        public Guid PeopleId { get; set; }
        public Guid SalaryCostId { get; set; }
        public string CostDescription { get; set; }
        public enum CostType
        {
            Salario = 1,
            Manutencao = 2
        }

        public People People { get; set; }
    }
}
