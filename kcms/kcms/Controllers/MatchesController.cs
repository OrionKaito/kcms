using KCMS.Domain.Base;
using KCMS.Domain.Match;
using KCMS.Domain.ViewModel;
using KCMS.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KCMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MatchesController : ControllerBase
    {
        private readonly MatchService _matchService;

        public MatchesController(MatchService matchService)
        {
            _matchService = matchService;
        }

        // GET: api/Matches
        [HttpGet]
        public ActionResult<MatchListViewModel> GetMatchs(MatchType? matchType, MatchTime? matchTime, DateTime? fromDate, DateTime? toDate, int pageNumber = 1, int pageSize = 10, long leagueId = 0, string searchValue = "")
        {
            return _matchService.GetMatchs(pageNumber, pageSize, matchType, leagueId, matchTime, searchValue, fromDate, toDate);
        }

        // GET: api/GetMatchSchedules
        [HttpGet("GetMatchSchedules")]
        public ActionResult<MatchScheduleListViewModel> GetMatchSchedules(int pageNumber = 1, int pageSize = 10)
        {
            return _matchService.GetMatchSchedules(pageNumber, pageSize);
        }

        // GET: api/GetMatchHistorys
        [HttpGet("GetMatchHistoryOfTeam")]
        public ActionResult<IEnumerable<MatchScheduleViewModel>> GetMatchHistoryOfTeam(long id, int number = 6)
        {
            return Ok(_matchService.GetMatchHistoryOfTeam(id, number));
        }

        [HttpGet("GetMatchHistoryBetweenTeam")]
        public ActionResult<IEnumerable<MatchScheduleViewModel>> GetMatchHistoryBetweenTeam(long team1Id, long team2Id, int number = 6)
        {
            return Ok(_matchService.GetMatchHistoryBetweenTeam(team1Id, team2Id, number));
        }

        // GET: api/Matches/5
        [HttpGet("{id}")]
        public ActionResult<MatchViewModel> GetMatch(long id)
        {
            return _matchService.GetMatch(id);
        }

        // PUT: api/Matches
        [HttpPut()]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> PutMatch(MatchUpdateModel model)
        {
            await _matchService.UpdateMatch(model);

            return NoContent();
        }

        // POST: api/Matches
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<Match>> PostMatch(MatchInsertModel model)
        {
            var match = await _matchService.AddMatch(model);

            return CreatedAtAction("GetMatch", new { id = match.Id }, match);
        }

        // DELETE: api/Matches/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Match>> DeleteMatch(long id)
        {
            var match = await _matchService.DeleteMatch(id);

            return match;
        }
    }
}
