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
