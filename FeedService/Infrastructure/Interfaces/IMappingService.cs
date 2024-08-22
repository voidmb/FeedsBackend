using FeedService.Core.Models;
using FeedService.Infrastructure.DTOs;

namespace FeedService.Infrastructure.Interfaces
{
    public interface IMappingService
    {
        List<FeedDTO> MapFeedsToDTO(IQueryable<Feed> modelList);

        FeedDTO MapFeedToDTO(Feed model);

        Feed MapDTOToFeed(FeedDTO dto);
    }
}
