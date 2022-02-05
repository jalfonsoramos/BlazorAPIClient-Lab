using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using BlazorAPIClient.Dtos;

namespace BlazorAPIClient.DataServices
{
    public class GrapQLSpaceXDataService : ISpaceXDataService
    {
        private readonly HttpClient _httpClient;

        public GrapQLSpaceXDataService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<LaunchDto[]> GetAllLaunchesAsync()
        {
            var queryObject = new
            {
                query = @"query{launches { id is_tentative mission_name launch_date_local}}",
                variables = new { }
            };

            var launchQuery = new StringContent(
                JsonSerializer.Serialize(queryObject),
                Encoding.UTF8,
                "application/json");

            var response = await _httpClient.PostAsync("/graphql", launchQuery);

            if (response.IsSuccessStatusCode)
            {
                var gqlData = await JsonSerializer.DeserializeAsync<GqlData>(await response.Content.ReadAsStreamAsync());

                return gqlData.Data.Launches;
            }

            return null;
        }
    }
}