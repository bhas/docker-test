namespace Application.Utilities;
public interface ICache
{
    bool TryGetValue<T>(string key, out T? cacheValue);
    void Set<T>(string key, T value, TimeSpan duration);
    void Remove(string key);
}
