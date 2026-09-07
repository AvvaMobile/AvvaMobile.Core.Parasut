namespace AvvaMobile.Core.Parasut;

public class CustomerPaymentService : ParasutBaseService
{
    public CustomerPaymentService(Auth auth, string parasutBaseUrl) : this(auth, parasutBaseUrl, null)
    {
    }

    public CustomerPaymentService(Auth auth, string parasutBaseUrl, HttpClient? httpClient) : base(auth, parasutBaseUrl, httpClient)
    {
    }

    /// <summary>
    /// Bir müşteri için borç/tahsilat hareketi oluşturur.
    /// </summary>
    public async Task<ParasutServiceResult<CustomerPaymentResponse>> ContactDebitTransactions(CustomerPaymentRequest payment, string customerId, CancellationToken cancellationToken = default)
    {
        var result = new ParasutServiceResult<CustomerPaymentResponse>();

        var token = await GetAccessTokenAsync(result, cancellationToken).ConfigureAwait(false);
        if (token is null)
        {
            return result;
        }

        return await Http.PostAsync<CustomerPaymentResponse>($"/contacts/{customerId}/contact_debit_transactions", payment, token, cancellationToken).ConfigureAwait(false);
    }
}
