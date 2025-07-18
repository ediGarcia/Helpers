﻿using System.Net.Http;
using System.Text;
using System.Text.Json;

#pragma warning disable CS8603
#pragma warning disable CS8618

namespace HelperClasses.Classes;

public class ApiClientAction
{
    private readonly Dictionary<string, string> _headers = [];
    private readonly List<KeyValuePair<string, string>> _queryFields = [];
    private readonly string _url;
    private readonly HttpMethod _httpMethod;

    private HttpContent _content;

    internal ApiClientAction(
        HttpMethod httpVerb,
        string url,
        Dictionary<string, string> defaultHeaders
    )
    {
        _httpMethod = httpVerb;
        _url = url;

        foreach ((string key, string value) in defaultHeaders)
            _headers.Add(key, value);
    }

    #region Public Methods

    #region AddHeader
    /// <summary>
    /// Adds a new header.
    /// </summary>
    /// <param name="key"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    public ApiClientAction AddHeader(string key, string value)
    {
        _headers[key] = value;
        return this;
    }
    #endregion

    #region AddQueryField
    /// <summary>
    /// Adds a new query field.
    /// </summary>
    /// <param name="key"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    public ApiClientAction AddQueryField(string key, string value)
    {
        _queryFields.Add(new(key, value));
        return this;
    }
    #endregion

    #region SendRequest*

    #region SendRequest()
    /// <summary>
    /// Sends the request and retrieves the result.
    /// </summary>
    /// <returns></returns>
    /// <exception cref="HttpRequestException"></exception>
    public async Task<string> SendRequest()
    {
        HttpClient httpClient = new();
        string actualUrl = _url;

        if (_queryFields.Count > 0)
            actualUrl +=
                "?"
                + String.Join(
                    '&',
                    _queryFields.Select(_ =>
                        $"{Uri.EscapeDataString(_.Key)}={Uri.EscapeDataString(_.Value)}"
                    )
                );

        foreach ((string key, string value) in _headers)
            httpClient.DefaultRequestHeaders.Add(key, value);

        HttpResponseMessage response = await httpClient.SendAsync(new(_httpMethod, actualUrl){ Content = _content });
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadAsStringAsync();
    }
    #endregion

    #region SendRequest<T>()
    /// <summary>
    /// Sends the request and retrieves the deserialized result.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    /// <exception cref="HttpRequestException"></exception>
    public async Task<T> SendRequest<T>() =>
        JsonSerializer.Deserialize<T>(
            await SendRequest(),
            new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                IncludeFields = true
            }
        );
    #endregion

    #endregion

    #region SetContent*

    #region SetContent(HttpContent)
    /// <summary>
    /// Sets the content for HTTP methods that support content.
    /// </summary>
    /// <param name="content"></param>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException"></exception>
    public ApiClientAction SetContent(HttpContent content)
    {
        if (_content is not null)
            throw new InvalidOperationException("The content is already set.");

        _content = content;
        return this;
    }
    #endregion

    #region SetJsonContent(string)
    /// <summary>
    /// Sets the JSON content for HTTP methods that support content.
    /// </summary>
    /// <param name="json"></param>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException"></exception>
    public ApiClientAction SetJsonContent(string json) =>
        SetContent(new StringContent(json, Encoding.UTF8, "application/json"));
    #endregion

    #region SetJsonContent(object)
    /// <summary>
    /// Sets the JSON content for HTTP methods that support content.
    /// </summary>
    /// <param name="content"></param>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException"></exception>
    public ApiClientAction SetJsonContent(object content) =>
        SetJsonContent(JsonSerializer.Serialize(content));
    #endregion

    #endregion

    #endregion
}
