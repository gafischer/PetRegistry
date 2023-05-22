using PetRegistry.Domain.Entities.Base;
using System.Diagnostics.CodeAnalysis;

namespace PetRegistry.Domain.Entities
{
    [ExcludeFromCodeCoverage]
    public class Pet : BaseEntity
    {
        public string Name { get; set; } = "";
        public string Breed { get; set; } = "";
        public int Sex { get; set; }
        public string? Description { get; set; }
        public bool Neutered { get; set; }
        public DateTime? NeuterDate { get; set; }
        public string Specie { get; set; } = "";
        public float Weight { get; set; }
        public string Color { get; set; } = "";
        public DateTime BirthDate { get; set; }
    }
}