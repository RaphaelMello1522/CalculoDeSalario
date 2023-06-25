using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.UI.V4.Pages.Account.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class People
    {
        [Required, Key]
        public Guid Id { get; set; }
        
        [Required(ErrorMessage = "É necessário informar um cargo ao registrar uma pessoa"), ForeignKey("Cargo")]
        public Guid CargoId { get; set; }

        [Required(ErrorMessage = "Informe um nome!")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Informe o sexo!")]
        public string Sexo { get; set; }
        public string PictureSource { get; set; }
        [NotMapped]
        public IFormFile ProfilePicture { get; set; }
        public virtual Cargo Cargo { get; set; }
    }
}
