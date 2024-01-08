using AllrideApiCore.Dtos.Insert;
using AllrideApiCore.Dtos.ResponseDto;
using AllrideApiCore.Dtos.ResponseDtos;
using AllrideApiCore.Dtos.Select;
using AllrideApiCore.Dtos.Weather;
using AllrideApiCore.Entities;
using AllrideApiCore.Entities.Clubs;
using AllrideApiCore.Entities.Here;
using AllrideApiCore.Entities.Routes;
using AllrideApiCore.Entities.Users;
using AllrideApiCore.Entities.Weathers;
using AutoMapper;
using DTO.Insert;
using DTO.Select;
using NetTopologySuite.Geometries;
using SMSApi.Api.Response;
using System.Globalization;

namespace AllrideApiService.Configuration
{
    public class MapperProfile : Profile
    {
        public MapperProfile(GeometryFactory geometryFactory)  
        {
            CreateMap<LoginUserDto, UserEntity>().ReverseMap();
            CreateMap<CreateUserDto, UserEntity>();
            CreateMap<string, UserPassword>().ConvertUsing(new StringToClassConverter());
            CreateMap<CreateUserDto, UserDetail>().ForMember(
                x => x.Name, x => x.MapFrom(y => y.Name)).ForMember(
                x => x.LastName, x => x.MapFrom(y => y.LastName)).ForMember(
                x => x.Gender, x => x.MapFrom(y => y.Gender)).ForMember(
                x => x.Phone, x => x.MapFrom(y => y.Phone)).ForMember(
                x => x.Country, x => x.MapFrom(y => y.Country)).ForMember(dest => dest.DateOfBirth,
                   opt => opt.MapFrom(src => DateTime.ParseExact(src.DateOfBirth, "dd.MM.yyyy", CultureInfo.InvariantCulture))).ReverseMap();
            CreateMap<News, NewsRequestDto>().ReverseMap();
            CreateMap<UserDetail, UserGeneralDto>();
            CreateMap<Action, RouteInstruction>().
                ForMember(x => x.Offset, x => x.MapFrom(y => y.offset)).
                ForMember(x => x.Instruction, x => x.MapFrom(y => y.instruction));
             CreateMap<TurnByTurnAction, RouteInstructionDetail>().
                ForMember(x => x.Duration, x => x.MapFrom(y => y.duration)).
                ForMember(x => x.Leng, x => x.MapFrom(y => y.length)).
                ForMember(x => x.Direction, x => x.MapFrom(y => y.direction));
            CreateMap<WeatherResponseDto, Weather>().ReverseMap();
            CreateMap<LoginUserResponseDto, UserDetail>().ReverseMap();
            CreateMap<NewsResponseDto,News>().ReverseMap();
            CreateMap<NewsDetailResponseDto,News>().ReverseMap();
            CreateMap<CreateActionTypeNewsDto,UserNewsReaction>().ReverseMap();
            CreateMap<RouteDetailResponseDto,RouteDetail>().ReverseMap();
            CreateMap<GroupResponseDto,Group>().ReverseMap();
            CreateMap<ClubResponseDto,Club>().ReverseMap();

        }
    }
}

