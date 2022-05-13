using AutoMapper;
using KCMS.Domain.Advertising;
using KCMS.Domain.Article;
using KCMS.Domain.Match;
using KCMS.Domain.Team;
using KCMS.Domain.User;
using KCMS.Domain.ViewModel;

namespace KCMS.AppConfigs
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            CreateMap<ArticleInsertModel, Article>();
            CreateMap<TeamInsertModel, Team>();
            CreateMap<AdvertisingInsertModel, Advertising>();
            CreateMap<Match, MatchScheduleViewModel>()
                .ForMember(dest => dest.LeagueName, opt => opt.MapFrom(src => src.League.Name))
                .ForMember(dest => dest.HomeTeamName, opt => opt.MapFrom(src => src.HomeTeam.TeamName))
                .ForMember(dest => dest.GuestTeamName, opt => opt.MapFrom(src => src.GuestTeam.TeamName));
            CreateMap<Match, MatchViewModel>()
                .ForMember(dest => dest.LeagueName, opt => opt.MapFrom(src => src.League.Name))
                .ForMember(dest => dest.LeagueId, opt => opt.MapFrom(src => src.League.Id))
                .ForMember(dest => dest.HomeTeamName, opt => opt.MapFrom(src => src.HomeTeam.TeamName))
                .ForMember(dest => dest.HomeTeamId, opt => opt.MapFrom(src => src.HomeTeam.Id))
                .ForMember(dest => dest.GuestTeamName, opt => opt.MapFrom(src => src.GuestTeam.TeamName))
                .ForMember(dest => dest.GuestTeamId, opt => opt.MapFrom(src => src.GuestTeam.Id));
            CreateMap<MatchInsertModel, Match>();
            CreateMap<UserInsertModel, User>();
        }
    }
}
