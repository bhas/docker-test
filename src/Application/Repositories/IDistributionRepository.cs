

using Domain.Entities;

public interface IDistributionRepository
{
    void Add(Distribution entity);
    Distribution Get(long id);
    List<Distribution> GetAllForConfig(long configId);
}