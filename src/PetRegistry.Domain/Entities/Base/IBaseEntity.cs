using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PetRegistry.Domain.Entities.Base
{
    public interface IBaseEntity
    {
        int Id { get; }
        DateTime CreatedDate { get; }
        DateTime ModifiedDate { get; }
    }
}
