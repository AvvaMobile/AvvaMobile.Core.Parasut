namespace AvvaMobile.Core.Parasut;

public class ProductService : ParasutBaseService
{
    public ProductService(Auth auth, string parasutBaseUrl) : this(auth, parasutBaseUrl, null)
    {
    }

    public ProductService(Auth auth, string parasutBaseUrl, HttpClient? httpClient) : base(auth, parasutBaseUrl, httpClient)
    {
    }

    /// <summary>
    /// Creates a product on Paraşüt.
    /// </summary>
    public async Task<ParasutServiceResult<ProductResponse>> Create(ProductRequest product, CancellationToken cancellationToken = default)
    {
        var result = new ParasutServiceResult<ProductResponse>();

        var token = await GetAccessTokenAsync(result, cancellationToken).ConfigureAwait(false);
        if (token is null)
        {
            return result;
        }

        return await Http.PostAsync<ProductResponse>("/products", product, token, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Edits a product on Paraşüt.
    /// </summary>
    public async Task<ParasutServiceResult<ProductResponse>> Edit(ProductRequest product, CancellationToken cancellationToken = default)
    {
        var result = new ParasutServiceResult<ProductResponse>();

        var token = await GetAccessTokenAsync(result, cancellationToken).ConfigureAwait(false);
        if (token is null)
        {
            return result;
        }

        return await Http.PutAsync<ProductResponse>($"/products/{product.data?.id}", product, token, cancellationToken).ConfigureAwait(false);
    }
}
