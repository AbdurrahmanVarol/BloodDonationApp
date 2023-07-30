namespace BloodDonationApp.Business.Caching;
public interface ICache
{
    void Set(string key, object value, TimeSpan expiration);

    T? Get<T>(string key) where T : class;

    void Remove(string key);
}
