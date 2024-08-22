using AutoMapper;
using FeedService.Core.Models;
using FeedService.Infrastructure.DTOs;
using FeedService.Infrastructure.Interfaces;

namespace FeedService.Infrastructure.Services
{
    public class MappingService : IMappingService
    {
        private readonly IMapper _mapper;

        public MappingService(IMapper mapper)
        {
            _mapper = mapper;
        }

        public List<FeedDTO> MapFeedsToDTO(IQueryable<Feed> modelList)
        {
            return _mapper.Map<List<FeedDTO>>(modelList);
        }

        public FeedDTO MapFeedToDTO(Feed model)
        {
            return _mapper.Map<FeedDTO>(model);
        }

        public Feed MapDTOToFeed(FeedDTO dto)
        {
            return _mapper.Map<Feed>(dto);
        }
    }
}
