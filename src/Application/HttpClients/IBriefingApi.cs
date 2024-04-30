using Domain.Entities;

namespace Application.HttpClients;

public interface IBriefingApi
{
    Task<IReadOnlyList<BriefingDto>> GetBriefingsAsync(HashSet<string> names);
}
