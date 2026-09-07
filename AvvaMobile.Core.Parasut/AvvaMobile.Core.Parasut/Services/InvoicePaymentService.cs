namespace AvvaMobile.Core.Parasut;

public class InvoicePaymentService : ParasutBaseService
{
    public InvoicePaymentService(Auth auth, string parasutBaseUrl) : this(auth, parasutBaseUrl, null)
    {
    }

    public InvoicePaymentService(Auth auth, string parasutBaseUrl, HttpClient? httpClient) : base(auth, parasutBaseUrl, httpClient)
    {
    }

    /// <summary>
    /// Kaydedilmiş bir ödemeyi (kasa/banka hareketini) ID'si ile getirir.
    /// Ödeme kaydının ID'si Pay metodunun döndürdüğü data.id değeridir.
    /// </summary>
    public Task<ParasutServiceResult<TransactionResponse>> Get(string transactionId, CancellationToken cancellationToken = default)
        => GetByIdAsync<TransactionResponse>($"/transactions/{Uri.EscapeDataString(transactionId)}", cancellationToken);

    /// <summary>
    /// Kaydedilmiş bir ödemeyi siler.
    /// </summary>
    public Task<ParasutServiceResult> Delete(string transactionId, CancellationToken cancellationToken = default)
        => DeleteAsync($"/transactions/{Uri.EscapeDataString(transactionId)}", cancellationToken);

    /// <summary>
    /// Bir satış faturasına ödeme kaydeder.
    /// </summary>
    public async Task<ParasutServiceResult<InvoicePaymentResponse>> Pay(InvoicePaymentRequest payment, string invoiceId, CancellationToken cancellationToken = default)
    {
        var result = new ParasutServiceResult<InvoicePaymentResponse>();

        var token = await GetAccessTokenAsync(result, cancellationToken).ConfigureAwait(false);
        if (token is null)
        {
            return result;
        }

        return await Http.PostAsync<InvoicePaymentResponse>($"/sales_invoices/{invoiceId}/payments", payment, token, cancellationToken).ConfigureAwait(false);
    }
}
