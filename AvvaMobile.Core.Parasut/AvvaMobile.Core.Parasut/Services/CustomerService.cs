using AvvaMobile.Core.Parasut.Models.Requests;
using AvvaMobile.Core.Parasut.Models.Responses;

namespace AvvaMobile.Core.Parasut.Services;

public class CustomerService : BaseService
{
    public CustomerService(Auth auth, string parasutBaseUrl) : base(auth, parasutBaseUrl)
    {
    }

    /// <summary>
    /// Creates a customer on Paraşüt.
    /// </summary>
    /// <returns></returns>
    public async Task<ServiceResult<CustomerResponse>> Create(CustomerRequest customer)
    {
        var result = new ServiceResult<CustomerResponse>();

        var nm = new NetworkManager(ParasutBaseUrl);
        nm.AddContentTypeJSONHeader();

        var token = await Auth.Token();
        if (!token.IsSuccess)
        {
            result.SetError("Token alınamadı.");
            return result;
        }
        nm.AddBearerToken(token.Data.access_token);

        var httpResponse = await nm.PostAsync<CustomerResponse>("/contacts", customer);
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
    /// Edits a customer on Paraşüt.
    /// </summary>
    /// <returns></returns>
    public async Task<ServiceResult<CustomerResponse>> Edit(CustomerRequest customer)
    {
        var result = new ServiceResult<CustomerResponse>();

        var nm = new NetworkManager(ParasutBaseUrl);
        nm.AddContentTypeJSONHeader();

        var token = await Auth.Token();
        if (!token.IsSuccess)
        {
            result.SetError("Token alınamadı.");
            return result;
        }
        nm.AddBearerToken(token.Data.access_token);

        var httpResponse = await nm.PutAsync<CustomerResponse>("/contacts/" + customer.data.id, customer);
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