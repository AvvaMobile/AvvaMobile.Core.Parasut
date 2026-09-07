using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace AvvaMobile.Core.Parasut;

/// <summary>
/// Paraşüt API'ına yapılan HTTP isteklerini yöneten dahili istemci.
/// </summary>
internal sealed class ParasutHttpClient
{
    /// <summary>
    /// Dışarıdan bir HttpClient verilmediğinde kullanılan, uygulama ömrü boyunca paylaşılan istemci.
    /// </summary>
    private static readonly HttpClient SharedClient = new();

    internal static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNameCaseInsensitive = true,
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
        // Paraşüt bir sayıyı string olarak dönerse ("123.45") çözümleme hata vermesin.
        NumberHandling = JsonNumberHandling.AllowReadingFromString
    };

    /// <summary>
    /// Paraşüt hata gövdesi: {"errors":[{"title":"...","detail":"..."}]}
    /// </summary>
    private sealed class ErrorEnvelope
    {
        public List<ParasutError>? errors { get; set; }
    }

    private readonly HttpClient _client;
    private readonly string _baseUrl;

    public ParasutHttpClient(string baseUrl, HttpClient? client = null)
    {
        _baseUrl = baseUrl.TrimEnd('/');
        _client = client ?? SharedClient;
    }

    public Task<ParasutServiceResult<T>> GetAsync<T>(string path, string? bearerToken = null, CancellationToken cancellationToken = default)
        => SendAsync<T>(HttpMethod.Get, path, null, bearerToken, cancellationToken);

    public Task<ParasutServiceResult<T>> PostAsync<T>(string path, object? body, string? bearerToken = null, CancellationToken cancellationToken = default)
        => SendAsync<T>(HttpMethod.Post, path, body, bearerToken, cancellationToken);

    public Task<ParasutServiceResult<T>> PutAsync<T>(string path, object? body, string? bearerToken = null, CancellationToken cancellationToken = default)
        => SendAsync<T>(HttpMethod.Put, path, body, bearerToken, cancellationToken);

    public Task<ParasutServiceResult<T>> PatchAsync<T>(string path, object? body, string? bearerToken = null, CancellationToken cancellationToken = default)
        => SendAsync<T>(new HttpMethod("PATCH"), path, body, bearerToken, cancellationToken);

    /// <summary>
    /// Gövde döndürmeyen silme isteği. Paraşüt başarılı silmede 204 döner.
    /// </summary>
    public async Task<ParasutServiceResult> DeleteAsync(string path, string? bearerToken = null, CancellationToken cancellationToken = default)
    {
        var response = await SendAsync<object>(HttpMethod.Delete, path, null, bearerToken, cancellationToken).ConfigureAwait(false);

        return new ParasutServiceResult
        {
            IsSuccess = response.IsSuccess,
            Message = response.Message,
            Errors = response.Errors
        };
    }

    private async Task<ParasutServiceResult<T>> SendAsync<T>(HttpMethod method, string path, object? body, string? bearerToken, CancellationToken cancellationToken)
    {
        var result = new ParasutServiceResult<T>();

        try
        {
            using var request = new HttpRequestMessage(method, _baseUrl + path);

            if (!string.IsNullOrWhiteSpace(bearerToken))
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", bearerToken);
            }

            if (body is not null)
            {
                var json = JsonSerializer.Serialize(body, body.GetType(), JsonOptions);
                request.Content = new StringContent(json, Encoding.UTF8, "application/json");
            }

            using var response = await _client.SendAsync(request, cancellationToken).ConfigureAwait(false);
            var responseBody = await response.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false);

            if (!response.IsSuccessStatusCode)
            {
                SetHttpError(result, response, responseBody);
                return result;
            }

            if (!string.IsNullOrWhiteSpace(responseBody))
            {
                result.Data = JsonSerializer.Deserialize<T>(responseBody, JsonOptions);
            }
        }
        catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
        {
            throw;
        }
        catch (Exception ex)
        {
            result.SetError(ex.Message);
        }

        return result;
    }

    /// <summary>
    /// Hata gövdesini yapılandırılmış olarak çözmeye çalışır; çözemezse ham gövdeyi mesaja koyar.
    /// </summary>
    private static void SetHttpError(ParasutServiceResult result, HttpResponseMessage response, string responseBody)
    {
        var prefix = $"{(int)response.StatusCode} {response.ReasonPhrase}";

        if (!string.IsNullOrWhiteSpace(responseBody))
        {
            try
            {
                var envelope = JsonSerializer.Deserialize<ErrorEnvelope>(responseBody, JsonOptions);
                if (envelope?.errors is { Count: > 0 })
                {
                    result.Errors = envelope.errors;
                    var detail = string.Join(" | ", envelope.errors.Select(e =>
                        string.IsNullOrWhiteSpace(e.detail) ? e.title : $"{e.title}: {e.detail}"));

                    result.SetError($"{prefix}: {detail}");
                    return;
                }
            }
            catch (JsonException)
            {
                // Gövde JSON değil; aşağıda ham haliyle kullanılır.
            }
        }

        result.SetError($"{prefix}: {responseBody}");
    }
}
