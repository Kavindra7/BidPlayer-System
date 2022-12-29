
// AutoMapping.cs
using AutoMapper;
using ASPRad.Models;
public class AutoMapping : Profile
{
    public AutoMapping()
    {
    		CreateMap<BidPlayersAddDTO, Bid_Players>();
		CreateMap<BidPlayersEditDTO, Bid_Players>();
		CreateMap<MyPlayersAddDTO, My_Players>();
		CreateMap<MyPlayersEditDTO, My_Players>();
		CreateMap<PermissionsAddDTO, Permissions>();
		CreateMap<PermissionsEditDTO, Permissions>();
		CreateMap<RegisterWithTrophyAddDTO, Register_With_Trophy>();
		CreateMap<RegisterWithTrophyEditDTO, Register_With_Trophy>();
		CreateMap<RolesAddDTO, Roles>();
		CreateMap<RolesEditDTO, Roles>();
		CreateMap<TeamsAddDTO, Teams>();
		CreateMap<TeamsEditDTO, Teams>();
		CreateMap<TrophiesAddDTO, Trophies>();
		CreateMap<TrophiesEditDTO, Trophies>();
		CreateMap<UsersRegisterDTO, Users>();
		CreateMap<UsersAccounteditDTO, Users>();
		CreateMap<UsersAddDTO, Users>();
		CreateMap<UsersEditDTO, Users>();
    }
}
