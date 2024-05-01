using Domain.Entities;
using Domain.Exceptions;

namespace Infrastructure.Database.Repositories;

public interface IDistributionRepository
{
    void Add(Distribution entity);
    Distribution Get(long id);
    List<Distribution> GetAllForConfig(long configId);
}

public class DistributionRepository(DockerTestContext context) : IDistributionRepository
{
    public void Add(Distribution entity)
    {
        entity.Date = DateTimeOffset.UtcNow;
        context.Distributions.Add(entity);
        context.SaveChanges();
    }

    public Distribution Get(long id)
    {
        var entity = context.Distributions.SingleOrDefault(x => x.Id == id);
        return entity ?? throw new NotFoundException();
    }

    public List<Distribution> GetAllForConfig(long configId)
    {
        return context.Distributions.Where(x => x.DistributionConfigId == configId).ToList();
    }
}