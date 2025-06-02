using System.Net.Http;
using System.Text;

namespace HelperClasses.Classes;

public class ApiClient
{
    #region Properties

    /// <summary>
    /// Gets the API's base url.
    /// </summary>
    public string BaseUrl { get; }

    /// <summary>
    /// Gets or sets the API's response format.
    /// </summary>
    public string ResponseType
    {
        get => _defaultHeaders["Accept"];
        set => _defaultHeaders["Accept"] = value;
    }

    #endregion

    private readonly Dictionary<string, string> _defaultHeaders = new() { { "Accept", "application/json" } }; // Keeps the headers that will be used in every request.

    /// <summary>
    /// Creates a client for an API that doesn't require authorization.
    /// </summary>
    /// <param name="baseUrl"></param>
    public ApiClient(string baseUrl) =>
        BaseUrl = baseUrl;

    /// <summary>
    /// Creates a client for an API that requires a Basic authorization.
    /// </summary>
    /// <param name="baseUrl"></param>
    /// <param name="username"></param>
    /// <param name="password"></param>
    public ApiClient(string baseUrl, string username, string password)
        : this(baseUrl)
    {
        string token = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{username}:{password}"));
        _defaultHeaders.Add("Authorization", "Basic " + token);
    }

    /// <summary>
    /// Creates a client for an API that requires a Bearer Token for authorization.
    /// </summary>
    /// <param name="baseUrl"></param>
    /// <param name="bearerToken"></param>
    public ApiClient(string baseUrl, string bearerToken)
        : this(baseUrl)
    {
        string authorizationToken = bearerToken;

        if (!authorizationToken.StartsWith("Bearer "))
            authorizationToken = "Bearer " + authorizationToken;

        _defaultHeaders.Add("Authorization", authorizationToken);
    }

    #region Public Methods

    #region AddDefaultHeader
    /// <summary>
    /// Inserts or replaces a header that will be used for every request.
    /// </summary>
    /// <param name="key"></param>
    /// <param name="value"></param>
    public void AddDefaultHeader(string key, string value) => 
        _defaultHeaders[key] = value;
    #endregion

    #region CreateNewRequest
    /// <summary>
    /// Creates a new request from the current <see cref="ApiClient"/> data targeting the specified endpoint and for the specified HTTP method.
    /// </summary>
    /// <param name="httpMethod"></param>
    /// <param name="endpoint"></param>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException"></exception>
    public ApiClientAction CreateNewRequest(HttpMethod httpMethod, string endpoint)
    {
        if (Uri.TryCreate(endpoint, UriKind.Absolute, out _))
        {
            if (!endpoint.StartsWith(BaseUrl))
                throw new InvalidOperationException($"The \"{endpoint}\" base path does not match \"{BaseUrl}\". Consider using a relative URL.");
        }
        else
            endpoint = BaseUrl + endpoint;

        return new(httpMethod, endpoint, _defaultHeaders);
    }
    #endregion

    #endregion
}