using AutoMapper;
using FeedService.Core.Models;
using FeedService.Infrastructure.DTOs;

namespace FeedService.Infrastructure.Common
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Feed, FeedDTO>().ReverseMap();
        }
    }
}
