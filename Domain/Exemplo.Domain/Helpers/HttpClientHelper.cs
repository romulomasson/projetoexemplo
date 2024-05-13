using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Security;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Exemplo.Domain.Helpers;

public class OAuth2Token
{
    public string access_token { get; set; }
    public string refresh_token { get; set; }
    public string username { get; set; }
    public long expires_in { get; set; }
}

public class HttpClientHelper
{
    public OAuth2Token OAuth2Token { get; private set; }
    public bool IsAuthenticated
    {
        get { return OAuth2Token != null && OAuth2Token.access_token != null; }
    }

    private string UrlBase { get; set; }
    private string ApiKey { get; set; }
    public bool IsAnonymous { get; set; }
    private Dictionary<string, string> Headers;

    public HttpClientHelper(string urlbase)
    {
        UrlBase = urlbase;
        Headers = new Dictionary<string, string>();
    }

    public HttpClientHelper(string urlbase, string apiKey)
    {
        UrlBase = urlbase;
        ApiKey = apiKey;
        Headers = new Dictionary<string, string>();
    }

    public void AddHeader(string name, string value)
    {
        Headers.Add(name, value);
    }

    private HttpClient GetNewHttpClient(List<KeyValuePair<string, string>> headerData = null)
    {
        var httpClient = new HttpClient();
        httpClient.BaseAddress = new Uri(UrlBase);
        httpClient.DefaultRequestHeaders.Accept.Clear();
        httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        if (IsAuthenticated && string.IsNullOrEmpty(ApiKey))
        {
            httpClient.DefaultRequestHeaders.Remove("Authorization");
            httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + OAuth2Token.access_token);
        }
        else if (!string.IsNullOrEmpty(ApiKey))
        {
            httpClient.DefaultRequestHeaders.Remove("Authorization");
            httpClient.DefaultRequestHeaders.Add("Authorization", ApiKey);
        }

        if (headerData != null)
        {
            foreach (var header in headerData)
            {
                httpClient.DefaultRequestHeaders.Remove(header.Key);
                httpClient.DefaultRequestHeaders.Add(header.Key, header.Value);
            }
        }

        if (Headers != null)
        {
            foreach (var header in Headers)
            {
                httpClient.DefaultRequestHeaders.Remove(header.Key);
                httpClient.DefaultRequestHeaders.Add(header.Key, header.Value);
            }
        }

        return httpClient;
    }
    private HttpClient GetNewHttpClient(string newUrlBase, List<KeyValuePair<string, string>> headerData = null)
    {
        var httpClient = new HttpClient();
        httpClient.BaseAddress = new Uri(newUrlBase);
        httpClient.DefaultRequestHeaders.Accept.Clear();
        httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        if (IsAuthenticated && string.IsNullOrEmpty(ApiKey))
        {
            httpClient.DefaultRequestHeaders.Remove("Authorization");
            httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + OAuth2Token.access_token);
        }
        else if (!string.IsNullOrEmpty(ApiKey))
        {
            httpClient.DefaultRequestHeaders.Remove("Authorization");
            httpClient.DefaultRequestHeaders.Add("Authorization", ApiKey);
        }

        if (headerData != null)
        {
            foreach (var header in headerData)
            {
                httpClient.DefaultRequestHeaders.Remove(header.Key);
                httpClient.DefaultRequestHeaders.Add(header.Key, header.Value);
            }
        }

        if (Headers != null)
        {
            foreach (var header in Headers)
            {
                httpClient.DefaultRequestHeaders.Remove(header.Key);
                httpClient.DefaultRequestHeaders.Add(header.Key, header.Value);
            }
        }

        return httpClient;
    }

    public async Task<OAuth2Token> AuthenticateGrantType(string route, string userName, string password)
    {
        var body = new List<KeyValuePair<string, string>>();
        body.Add(new KeyValuePair<string, string>("grant_type", "password"));
        body.Add(new KeyValuePair<string, string>("username", userName));
        body.Add(new KeyValuePair<string, string>("password", password));

        using (var HttpClient = GetNewHttpClient())
        {
            HttpResponseMessage response = await HttpClient.PostAsync(route, new FormUrlEncodedContent(body));

            if (!response.IsSuccessStatusCode)
                throw new Exception("Nome do usu�rio ou senha inv�lidos.");

            OAuth2Token = await response.Content.ReadAsAsync<OAuth2Token>();
        }

        return OAuth2Token;
    }

    public async Task<OAuth2Token> AuthenticateGrantTypeOAuth2(string route, string clientId, string clientSecret, string scope, string userName, string password, string grantType = null, string tokenPattern = null)
    {
        var body = new List<KeyValuePair<string, string>>();

        if (!string.IsNullOrEmpty(grantType))
            body.Add(new KeyValuePair<string, string>("grant_type", grantType));
        if (!string.IsNullOrEmpty(userName))
            body.Add(new KeyValuePair<string, string>("username", userName));
        if (!string.IsNullOrEmpty(password))
            body.Add(new KeyValuePair<string, string>("password", password));
        if (!string.IsNullOrEmpty(clientId))
            body.Add(new KeyValuePair<string, string>("client_id", clientId));
        if (!string.IsNullOrEmpty(clientSecret))
            body.Add(new KeyValuePair<string, string>("client_secret", clientSecret));
        if (!string.IsNullOrEmpty(scope) && scope.Contains("resource"))
            body.Add(new KeyValuePair<string, string>("resource", scope.Split("|")[1].ToString()));
        else if (!string.IsNullOrEmpty(scope))
            body.Add(new KeyValuePair<string, string>("scope", scope));

        using (var HttpClient = GetNewHttpClient())
        {
            if (HttpClient.BaseAddress.Scheme == "https")
            {
                ServicePointManager.ServerCertificateValidationCallback += ValidateRemoteCertificate;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            }

            HttpResponseMessage response = await HttpClient.PostAsync(route, new FormUrlEncodedContent(body));

            if (!response.IsSuccessStatusCode)
            {
                if (response.StatusCode == System.Net.HttpStatusCode.UnsupportedMediaType || response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    var bodyJson = JsonConvert.SerializeObject(body.ToDictionary((keyItem) => keyItem.Key, (valueItem) => valueItem.Value));
                    var httpRequestMessage = new HttpRequestMessage
                    {
                        Content = new StringContent(bodyJson, Encoding.UTF8, "application/json")
                    };
                    httpRequestMessage.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    response = await HttpClient.PostAsync(route, httpRequestMessage.Content);

                    if (!response.IsSuccessStatusCode)
                        throw new Exception("Nome do usu�rio ou senha inv�lidos.");
                }
                else throw new Exception("Nome do usu�rio ou senha inv�lidos.");
            }

            try
            {
                var data = await response.Content.ReadAsAsync<dynamic>();
                string accessTokenData = string.Empty;

                if (!string.IsNullOrEmpty(tokenPattern))
                    accessTokenData = ((Newtonsoft.Json.Linq.JValue)JToken.Parse(data.ToString()).SelectToken(tokenPattern)).Value.ToString();
                else accessTokenData = JsonConvert.DeserializeObject<dynamic>(data.ToString())["data"]["access_token"].ToString();

                if (!string.IsNullOrEmpty(accessTokenData))
                {
                    OAuth2Token = new OAuth2Token()
                    {
                        access_token = accessTokenData
                    };
                }
                else OAuth2Token = await response.Content.ReadAsAsync<OAuth2Token>();
            }
            catch (Exception)
            {
                await response.Content.ReadAsAsync<OAuth2Token>();
            }
        }

        return OAuth2Token;
    }

    private static bool ValidateRemoteCertificate(object sender, X509Certificate cert, X509Chain chain, SslPolicyErrors error)
    {
        // If the certificate is a valid, signed certificate, return true.
        if (error == System.Net.Security.SslPolicyErrors.None)
        {
            return true;
        }

        return false;
    }

    public async Task<HttpResponseMessage> Post(string route, string json)
    {
        if ((!IsAuthenticated && string.IsNullOrEmpty(ApiKey)) && (IsAnonymous = false))
            throw new AuthenticationException("Sess�o inv�lida. Fa�a nova autentica��o");

        using (var HttpClient = GetNewHttpClient())
        {
            HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await HttpClient.PostAsync(route, content);

            return response;
        }
    }

    public async Task<HttpResponseMessage> Post(string route, object data)
    {
        if ((!IsAuthenticated && string.IsNullOrEmpty(ApiKey)) && (IsAnonymous = false))
            throw new AuthenticationException("Sess�o inv�lida. Fa�a nova autentica��o");

        using (var HttpClient = GetNewHttpClient())
        {
            var json = JsonConvert.SerializeObject(data);

            HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await HttpClient.PostAsync(route, content);

            return response;
        }
    }
    public async Task<T> Post<T>(string urlBase, string route, object data)
    {
        if ((!IsAuthenticated && string.IsNullOrEmpty(ApiKey)) && (IsAnonymous = false))
            throw new AuthenticationException("Sess�o inv�lida. Fa�a nova autentica��o");

        using (var HttpClient = GetNewHttpClient(urlBase))
        {
            var json = JsonConvert.SerializeObject(data);

            HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await HttpClient.PostAsync(route, content);

            if (!response.IsSuccessStatusCode)
            {
                string retorno = response.Content?.ReadAsStringAsync()?.Result;
                throw new Exception(string.Format("WebApiClient - Erro ao realizar o POST | {0}", retorno ?? ""));
            }

            return response.Content.ReadAsAsync<T>().Result;
        }
    }

    public async Task<T> Post<T>(string route, object data)
    {
        if ((!IsAuthenticated && string.IsNullOrEmpty(ApiKey)) && (IsAnonymous = false))
            throw new AuthenticationException("Sess�o inv�lida. Fa�a nova autentica��o");

        using (var HttpClient = GetNewHttpClient())
        {
            var json = JsonConvert.SerializeObject(data);

            HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await HttpClient.PostAsync(route, content);

            if (!response.IsSuccessStatusCode)
            {
                string retorno = response.Content?.ReadAsStringAsync()?.Result;
                throw new Exception(string.Format("WebApiClient - Erro ao realizar o POST | {0}", retorno ?? ""));
            }

            return response.Content.ReadAsAsync<T>().Result;
        }
    }

    public async Task<bool> PostFormData(string route, MultipartFormDataContent data)
    {
        if ((!IsAuthenticated && string.IsNullOrEmpty(ApiKey)) && (IsAnonymous = false))
            throw new AuthenticationException("Sess�o inv�lida. Fa�a nova autentica��o");

        using (var HttpClient = GetNewHttpClient())
        {
            HttpResponseMessage response = await HttpClient.PostAsync(route, data);

            return response.IsSuccessStatusCode;
        }
    }

    public async Task<List<T>> PostList<T>(string route, dynamic data)
    {
        if ((!IsAuthenticated && string.IsNullOrEmpty(ApiKey)) && (IsAnonymous = false))
            throw new AuthenticationException("Sess�o inv�lida. Fa�a nova autentica��o");

        using (var HttpClient = GetNewHttpClient())
        {
            var json = JsonConvert.SerializeObject(data);

            HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await HttpClient.PostAsync(route, content);

            if (!response.IsSuccessStatusCode)
                throw new Exception("WebApiClient - Erro ao realizar o POST");

            return default(List<T>);
        }
    }

    public async Task<T> Put<T>(string route, T data)
    {
        if ((!IsAuthenticated && string.IsNullOrEmpty(ApiKey)) && (IsAnonymous = false))
            throw new AuthenticationException("Sess�o inv�lida. Fa�a nova autentica��o");

        using (var HttpClient = GetNewHttpClient())
        {
            var json = JsonConvert.SerializeObject(data);

            HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await HttpClient.PutAsync(route, content);

            if (!response.IsSuccessStatusCode)
                throw new Exception("WebApiClient - Erro ao realizar o PUT");

            return JsonConvert.DeserializeObject<T>(response.Content.ReadAsStringAsync().Result);
        }
    }


    public async Task<T> Delete<T>(string route)
    {
        if ((!IsAuthenticated && string.IsNullOrEmpty(ApiKey)) && (IsAnonymous = false))
            throw new AuthenticationException("Sess�o inv�lida. Fa�a nova autentica��o");

        using (var HttpClient = GetNewHttpClient())
        {
            HttpResponseMessage response = await HttpClient.DeleteAsync(route);

            if (!response.IsSuccessStatusCode)
                throw new Exception("WebApiClient - Erro ao realizar o DELETE");

            return await response.Content.ReadAsAsync<T>();
        }
    }

    public async Task<T> Get<T>(string route)
    {
        if ((!IsAuthenticated && string.IsNullOrEmpty(ApiKey)) && (IsAnonymous = false))
            throw new AuthenticationException("Sess�o inv�lida. Fa�a nova autentica��o");

        using (var HttpClient = GetNewHttpClient())
        {
            HttpResponseMessage response = await HttpClient.GetAsync(route);

            if (!response.IsSuccessStatusCode)
                throw new Exception("WebApiClient - Erro ao realizar o GET");

            return await response.Content.ReadAsAsync<T>();
        }
    }
    public async Task<T> Get<T>(string urlBase, string route)
    {
        if ((!IsAuthenticated && string.IsNullOrEmpty(ApiKey)) && (IsAnonymous = false))
            throw new AuthenticationException("Sess�o inv�lida. Fa�a nova autentica��o");

        using (var HttpClient = GetNewHttpClient(urlBase))
        {
            HttpResponseMessage response = await HttpClient.GetAsync(route);

            if (!response.IsSuccessStatusCode)
                throw new Exception("WebApiClient - Erro ao realizar o GET");

            return await response.Content.ReadAsAsync<T>();
        }
    }

    public async Task<HttpResponseMessage> GetWithResponse<T>(string route)
    {
        if ((!IsAuthenticated && string.IsNullOrEmpty(ApiKey)) && (IsAnonymous = false))
            throw new AuthenticationException("Sess�o inv�lida. Fa�a nova autentica��o");

        using (var HttpClient = GetNewHttpClient())
        {

            HttpResponseMessage response = await HttpClient.GetAsync(route);

            if (!response.IsSuccessStatusCode)
                throw new Exception("WebApiClient - Erro ao realizar o GET");

            return response;
        }
    }

    public async Task<HttpResponseMessage> GetWithBodyResponse<T>(string route, object data, List<KeyValuePair<string, string>> header = null, int timeout = 100)
    {
        if ((!IsAuthenticated && string.IsNullOrEmpty(ApiKey)) && (IsAnonymous = false))
            throw new AuthenticationException("Sess�o inv�lida. Fa�a nova autentica��o");

        using (var HttpClient = GetNewHttpClient(header))
        {
            var json = JsonConvert.SerializeObject(data);
            HttpClient.Timeout = TimeSpan.FromSeconds(timeout);
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(HttpClient.BaseAddress, route),
                Content = new StringContent(json, Encoding.UTF8, "application/json")
            };
            HttpResponseMessage response = await HttpClient.SendAsync(request);

            return response;
        }
    }

    public async Task<HttpResponseMessage> PostListWithHeader<T>(string route, dynamic data, string arrayName, List<KeyValuePair<string, string>> header, int timeout = 100)
    {
        if ((!IsAuthenticated && string.IsNullOrEmpty(ApiKey)) && (IsAnonymous = false))
            throw new AuthenticationException("Sess�o inv�lida. Fa�a nova autentica��o");

        using (var HttpClient = GetNewHttpClient(header))
        {
            string jsonData;
            var json = JsonConvert.SerializeObject(data);

            if (!string.IsNullOrEmpty(arrayName))
                jsonData = @"{" + "\"" + arrayName + "\"" + ":" + json + "}";
            else jsonData = json;

            HttpContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            HttpClient.Timeout = TimeSpan.FromSeconds(timeout);
            HttpResponseMessage response = await HttpClient.PostAsync(route, content);

            return response;
        }
    }
    public async Task<HttpResponseMessage> PostWithHeaderJson<T>(string route, string dataJson, List<KeyValuePair<string, string>> header = null, int timeout = 100)
    {
        if ((!IsAuthenticated && string.IsNullOrEmpty(ApiKey)) && (IsAnonymous = false))
            throw new AuthenticationException("Sess�o inv�lida. Fa�a nova autentica��o");

        using (var HttpClient = GetNewHttpClient(header))
        {
            HttpContent content = new StringContent(dataJson, Encoding.UTF8, "application/json");
            HttpClient.Timeout = TimeSpan.FromSeconds(timeout);
            HttpResponseMessage response = await HttpClient.PostAsync(route, content);

            return response;
        }
    }

    public async Task<HttpResponseMessage> PostWithHeader<T>(string route, object data, List<KeyValuePair<string, string>> header = null, int timeout = 100)
    {
        if ((!IsAuthenticated && string.IsNullOrEmpty(ApiKey)) && (IsAnonymous = false))
            throw new AuthenticationException("Sess�o inv�lida. Fa�a nova autentica��o");

        using (var HttpClient = GetNewHttpClient(header))
        {
            var json = JsonConvert.SerializeObject(data);

            HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpClient.Timeout = TimeSpan.FromSeconds(timeout);
            HttpResponseMessage response = await HttpClient.PostAsync(route, content);

            return response;
        }
    }
    public async Task<HttpResponseMessage> PutWithHeader<T>(string route, dynamic data, List<KeyValuePair<string, string>> header = null, int timeout = 100)
    {
        if ((!IsAuthenticated && string.IsNullOrEmpty(ApiKey)) && (IsAnonymous = false))
            throw new AuthenticationException("Sess�o inv�lida. Fa�a nova autentica��o");

        using (var HttpClient = GetNewHttpClient(header))
        {
            var json = JsonConvert.SerializeObject(data);
            HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpClient.Timeout = TimeSpan.FromSeconds(timeout);
            HttpResponseMessage response = await HttpClient.PutAsync(route, content);

            return response;
        }
    }
}



