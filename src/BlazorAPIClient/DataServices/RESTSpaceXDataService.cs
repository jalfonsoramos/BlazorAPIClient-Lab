using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using BlazorAPIClient.Dtos;

namespace BlazorAPIClient.DataServices
{
    public class RESTSpaceXDataService : ISpaceXDataService
    {
        private readonly HttpClient _httpClient;

        public RESTSpaceXDataService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<LaunchDto[]> GetAllLaunchesAsync()
        {
            return await _httpClient.GetFromJsonAsync<LaunchDto[]>("/rest/launches");
        }
    }
}