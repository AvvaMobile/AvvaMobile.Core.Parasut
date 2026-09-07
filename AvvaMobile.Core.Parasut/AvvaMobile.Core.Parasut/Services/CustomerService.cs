namespace AvvaMobile.Core.Parasut;

public class CustomerService : ParasutBaseService
{
    public CustomerService(Auth auth, string parasutBaseUrl) : this(auth, parasutBaseUrl, null)
    {
    }

    public CustomerService(Auth auth, string parasutBaseUrl, HttpClient? httpClient) : base(auth, parasutBaseUrl, httpClient)
    {
    }

    /// <summary>
    /// Tek bir müşteriyi/tedarikçiyi ID'si ile getirir.
    /// </summary>
    public Task<ParasutServiceResult<CustomerResponse>> Get(string id, CancellationToken cancellationToken = default)
        => GetByIdAsync<CustomerResponse>($"/contacts/{Uri.EscapeDataString(id)}", cancellationToken);

    /// <summary>
    /// Bir müşteriyi/tedarikçiyi siler.
    /// </summary>
    public Task<ParasutServiceResult> Delete(string id, CancellationToken cancellationToken = default)
        => DeleteAsync($"/contacts/{Uri.EscapeDataString(id)}", cancellationToken);

    /// <summary>
    /// Paraşüt'teki müşterileri/tedarikçileri listeler.
    /// </summary>
    public Task<ParasutServiceResult<ParasutListResponse<CustomerResponse_Data>>> List(CustomerListQuery? query = null, CancellationToken cancellationToken = default)
        => ListAsync<CustomerResponse_Data>("/contacts", query, cancellationToken);

    /// <summary>
    /// Creates a customer on Paraşüt.
    /// </summary>
    public async Task<ParasutServiceResult<CustomerResponse>> Create(CustomerRequest customer, CancellationToken cancellationToken = default)
    {
        var result = new ParasutServiceResult<CustomerResponse>();

        var token = await GetAccessTokenAsync(result, cancellationToken).ConfigureAwait(false);
        if (token is null)
        {
            return result;
        }

        return await Http.PostAsync<CustomerResponse>("/contacts", customer, token, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Edits a customer on Paraşüt.
    /// </summary>
    public async Task<ParasutServiceResult<CustomerResponse>> Edit(CustomerRequest customer, CancellationToken cancellationToken = default)
    {
        var result = new ParasutServiceResult<CustomerResponse>();

        var token = await GetAccessTokenAsync(result, cancellationToken).ConfigureAwait(false);
        if (token is null)
        {
            return result;
        }

        return await Http.PutAsync<CustomerResponse>($"/contacts/{customer.data?.id}", customer, token, cancellationToken).ConfigureAwait(false);
    }
}
