using System.ComponentModel.DataAnnotations.Schema;

namespace CalculoDeSalario.Models
{
    public class People
    {
        public Guid Id { get; set; }
        public Guid CargoId { get; set; }
        public string Name { get; set; }
        public string Sexo { get; set; }
        public string PictureSource { get; set; }
        [NotMapped]
        public IFormFile ProfilePicture { get; set; }
        public Cargo Cargo { get; set; }
    }
}
