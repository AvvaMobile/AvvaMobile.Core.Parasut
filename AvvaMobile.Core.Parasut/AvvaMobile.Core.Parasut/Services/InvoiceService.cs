using AvvaMobile.Core.Parasut.Models.Requests;
using AvvaMobile.Core.Parasut.Models.Responses;

namespace AvvaMobile.Core.Parasut.Services;

public class InvoiceService : BaseService
{
    public InvoiceService(Auth auth, string parasutBaseUrl) : base(auth, parasutBaseUrl)
    {
    }

    /// <summary>
    /// Creates a invoice on Paraşüt.
    /// </summary>
    /// <returns></returns>
    public async Task<ServiceResult<InvoiceResponse>> Create(InvoiceRequest invoice)
    {
        var result = new ServiceResult<InvoiceResponse>();

        var nm = new NetworkManager(ParasutBaseUrl);
        nm.AddContentTypeJSONHeader();

        var token = await Auth.Token();
        if (!token.IsSuccess)
        {
            result.SetError("Token alınamadı.");
            return result;
        }
        nm.AddBearerToken(token.Data.access_token);

        var httpResponse = await nm.PostAsync<InvoiceResponse>("/sales_invoices", invoice);
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
    /// Edits a invoice on Paraşüt.
    /// </summary>
    /// <returns></returns>
    public async Task<ServiceResult<InvoiceResponse>> Edit(InvoiceRequest invoice)
    {
        var result = new ServiceResult<InvoiceResponse>();

        var nm = new NetworkManager(ParasutBaseUrl);
        nm.AddContentTypeJSONHeader();

        var token = await Auth.Token();
        if (!token.IsSuccess)
        {
            result.SetError("Token alınamadı.");
            return result;
        }
        nm.AddBearerToken(token.Data.access_token);

        var httpResponse = await nm.PutAsync<InvoiceResponse>("/sales_invoices/" + invoice.data.id, invoice);
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