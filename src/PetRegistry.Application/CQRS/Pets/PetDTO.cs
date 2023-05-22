using PetRegistry.Shared.Enums;

namespace PetRegistry.Application.CQRS.Pets
{
    public class PetDTO
    {
        public PetDTO()
        {
        }

        public int Id { get; private set; }
        public string Name { get; set; } = "";
        public string Breed { get; set; } = "";
        public EPetSex Sex { get; set; }
        public string? Description { get; set; }
        public bool Neutered { get; set; }
        public DateTime? NeuterDate { get; set; }
        public string Specie { get; set; } = "";
        public float Weight { get; set; }
        public string Color { get; set; } = "";
        public DateTime BirthDate { get; set; }

        public void SetId(int id)
        {
            Id = id;
        }
    }
}
