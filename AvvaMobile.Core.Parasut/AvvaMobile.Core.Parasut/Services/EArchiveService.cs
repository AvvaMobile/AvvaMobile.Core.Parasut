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
    /// Resmileşmiş e-arşiv faturasını ID'si ile getirir. Bu ID, Create sonrası dönen trackable job
    /// tamamlandığında elde edilir.
    /// </summary>
    public Task<ParasutServiceResult<EArchiveResponse>> Get(string id, CancellationToken cancellationToken = default)
        => GetByIdAsync<EArchiveResponse>($"/e_archives/{Uri.EscapeDataString(id)}", cancellationToken);

    /// <summary>
    /// E-arşiv faturasını PDF bağlantısını getirir. Dönen bağlantı sürelidir.
    /// </summary>
    public Task<ParasutServiceResult<EDocumentPdfResponse>> GetPdf(string id, CancellationToken cancellationToken = default)
        => GetByIdAsync<EDocumentPdfResponse>($"/e_archives/{Uri.EscapeDataString(id)}/pdf", cancellationToken);

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
