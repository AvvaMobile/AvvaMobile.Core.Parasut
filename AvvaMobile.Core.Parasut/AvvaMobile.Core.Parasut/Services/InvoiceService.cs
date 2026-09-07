namespace AvvaMobile.Core.Parasut;

public class InvoiceService : ParasutBaseService
{
    public InvoiceService(Auth auth, string parasutBaseUrl) : this(auth, parasutBaseUrl, null)
    {
    }

    public InvoiceService(Auth auth, string parasutBaseUrl, HttpClient? httpClient) : base(auth, parasutBaseUrl, httpClient)
    {
    }

    /// <summary>
    /// Tek bir satış faturasını ID'si ile getirir.
    /// </summary>
    public Task<ParasutServiceResult<InvoiceResponse>> Get(string id, CancellationToken cancellationToken = default)
        => GetByIdAsync<InvoiceResponse>($"/sales_invoices/{Uri.EscapeDataString(id)}", cancellationToken);

    /// <summary>
    /// Paraşüt'teki satış faturalarını listeler.
    /// </summary>
    public Task<ParasutServiceResult<ParasutListResponse<InvoiceResponse_Data>>> List(InvoiceListQuery? query = null, CancellationToken cancellationToken = default)
        => ListAsync<InvoiceResponse_Data>("/sales_invoices", query, cancellationToken);

    /// <summary>
    /// Bir satış faturasını siler.
    /// </summary>
    public Task<ParasutServiceResult> Delete(string id, CancellationToken cancellationToken = default)
        => DeleteAsync($"/sales_invoices/{Uri.EscapeDataString(id)}", cancellationToken);

    /// <summary>
    /// Creates an invoice on Paraşüt.
    /// </summary>
    public async Task<ParasutServiceResult<InvoiceResponse>> Create(InvoiceRequest invoice, CancellationToken cancellationToken = default)
    {
        var result = new ParasutServiceResult<InvoiceResponse>();

        var token = await GetAccessTokenAsync(result, cancellationToken).ConfigureAwait(false);
        if (token is null)
        {
            return result;
        }

        return await Http.PostAsync<InvoiceResponse>("/sales_invoices", invoice, token, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Edits an invoice on Paraşüt.
    /// </summary>
    public async Task<ParasutServiceResult<InvoiceResponse>> Edit(InvoiceRequest invoice, CancellationToken cancellationToken = default)
    {
        var result = new ParasutServiceResult<InvoiceResponse>();

        var token = await GetAccessTokenAsync(result, cancellationToken).ConfigureAwait(false);
        if (token is null)
        {
            return result;
        }

        return await Http.PutAsync<InvoiceResponse>($"/sales_invoices/{invoice.data?.id}", invoice, token, cancellationToken).ConfigureAwait(false);
    }
}
