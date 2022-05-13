using KCMS.Domain.Advertising;
using KCMS.Domain.ViewModel;
using KCMS.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace KCMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdvertisingsController : ControllerBase
    {
        private readonly AdversitingService _adversitingService;

        public AdvertisingsController(AdversitingService adversitingService)
        {
            _adversitingService = adversitingService;
        }

        // GET: api/Advertisings
        [HttpGet]
        public ActionResult<AdvertisingListViewModel> GetAdvertisings(int pageNumber = 1, int pageSize = 10, string searchValue = "")
        {
            return _adversitingService.GetAdvertisings(pageNumber, pageSize, searchValue);
        }

        // GET: api/Advertisings/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public ActionResult<Advertising> GetAdvertising(long id)
        {
            return _adversitingService.GetAdvertising(id);
        }

        // PUT: api/Advertisings/5
        [HttpPut()]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> PutAdvertising([FromForm] AdvertisingUpdateModel model)
        {
            await _adversitingService.UpdateAdvertising(model);

            return NoContent();
        }

        // POST: api/Advertisings
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<Advertising>> PostAdvertising([FromForm] AdvertisingInsertModel model)
        {
            var advertising = await _adversitingService.AddAdvertising(model);

            return CreatedAtAction("GetAdvertising", new { id = advertising.Id }, advertising);
        }

        // DELETE: api/Advertisings/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<Advertising>> DeleteAdvertising(long id)
        {
            var advertising = await _adversitingService.DeleteAdvertising(id);

            return advertising;
        }
    }
}
