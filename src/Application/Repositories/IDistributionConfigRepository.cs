
namespace Domain.Entities;

public interface IDistributionConfigRepository
{
    void Add(DistributionConfig entity);
    DistributionConfig Get(long id);
    List<DistributionConfig> GetScheduledConfigs();
    void Delete(long id);
    void Deactivate(DistributionConfig entity);
}