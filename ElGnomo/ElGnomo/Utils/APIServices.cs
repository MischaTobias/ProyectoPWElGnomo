using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace ElGnomo.Utils;

public class APIServices
{
    private readonly int Timeout = 30;
    private string Url = default!;
    //private readonly string BaseUri = "https://localhost:7297/api/";
    private readonly string BaseUri = "https://localhost/ElGnomoAPI/api/";
    private HttpClient _client = new();
    private readonly HttpClientHandler _clientHandler = new();
    private readonly IHttpContextAccessor _accessor;
    private readonly HttpStatusCode[] ErrorCodes = new[] { HttpStatusCode.BadRequest, HttpStatusCode.InternalServerError };

    public APIServices(IHttpContextAccessor accessor)
    {
        _accessor = accessor;
        _clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
        _client = new(_clientHandler)
        {
            Timeout = TimeSpan.FromSeconds(Timeout)
        };

        var token = _accessor.HttpContext!.Session.GetString("Token");
        _client.DefaultRequestHeaders.Authorization = null;
        _client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
    }

    public APIServices SetModule(string controllerName)
    {
        Url = $"{BaseUri}{controllerName}/";
        return this;
    }

    public async Task<T?> Get<T>(string path = "")
    {
        var response = await _client.GetAsync(Url + path);
        if (ErrorCodes.Contains(response.StatusCode))
        {
            throw new Exception(response.StatusCode.ToString());
        }
        return JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync());
    }

    public async Task<T?> Post<T>(T content, string path = "")
    {
        var json = JsonConvert.SerializeObject(content);
        var jsonContent = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await _client.PostAsync(Url + path, jsonContent);
        if (ErrorCodes.Contains(response.StatusCode))
        {
            throw new Exception(response.StatusCode.ToString());
        }
        return JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync());
    }

    public async Task<T?> Post<T>(object content, string path = "")
    {
        var json = JsonConvert.SerializeObject(content);
        var jsonContent = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await _client.PostAsync(Url + path, jsonContent);
        if (ErrorCodes.Contains(response.StatusCode))
        {
            throw new Exception(response.StatusCode.ToString());
        }
        return JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync());
    }

    public async Task<T?> Put<T>(T content, string path = "")
    {
        var json = JsonConvert.SerializeObject(content);
        var jsonContent = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await _client.PutAsync(Url + path, jsonContent);
        if (ErrorCodes.Contains(response.StatusCode))
        {
            throw new Exception(response.StatusCode.ToString());
        }
        return JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync());
    }

    public async Task Delete(string path = "")
    {
        var response = await _client.DeleteAsync(Url + path);
        if (ErrorCodes.Contains(response.StatusCode))
        {
            throw new Exception(response.StatusCode.ToString());
        }
    }
}
