namespace AvvaMobile.Core.Parasut;

public class EInvoiceService : ParasutBaseService
{
    public EInvoiceService(Auth auth, string parasutBaseUrl) : this(auth, parasutBaseUrl, null)
    {
    }

    public EInvoiceService(Auth auth, string parasutBaseUrl, HttpClient? httpClient) : base(auth, parasutBaseUrl, httpClient)
    {
    }

    /// <summary>
    /// Resmileşmiş e-faturayı ID'si ile getirir. Bu ID, Create sonrası dönen trackable job
    /// tamamlandığında elde edilir.
    /// </summary>
    public Task<ParasutServiceResult<EInvoiceResponse>> Get(string id, CancellationToken cancellationToken = default)
        => GetByIdAsync<EInvoiceResponse>($"/e_invoices/{Uri.EscapeDataString(id)}", cancellationToken);

    /// <summary>
    /// E-faturayı PDF bağlantısını getirir. Dönen bağlantı sürelidir.
    /// </summary>
    public Task<ParasutServiceResult<EDocumentPdfResponse>> GetPdf(string id, CancellationToken cancellationToken = default)
        => GetByIdAsync<EDocumentPdfResponse>($"/e_invoices/{Uri.EscapeDataString(id)}/pdf", cancellationToken);

    /// <summary>
    /// Daha önce oluşturulmuş bir satış faturasını e-faturaya dönüştürür.
    /// </summary>
    public async Task<ParasutServiceResult<EInvoiceCreateResponse>> Create(EInvoiceCreateRequest invoice, CancellationToken cancellationToken = default)
    {
        var result = new ParasutServiceResult<EInvoiceCreateResponse>();

        var token = await GetAccessTokenAsync(result, cancellationToken).ConfigureAwait(false);
        if (token is null)
        {
            return result;
        }

        return await Http.PostAsync<EInvoiceCreateResponse>("/e_invoices", invoice, token, cancellationToken).ConfigureAwait(false);
    }
}
