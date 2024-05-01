using Domain.Entities;
using Domain.Exceptions;

namespace Infrastructure.Database.Repositories;

public interface IDistributionConfigRepository
{
    void Add(DistributionConfig entity);
    DistributionConfig Get(long id);
    void Delete(long id);
}

public class DistributionConfigRepository(DockerTestContext context) : IDistributionConfigRepository
{
    public void Add(DistributionConfig entity)
    {
        entity.CreatedDate = DateTimeOffset.UtcNow;
        entity.CreatedBy = "Username read from session";
        entity.LastModifiedDate = DateTimeOffset.UtcNow;
        entity.LastModifiedBy = "Username read from session";
        context.DistributionConfigs.Add(entity);
        context.SaveChanges();
    }

    public DistributionConfig Get(long id)
    {
        var entity = context.DistributionConfigs.SingleOrDefault(x => x.Id == id && !x.Deleted);
        return entity ?? throw new NotFoundException();
    }

    public void Delete(long id)
    {
        var entity = context.DistributionConfigs.Find(id) ?? throw new NotFoundException();
        entity.Deleted = true;
        context.SaveChanges();
    }
}
