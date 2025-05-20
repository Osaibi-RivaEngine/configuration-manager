using ConfigManagerApp.API.Models;
using static ConfigManagerApp.API.Repos.ConfigRepo;

namespace ConfigManagerApp.API.Repos;

public interface IConfigRepo
{
    IEnumerable<KeyValuePairModel> GetAll();
    KeyValuePairModel Add(string keyValuePair);
    IEnumerable<KeyValuePairModel> Filter(string keyValuePair);
    IEnumerable<KeyValuePairModel> Sort(SortType type);
    void Delete();
}