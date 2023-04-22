using AutoMapper;
using ChatServer.Api.ViewModels.User;
using ChatServer.Data.Entities;
using ChatServer.Shared.DTOs;
using ChatServer.Shared.DTOs.Friends;
using ChatServer.Shared.DTOs.User;

namespace ChatServer.Api.WebConfig
{
	public class AutoMapperProfile : Profile
	{
		public AutoMapperProfile()
		{
			CreateMap<AppUser, UserDataForApp>()
				.ForMember(uItem => uItem.RoleName, opts => opts.MapFrom(uEntity => uEntity.AppRole == null ? "" : uEntity.AppRole.Name));
			CreateMap<AppUser, RegisterDTO>().ReverseMap();
		}
		// Cấu hình mapping cho AccountController, action Login
		public static MapperConfiguration LoginConf = new(mapper =>
		{
			// Map dữ liệu từ AppUser sang UserListItemVM, map thuộc tính RoleName
			mapper.CreateMap<AppUser, UserDataForApp>()
				.ForMember(uItem => uItem.RoleName, opts => opts.MapFrom(uEntity => uEntity.AppRole == null ? "" : uEntity.AppRole.Name));
		});
		public static MapperConfiguration ListFriendConfig = new(mapper =>
		{
			mapper.CreateMap<AppUser, FriendDTO>();
		});
	}
}
