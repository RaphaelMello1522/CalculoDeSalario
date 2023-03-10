namespace CalculoDeSalario.Models
{
    public class Salary
    {
        public Guid Id { get; set; }
        public Guid PeopleId { get; set; }
        public DateTime TimeWorkStart { get; set; }
        public DateTime TimeWorkEnd { get; set; }
        public TimeSpan TotalTimeWorked { get; set; }
        public double Total { get; set; }
        public People People { get; set; }
    }
}
