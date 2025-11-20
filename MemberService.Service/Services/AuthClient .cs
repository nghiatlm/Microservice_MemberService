


using System.Net;
using System.Net.Http.Headers;
using System.Text.Json;

namespace MemberService.Service.Services
{
    public class AuthClient : IAuthClient
    {
        private readonly HttpClient _client;

        public AuthClient(HttpClient client)
        {
            _client = client;
        }

        public async Task<bool> AccountExistsAsync(int id, string bearerToken)
        {
            if (string.IsNullOrWhiteSpace(bearerToken)) throw new ArgumentException("Bearer token is required", nameof(bearerToken));

            var request = new HttpRequestMessage(HttpMethod.Get, $"api/v1/accounts/{id}");
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", bearerToken);

            var response = await _client.SendAsync(request);

            if (response.StatusCode == HttpStatusCode.NotFound) return false;
            if (response.StatusCode == HttpStatusCode.Unauthorized) return false;
            if (!response.IsSuccessStatusCode) return false;

            var json = await response.Content.ReadAsStringAsync();
            try
            {
                using var doc = JsonDocument.Parse(json);
                var root = doc.RootElement;
                if (root.TryGetProperty("success", out var successEl))
                {
                    if (successEl.ValueKind == JsonValueKind.False) return false;
                    if (successEl.ValueKind != JsonValueKind.True && successEl.ValueKind != JsonValueKind.False) return false;
                }
                else
                {
                    return false;
                }
                if (!root.TryGetProperty("data", out var dataEl)) return false;
                return dataEl.ValueKind != JsonValueKind.Null;
            }
            catch (JsonException)
            {
                return false;
            }
        }
    }
}