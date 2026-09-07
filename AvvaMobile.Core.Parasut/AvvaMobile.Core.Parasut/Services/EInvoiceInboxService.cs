namespace AvvaMobile.Core.Parasut;

public class EInvoiceInboxService : ParasutBaseService
{
    public EInvoiceInboxService(Auth auth, string parasutBaseUrl) : this(auth, parasutBaseUrl, null)
    {
    }

    public EInvoiceInboxService(Auth auth, string parasutBaseUrl, HttpClient? httpClient) : base(auth, parasutBaseUrl, httpClient)
    {
    }

    /// <summary>
    /// VKN bilgisi verilmiş olan firmanın bir e-fatura gelen kutusu olup olmadığını sorgular.
    /// </summary>
    public async Task<ParasutServiceResult<EInvoiceInboxResponse>> List(string vkn, CancellationToken cancellationToken = default)
    {
        var result = new ParasutServiceResult<EInvoiceInboxResponse>();

        var token = await GetAccessTokenAsync(result, cancellationToken).ConfigureAwait(false);
        if (token is null)
        {
            return result;
        }

        return await Http.GetAsync<EInvoiceInboxResponse>($"/e_invoice_inboxes?filter[vkn]={Uri.EscapeDataString(vkn)}", token, cancellationToken).ConfigureAwait(false);
    }
}
