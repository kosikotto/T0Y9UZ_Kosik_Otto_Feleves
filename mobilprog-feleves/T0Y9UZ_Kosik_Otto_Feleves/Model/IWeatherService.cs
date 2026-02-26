
namespace T0Y9UZ_Kosik_Otto_Feleves.Model
{
    public interface IWeatherService
    {
        Task<bool> EnsureLocationPermissionAsync();
        Task<string> FetchDataAsnyc();
        Task<string> FetchDataAsnyc(string location);
        List<object> ParseData(string responseMessage);
    }
}