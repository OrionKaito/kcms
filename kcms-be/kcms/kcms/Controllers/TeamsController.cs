using KCMS.Domain.Team;
using KCMS.Domain.ViewModel;
using KCMS.Infrastructure;
using KCMS.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace KCMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamsController : ControllerBase
    {
        private readonly TeamService _teamService;

        public TeamsController(TeamService teamService)
        {
            _teamService = teamService;
        }

        // GET: api/Teams
        [HttpGet]
        public ActionResult<RankingListViewModel> GetTeams(int pageNumber = 1, int pageSize = 10, long leagueId = 0, string searchValue = "")
        {
            return _teamService.GetRanking(pageNumber, pageSize, leagueId, searchValue);
        }

        // GET: api/Teams/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public ActionResult<Team> GetTeam(long id)
        {
            var team = _teamService.GetTeam(id);

            if (team == null)
            {
                return NotFound();
            }

            return team;
        }

        // PUT: api/Teams/5
        [HttpPut()]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> PutTeam([FromForm] TeamUpdateModel model)
        {
            await _teamService.UpdateTeam(model);

            return NoContent();
        }

        // POST: api/Teams
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<Team>> PostTeam([FromForm] TeamInsertModel model)
        {
            var team = await _teamService.AddTeam(model);

            return CreatedAtAction("GetTeam", new { id = team.Id }, team);
        }

        // DELETE: api/Teams/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Team>> DeleteTeam(long id)
        {
            var team = await _teamService.DeleteTeam(id);
            return team;
        }
    }
}
