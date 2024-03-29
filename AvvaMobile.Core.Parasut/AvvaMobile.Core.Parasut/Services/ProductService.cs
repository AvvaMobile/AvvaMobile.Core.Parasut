﻿namespace AvvaMobile.Core.Parasut;

public class ProductService : ParasutBaseService
{
    public ProductService(Auth auth, string parasutBaseUrl) : base(auth, parasutBaseUrl)
    {
    }

    /// <summary>
    /// Creates a product on Paraşüt.
    /// </summary>
    /// <returns></returns>
    public async Task<ParasutServiceResult<ProductResponse>> Create(ProductRequest product)
    {
        var result = new ParasutServiceResult<ProductResponse>();

        var nm = new NetworkManager(ParasutBaseUrl);
        nm.AddContentTypeJSONHeader();

        var token = await Auth.Token();
        if (!token.IsSuccess)
        {
            result.SetError("Token alınamadı.");
            return result;
        }
        nm.AddBearerToken(token.Data.access_token);

        var httpResponse = await nm.PostAsync<ProductResponse>("/products", product);
        if (httpResponse.IsSuccess)
        {
            result.Data = httpResponse.Data;
        }
        else
        {
            result.SetError(httpResponse.Message);
        }

        return result;
    }

    /// <summary>
    /// Edits a product on Paraşüt.
    /// </summary>
    /// <returns></returns>
    public async Task<ParasutServiceResult<ProductResponse>> Edit(ProductRequest product)
    {
        var result = new ParasutServiceResult<ProductResponse>();

        var nm = new NetworkManager(ParasutBaseUrl);
        nm.AddContentTypeJSONHeader();

        var token = await Auth.Token();
        if (!token.IsSuccess)
        {
            result.SetError("Token alınamadı.");
            return result;
        }
        nm.AddBearerToken(token.Data.access_token);

        var httpResponse = await nm.PutAsync<ProductResponse>("/products/" + product.data.id, product);
        if (httpResponse.IsSuccess)
        {
            result.Data = httpResponse.Data;
        }
        else
        {
            result.SetError(httpResponse.Message);
        }

        return result;
    }
}