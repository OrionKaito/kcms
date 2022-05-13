using KCMS.Domain.League;
using KCMS.Domain.ViewModel;
using KCMS.Infrastructure;
using KCMS.Ultitlies;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace KCMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeaguesController : ControllerBase
    {
        private readonly KCMSContext _context;
        private readonly IHostingEnvironment _hostingEnvironment;

        public LeaguesController(KCMSContext context, IHostingEnvironment hostingEnvironment)
        {
            _context = context;
            _hostingEnvironment = hostingEnvironment;
        }



        // GET: api/Leagues
        [HttpGet]
        public async Task<ActionResult<LeagueListViewModel>> GetLeagues(string searchValue = "")
        {
            Expression<Func<League, bool>> filter = l => (String.IsNullOrEmpty(searchValue)
            || ((l.Name.Contains(searchValue)) || (l.Id.ToString() == searchValue)));

            var leagues = await _context.Leagues.Where(filter).OrderByDescending(l => l.CreatedDate).ToListAsync();

            return new LeagueListViewModel
            {
                Results = leagues,
                TotalPages = 0
            };
        }

        // GET: api/Leagues/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<League>> GetLeague(long id)
        {
            var league = await _context.Leagues.FindAsync(id);

            if (league == null)
            {
                return NotFound();
            }

            return league;
        }

        // PUT: api/Leagues/5
        [HttpPut()]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> PutLeague([FromForm] LeagueUpdateModel model)
        {
            var league = await _context.Leagues.FindAsync(model.Id);

            var fileName = "";
            if (model.Image != null)
            {
                FileUlti.DeleteFile(league.Image, _hostingEnvironment);
                fileName = FileUlti.UploadFile(model.Image, _hostingEnvironment);
                league.Image = fileName;
            }

            league.Name = model.Name;
            league.Image = fileName;
            league.UpdatedDate = DateTime.UtcNow;
            _context.Entry(league).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }

            return NoContent();
        }

        // POST: api/Leagues
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<League>> PostLeague([FromForm] LeagueInsertModel model)
        {
            var fileName = "";
            if (model.Image != null)
            {
                fileName = FileUlti.GetUniqueFileName(model.Image.FileName);
            }

            var league = new League { Name = model.Name, Image = fileName, CreatedDate = DateTime.UtcNow };
            _context.Leagues.Add(league);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetLeague", new { id = league.Id }, league);
        }

        // DELETE: api/Leagues/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<League>> DeleteLeague(long id)
        {
            var league = await _context.Leagues.FindAsync(id);
            if (league == null)
            {
                return NotFound();
            }

            _context.Leagues.Remove(league);
            await _context.SaveChangesAsync();

            return league;
        }
    }
}
