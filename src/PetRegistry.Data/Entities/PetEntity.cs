using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace PetRegistry.Data.Entities
{
    [ExcludeFromCodeCoverage]
    public class PetEntity
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = "";

        [Required]
        public string Breed { get; set; } = "";

        [Required]
        public int Sex { get; set; }
        public string? Description { get; set; }

        [Required]
        public bool Neutered { get; set; }
        public DateTime? NeuterDate { get; set; }

        [Required]
        public string Specie { get; set; } = "";

        [Required]
        public float Weight { get; set; }

        [Required]
        public string Color { get; set; } = "";

        [Required]
        public DateTime BirthDate { get; set; }
    }
}
