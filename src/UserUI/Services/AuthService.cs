using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace  UserUI.Services;

public class AuthService
{
    private readonly HttpClient _httpClient;
    private string? _token;
    
    public AuthService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    
    public string? GetToken => _token;
    
    public void LogOut()
    {
        _token = null;
        _httpClient.DefaultRequestHeaders.Authorization = null;
    }

    public async Task<HttpResponseMessage> RegisterAsync(string username, string email, string password)
    {
        var data = new
        {
            Username = username,
            Email = email,
            Password = password,
        };
        return await _httpClient.PostAsJsonAsync("api/users/register", data);
    }

    public async Task<string?> LoginAsync(string username, string password)
    {
        var data = new
        {
            Username = username,
            Password = password
        };
        var response = await _httpClient.PostAsJsonAsync("api/users/login",  data);

        if (response.IsSuccessStatusCode)
        {
            var token = await response.Content.ReadAsStringAsync();
            SetToken(token.Trim('"'));
            return token;
        }

        return null;
    }
    
    public void SetToken(string token)
    {
        _token = token;
        _httpClient.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", _token);
    }

}