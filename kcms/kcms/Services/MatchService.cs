using AutoMapper;
using KCMS.Domain.Base;
using KCMS.Domain.Match;
using KCMS.Domain.Team;
using KCMS.Domain.ViewModel;
using KCMS.Ultitlies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MatchType = KCMS.Domain.Base.MatchType;

namespace KCMS.Services
{
    public class MatchService
    {
        private readonly IMatchRepository _matchRepository;
        private readonly ITeamRepository _teamRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public MatchService(IMatchRepository matchRepository, ITeamRepository teamRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _matchRepository = matchRepository;
            _teamRepository = teamRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public MatchScheduleListViewModel GetMatchSchedules(int pageNumber, int pageSize)
        {
            Expression<Func<Match, bool>> filter = m => m.Time > DateTime.UtcNow;

            var matchs = _matchRepository.Get(filter, a => a.OrderByDescending(a => a.CreatedDate), "League,HomeTeam,GuestTeam")
                .Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
            var result = _mapper.Map<List<MatchScheduleViewModel>>(matchs);

            return new MatchScheduleListViewModel
            {
                TotalPages = _matchRepository.GetTotalPages(filter, pageSize),
                Results = result
            };
        }

        public MatchListViewModel GetMatchs(int pageNumber, int pageSize, MatchType? matchType, long leagueId, MatchTime? matchTime, string searchValue, DateTime? fromDate, DateTime? toDate)
        {
            var isToday = false;
            var isTomorrow = false;
            var today = DateTime.UtcNow.Date;
            var tomorrow = DateTime.Today.AddDays(1);

            if (matchTime != null)
            {
                if (matchTime == MatchTime.Today)
                {
                    isToday = true;
                }
                else
                {
                    isTomorrow = true;
                }
            }

            Expression<Func<Match, bool>> filter = m =>
            (matchType == null || m.MatchType == matchType)
            && (leagueId == 0 || m.LeagueId == leagueId)
            && (isToday == false || DateTime.Compare(m.Time.Date, today) == 0)
            && (isTomorrow == false || m.Time.Date == tomorrow)
            && (fromDate == null || m.Time >= fromDate)
            && (toDate == null || m.Time <= toDate)
            && (String.IsNullOrEmpty(searchValue) || ((m.HomeTeam.TeamName.Contains(searchValue)) || (m.GuestTeam.TeamName.Contains(searchValue)) || (m.Id.ToString() == searchValue) || (m.League.Name.Contains(searchValue))));

            var matchs = _matchRepository.Get(filter, a => a.OrderByDescending(a => a.CreatedDate), "League,HomeTeam,GuestTeam")
                .Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

            var result = _mapper.Map<List<MatchViewModel>>(matchs);

            return new MatchListViewModel
            {
                TotalPages = _matchRepository.GetTotalPages(filter, pageSize),
                Results = result
            };
        }

        public MatchViewModel GetMatch(long id)
        {
            var match = _matchRepository.Get(m => m.Id == id, a => a.OrderByDescending(a => a.CreatedDate), "League,HomeTeam,GuestTeam").FirstOrDefault();

            if (match == null)
            {
                throw new Exception("NotFound");
            }

            var result = _mapper.Map<MatchViewModel>(match);
            return result;
        }

        public async Task<Match> AddMatch(MatchInsertModel model)
        {
            var match = _mapper.Map<Match>(model);
            match.CreatedDate = DateTime.UtcNow;

            if (!String.IsNullOrEmpty(model.Slug))
            {
                match.Slug = model.Slug.Slugify();
            }
            else
            {
                var hometeam = _teamRepository.GetByID(model.HomeTeamId);
                var guestteam = _teamRepository.GetByID(model.GuestTeamId);

                var slug = $"{hometeam.TeamName} vs {guestteam.TeamName} {model.Time.ToString("HH:mm dd M yyyy")}".Slugify();
                match.Slug = slug;
            }

            try
            {
                _matchRepository.Insert(match);
                await _unitOfWork.CommitAsync();
                return match;
            }
            catch (Exception ex)
            {
                throw new Exception("Insert Fail : " + ex);
            }
        }

        public async Task UpdateMatch(MatchUpdateModel model)
        {
            var match = _matchRepository.GetByID(model.Id);

            if (match == null)
            {
                throw new Exception("NotFound");
            }

            if (!String.IsNullOrEmpty(model.Slug))
            {
                match.Slug = model.Slug.Slugify();
            }
            else
            {
                var hometeam = _teamRepository.GetByID(model.HomeTeamId);
                var guestteam = _teamRepository.GetByID(model.GuestTeamId);

                var slug = $"{hometeam.TeamName} vs {guestteam.TeamName} {model.Time.ToString("HH:mm dd M yyyy")}".Slugify();
                match.Slug = slug;
            }

            match.Video = model.Video;
            match.MatchType = model.MatchType;
            match.Status = model.Status;
            match.Time = model.Time;
            match.HomePoints = model.HomePoints;
            match.GuestPoints = model.GuestPoints;
            match.HomeTeamId = model.HomeTeamId;
            match.GuestTeamId = model.GuestTeamId;
            match.LeagueId = model.LeagueId;
            match.Commentator = model.Commentator;
            match.UpdatedDate = DateTime.UtcNow;

            try
            {
                _matchRepository.Update(match);
                await _unitOfWork.CommitAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Update Fail : " + ex);
            }
        }

        public async Task<Match> DeleteMatch(long id)
        {
            var match = _matchRepository.GetByID(id);

            if (match == null)
            {
                throw new Exception("NotFound");
            }

            _matchRepository.Delete(match);
            await _unitOfWork.CommitAsync();
            return match;
        }

        public IEnumerable<MatchScheduleViewModel> GetMatchHistoryOfTeam(long id, int number)
        {
            Expression<Func<Match, bool>> filter = m => (m.GuestTeamId == id || m.HomeTeamId == id);

            var matchs = _matchRepository.Get(filter, a => a.OrderByDescending(a => a.CreatedDate), "League,HomeTeam,GuestTeam").Take(number).ToList();
            var result = _mapper.Map<List<MatchScheduleViewModel>>(matchs);

            return result;
        }

        public IEnumerable<MatchScheduleViewModel> GetMatchHistoryBetweenTeam(long team1Id, long team2Id, int number)
        {
            Expression<Func<Match, bool>> filter = m => (m.GuestTeamId == team1Id || m.HomeTeamId == team1Id)
            && (m.GuestTeamId == team2Id || m.HomeTeamId == team2Id);

            var matchs = _matchRepository.Get(filter, a => a.OrderByDescending(a => a.CreatedDate), "League,HomeTeam,GuestTeam").Take(number).ToList();
            var result = _mapper.Map<List<MatchScheduleViewModel>>(matchs);

            return result;
        }
    }
}
