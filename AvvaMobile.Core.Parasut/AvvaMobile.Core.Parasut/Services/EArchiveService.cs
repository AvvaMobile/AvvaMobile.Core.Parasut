namespace AvvaMobile.Core.Parasut;

public class EArchiveService : ParasutBaseService
{
    public EArchiveService(Auth auth, string parasutBaseUrl) : this(auth, parasutBaseUrl, null)
    {
    }

    public EArchiveService(Auth auth, string parasutBaseUrl, HttpClient? httpClient) : base(auth, parasutBaseUrl, httpClient)
    {
    }

    /// <summary>
    /// Daha önce oluşturulmuş bir satış faturasını e-arşiv faturasına dönüştürür.
    /// </summary>
    public async Task<ParasutServiceResult<EArchiveCreateResponse>> Create(EArchiveCreateRequest invoice, CancellationToken cancellationToken = default)
    {
        var result = new ParasutServiceResult<EArchiveCreateResponse>();

        var token = await GetAccessTokenAsync(result, cancellationToken).ConfigureAwait(false);
        if (token is null)
        {
            return result;
        }

        return await Http.PostAsync<EArchiveCreateResponse>("/e_archives", invoice, token, cancellationToken).ConfigureAwait(false);
    }
}
