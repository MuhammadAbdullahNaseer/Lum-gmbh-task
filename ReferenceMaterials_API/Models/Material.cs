using System.Xml.Linq;
using System.ComponentModel.DataAnnotations;

namespace ReferenceMaterials_API.Models
{
    public class Material
    {
        
        public string? Id { get; set; }//to organize our model RavenDb willl automatically recognize that as the primary identification of your document

        [Required]
        public string? Name  { get; set; } //? represents a nullable type

        [Required]
        public string? IsVisible { get; set; }

        [Required]
        public string? TypeOfPhase { get; set; } //solid and liquid

        [Required]
        public string? MinTemp  { get; set; }

        [Required]
        public string? MaxTemp { get; set; }


    }
}
