namespace AvvaMobile.Core.Parasut;

public class Auth
{
    /// <summary>
    /// Token'ın süresi dolmadan önce yenilenmeye başlanacağı güvenlik payı.
    /// </summary>
    private static readonly TimeSpan ExpiryMargin = TimeSpan.FromSeconds(60);

    private readonly ParasutHttpClient _http;
    private readonly SemaphoreSlim _tokenLock = new(1, 1);

    private TokenResponse? _cachedToken;
    private DateTimeOffset _cachedTokenExpiresAt;

    private string _username;
    private string _password;
    private string _clientID;
    private string _clientSecret;

    // Kimlik bilgileri değiştirilirse önbellekteki token artık geçerli sayılmaz.
    public string Username
    {
        get => _username;
        set { _username = value; InvalidateToken(); }
    }

    public string Password
    {
        get => _password;
        set { _password = value; InvalidateToken(); }
    }

    public string ClientID
    {
        get => _clientID;
        set { _clientID = value; InvalidateToken(); }
    }

    public string ClientSecret
    {
        get => _clientSecret;
        set { _clientSecret = value; InvalidateToken(); }
    }

    public string CompanyID { get; set; }

    public Auth(string parasutBaseDomain, string username, string password, string clientID, string clientSecret, string companyID)
        : this(parasutBaseDomain, username, password, clientID, clientSecret, companyID, null)
    {
    }

    public Auth(string parasutBaseDomain, string username, string password, string clientID, string clientSecret, string companyID, HttpClient? httpClient)
    {
        _username = username;
        _password = password;
        _clientID = clientID;
        _clientSecret = clientSecret;
        CompanyID = companyID;
        _http = new ParasutHttpClient(parasutBaseDomain, httpClient);
    }

    /// <summary>
    /// Önbellekteki token'ı geçersiz kılar; sonraki çağrı Paraşüt'ten yeni token alır.
    /// </summary>
    public void InvalidateToken()
    {
        _cachedToken = null;
        _cachedTokenExpiresAt = default;
    }

    /// <summary>
    /// Paraşüt'e bağlanarak, ilgili kullanıcı için token yaratır. Geçerli bir token varsa yeniden istek yapılmaz.
    /// </summary>
    public async Task<ParasutServiceResult<TokenResponse>> Token(CancellationToken cancellationToken = default)
    {
        var cached = _cachedToken;
        if (cached is not null && DateTimeOffset.UtcNow < _cachedTokenExpiresAt)
        {
            return new ParasutServiceResult<TokenResponse> { Data = cached };
        }

        await _tokenLock.WaitAsync(cancellationToken).ConfigureAwait(false);
        try
        {
            // Kilidi beklerken başka bir çağrı token'ı yenilemiş olabilir.
            cached = _cachedToken;
            if (cached is not null && DateTimeOffset.UtcNow < _cachedTokenExpiresAt)
            {
                return new ParasutServiceResult<TokenResponse> { Data = cached };
            }

            var request = new
            {
                grant_type = "password",
                username = Username,
                password = Password,
                client_id = ClientID,
                client_secret = ClientSecret
            };

            var result = await _http.PostAsync<TokenResponse>("/oauth/token", request, cancellationToken: cancellationToken).ConfigureAwait(false);
            if (result.IsSuccess && result.Data is not null)
            {
                _cachedToken = result.Data;
                _cachedTokenExpiresAt = DateTimeOffset.UtcNow.AddSeconds(result.Data.expires_in) - ExpiryMargin;
            }

            return result;
        }
        finally
        {
            _tokenLock.Release();
        }
    }
}
