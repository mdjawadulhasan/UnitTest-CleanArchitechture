using Application.Common.Persistence;
using Domain.PersonEntity;
using Microsoft.AspNetCore.Mvc;

namespace Host.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class PersonController : ControllerBase
    {
        private IPersonService _personService;
        public PersonController(IPersonService personService)
        {
            _personService = personService;

        }


        [HttpGet]
        public IActionResult GetAllPersons()
        {
            try
            {
                var result = _personService.GetAllPersons();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost]
        public IActionResult CreatePerson([FromBody] Person person)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid Model Object");
            }

            try
            {
                var result = _personService.CreatePerson(person);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetPersonById(Guid id)
        {
            try
            {
                var person = _personService.GetPersonById(id);
                if (person == null)
                {
                    return NotFound();
                }
                return Ok(person);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }



        [HttpPut("{id}")]
        public IActionResult UpdatePerson(Guid id, [FromBody] Person person)
        {
            try
            {
                var result = _personService.UpdatePerson(person);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }


        [HttpDelete]
        public IActionResult DeletePerson(Guid id)
        {
            try
            {
                var result = _personService.DeletePerson(id);
                return Ok(result);

            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

    }
}
