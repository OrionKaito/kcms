using AutoMapper;
using KCMS.Domain.Base;
using KCMS.Domain.Team;
using KCMS.Domain.ViewModel;
using KCMS.Ultitlies;
using Microsoft.AspNetCore.Hosting;
using System;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace KCMS.Services
{
    public class TeamService
    {
        private readonly ITeamRepository _teamRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IHostingEnvironment _hostingEnvironment;

        public TeamService(ITeamRepository teamRepository, IUnitOfWork unitOfWork, IMapper mapper, IHostingEnvironment hostingEnvironment)
        {
            _teamRepository = teamRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _hostingEnvironment = hostingEnvironment;
        }

        public RankingListViewModel GetRanking(int pageNumber, int pageSize, long leagueId, string searchValue)
        {
            Expression<Func<Team, bool>> filter = t => (leagueId == 0 || t.LeagueId == leagueId)
            && (String.IsNullOrEmpty(searchValue) || ((t.TeamName.Contains(searchValue)) || (t.Id.ToString() == searchValue) || (t.League.Name.Contains(searchValue))));

            var rankings = _teamRepository.Get(filter, a => a.OrderByDescending(a => a.D), "League").Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

            return new RankingListViewModel
            {
                TotalPages = _teamRepository.GetTotalPages(filter),
                Results = rankings
            };
        }

        public Team GetTeam(long id)
        {
            var team = _teamRepository.GetByID(id);

            if (team == null)
            {
                throw new Exception("NotFound");
            }

            return team;
        }

        public async Task<Team> AddTeam(TeamInsertModel model)
        {
            var fileName = "";
            if (model.Image != null)
            {
                fileName = FileUlti.UploadFile(model.Image, _hostingEnvironment);
            }

            var team = _mapper.Map<Team>(model);
            team.Image = fileName;

            try
            {
                team.CreatedDate = DateTime.UtcNow;
                _teamRepository.Insert(team);
                await _unitOfWork.CommitAsync();
                return team;
            }
            catch (Exception ex)
            {
                throw new Exception("Insert Fail : " + ex);
            }
        }

        public async Task UpdateTeam(TeamUpdateModel model)
        {
            var team = _teamRepository.GetByID(model.Id);

            if (team == null)
            {
                throw new Exception("NotFound");
            }

            if (model.Image != null)
            {
                FileUlti.DeleteFile(team.Image, _hostingEnvironment);
                var fileName = FileUlti.UploadFile(model.Image, _hostingEnvironment);
                team.Image = fileName;
            }

            team.TeamName = model.TeamName;
            team.LeagueId = model.LeagueId;
            team.B = model.B;
            team.D = model.D;
            team.ST = model.ST;
            team.T = model.T;
            team.H = model.H;
            team.TG = model.TG;
            team.TH = model.TH;
            team.HS = model.HS;
            team.D = model.D;
            team.UpdatedDate = DateTime.UtcNow;

            try
            {
                _teamRepository.Update(team);
                await _unitOfWork.CommitAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Update Fail : " + ex);
            }
        }

        public async Task<Team> DeleteTeam(long id)
        {
            var team = _teamRepository.GetByID(id);
            var image = team.Image;

            if (team == null)
            {
                throw new Exception("NotFound");
            }

            _teamRepository.Delete(team);
            await _unitOfWork.CommitAsync();
            FileUlti.DeleteFile(image, _hostingEnvironment);
            return team;
        }
    }
}
