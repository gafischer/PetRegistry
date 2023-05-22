using MediatR;
using Microsoft.AspNetCore.Mvc;
using PetRegistry.API.Controllers.Base;
using PetRegistry.Application.Commands.Pets.CreatePet;
using PetRegistry.Application.Commands.Pets.UpdatePet;
using PetRegistry.Application.CQRS.Pets.Commands.DeletePet;
using PetRegistry.Application.Queries.Pets.GetAllPets;
using PetRegistry.Application.Queries.Pets.GetPetById;
using PetRegistry.Domain.Entities;

namespace PetRegistry.Api.Controllers
{
    public class PetController : BaseController
    {
        public PetController(ILogger<PetController> logger, IMediator mediator) : base(logger, mediator)
        {

        }

        [HttpPost(Name = "AddPet")]
        [ProducesResponseType(typeof(IEnumerable<Pet>), StatusCodes.Status201Created)]
        public async Task<IActionResult> CreatePet([FromBody] CreatePetCommandRequest createPetCommandRequest)
        {
            var createPetCommandResponse = await _mediator.Send(createPetCommandRequest);

            if (!createPetCommandResponse.Success)
            {
                return BadRequest(createPetCommandResponse);
            }

            return Created("", createPetCommandResponse);
        }

        [HttpGet(Name = "GetAllPets")]
        [ProducesResponseType(typeof(IEnumerable<Pet>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllPets()
        {
            var getAllPetsQueryRequest = new GetAllPetsQueryRequest();
            var getAllPetsQueryResponse = await _mediator.Send(getAllPetsQueryRequest);

            if (!getAllPetsQueryResponse.Success)
            {
                return BadRequest(getAllPetsQueryResponse);
            }

            return Ok(getAllPetsQueryResponse);
        }

        [HttpGet("{id:int}", Name = "GetPetById")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(Pet), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetPetById(int id)
        {
            var getPetByIdQueryRequest = new GetPetByIdQueryRequest(id);
            var getPetByIdQueryResponse = await _mediator.Send(getPetByIdQueryRequest);

            if (!getPetByIdQueryResponse.Success)
            {
                return BadRequest(getPetByIdQueryResponse);
            }

            return Ok(getPetByIdQueryResponse);
        }

        [HttpPut("{id:int}", Name = "UpdatePet")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdatePet(int id, [FromBody] UpdatePetCommandRequest updatePetCommandRequest)
        {
            updatePetCommandRequest.SetId(id);

            var updatePetCommandResposne = await _mediator.Send(updatePetCommandRequest);

            if (!updatePetCommandResposne.Success && updatePetCommandResposne.Errors?.Any() == true)
            {
                if (updatePetCommandResposne.Errors.First().Contains("not found"))
                {
                    return NotFound(updatePetCommandResposne.Errors);
                }

                return BadRequest(updatePetCommandResposne);
            }

            return NoContent();
        }

        [HttpDelete("{id:int}", Name = "DeletePet")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var deletePetCommandRequest = new DeletePetCommandRequest { Id = id };
            var deletePetCommandResponse = await _mediator.Send(deletePetCommandRequest);

            if (!deletePetCommandResponse.Success && deletePetCommandResponse.Errors?.Any() == true)
            {
                if (deletePetCommandResponse.Errors.First().Contains("not found"))
                {
                    return NotFound(deletePetCommandResponse.Errors);
                }

                return BadRequest(deletePetCommandResponse.Errors);
            }

            return NoContent();
        }
    }
}