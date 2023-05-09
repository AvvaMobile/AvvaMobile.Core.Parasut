namespace AvvaMobile.Core.Parasut;

public class EInvoiceService : ParasutBaseService
{
    public EInvoiceService(Auth auth, string parasutBaseUrl) : base(auth, parasutBaseUrl)
    {
    }

    /// <summary>
    /// Creates a invoice on Paraşüt.
    /// </summary>
    /// <returns></returns>
    public async Task<ServiceResult<EInvoiceCreateResponse>> Create(EInvoiceCreateRequest invoice)
    {
        var result = new ServiceResult<EInvoiceCreateResponse>();

        var nm = new NetworkManager(ParasutBaseUrl);
        nm.AddContentTypeJSONHeader();

        var token = await Auth.Token();
        if (!token.IsSuccess)
        {
            result.SetError("Token alınamadı.");
            return result;
        }
        nm.AddBearerToken(token.Data.access_token);

        var httpResponse = await nm.PostAsync<EInvoiceCreateResponse>("/e_invoices", invoice);
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