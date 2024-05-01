using Domain.Entities;
using Domain.Exceptions;

namespace Infrastructure.Database.Repositories;

public class DistributionConfigRepository(DockerTestContext context) : IDistributionConfigRepository
{
    public void Add(DistributionConfig entity)
    {
        entity.CreatedDate = DateTimeOffset.UtcNow;
        entity.Active = true;
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

    public List<DistributionConfig> GetScheduledConfigs()
    {
        return context.DistributionConfigs.Where(x => x.TriggerType == TriggerType.Scheduled && !x.Deleted && x.Active).ToList();
    }

    public void Deactivate(DistributionConfig entity)
    {
        entity.Active = false;
        context.SaveChanges();
    }

    public void Delete(long id)
    {
        var entity = context.DistributionConfigs.SingleOrDefault(x => x.Id == id && !x.Deleted) ?? throw new NotFoundException();
        entity.LastModifiedBy = "Username who deleted";
        entity.LastModifiedDate = DateTimeOffset.UtcNow;
        entity.Deleted = true;
        context.SaveChanges();
    }
}
