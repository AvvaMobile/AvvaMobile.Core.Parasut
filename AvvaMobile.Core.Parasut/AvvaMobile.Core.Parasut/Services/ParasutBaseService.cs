namespace AvvaMobile.Core.Parasut;

public class ParasutBaseService
{
    private readonly HttpClient? _httpClient;
    private ParasutHttpClient? _http;
    private string? _httpBaseUrl;

    public string ParasutBaseUrl { get; set; }
    public Auth Auth { get; set; }

    public ParasutBaseService(Auth auth, string parasutBaseUrl) : this(auth, parasutBaseUrl, null)
    {
    }

    public ParasutBaseService(Auth auth, string parasutBaseUrl, HttpClient? httpClient)
    {
        ParasutBaseUrl = parasutBaseUrl;
        Auth = auth;
        _httpClient = httpClient;
    }

    /// <summary>
    /// İstek istemcisi. ParasutBaseUrl çalışma anında değiştirilirse yeniden kurulur.
    /// </summary>
    private protected ParasutHttpClient Http
    {
        get
        {
            if (_http is null || _httpBaseUrl != ParasutBaseUrl)
            {
                _http = new ParasutHttpClient(ParasutBaseUrl, _httpClient);
                _httpBaseUrl = ParasutBaseUrl;
            }

            return _http;
        }
    }

    /// <summary>
    /// Geçerli bir access token döner. Token alınamazsa <paramref name="result"/> hata ile işaretlenir ve null döner.
    /// </summary>
    private protected async Task<string?> GetAccessTokenAsync(ParasutServiceResult result, CancellationToken cancellationToken)
    {
        var token = await Auth.Token(cancellationToken).ConfigureAwait(false);
        if (!token.IsSuccess || token.Data is null)
        {
            result.SetError(string.IsNullOrWhiteSpace(token.Message) ? "Token alınamadı." : token.Message);
            return null;
        }

        return token.Data.access_token;
    }
}

public static class Currencies
{
    public const string TRL = "TRL";
    public const string EUR = "EUR";
    public const string USD = "USD";
    public const string GBP = "GBP";
}

/// <summary>
/// custom_requirement_params içindeki additional_invoice_type alanı için geçerli değerler.
/// </summary>
public static class AdditionalInvoiceTypes
{
    public const string SAGLIK_ECZ = "SAGLIK_ECZ";
    public const string SAGLIK_HAS = "SAGLIK_HAS";
    public const string SAGLIK_OPT = "SAGLIK_OPT";
    public const string SAGLIK_MED = "SAGLIK_MED";
    public const string ABONELIK = "ABONELIK";
    public const string MAL_HIZMET = "MAL_HIZMET";
    public const string DIGER = "DIGER";
}

public static class DiscountTypes
{
    public const string percentage = "percentage";
    public const string amount = "amount";
}
