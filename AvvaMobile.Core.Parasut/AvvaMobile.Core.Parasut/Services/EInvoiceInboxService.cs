namespace AvvaMobile.Core.Parasut;

public class EInvoiceInboxService : ParasutBaseService
{
    public EInvoiceInboxService(Auth auth, string parasutBaseUrl) : base(auth, parasutBaseUrl)
    {
    }

    /// <summary>
    /// VKN bilgisi verilmiş olan firmanın bir e-fatura gelen kutusu olup olmadığını sorgular.
    /// </summary>
    /// <returns></returns>
    public async Task<ServiceResult<EInvoiceInboxResponse>> List(string vkn)
    {
        var result = new ServiceResult<EInvoiceInboxResponse>();

        var nm = new NetworkManager(ParasutBaseUrl);
        nm.AddContentTypeJSONHeader();

        var token = await Auth.Token();
        if (!token.IsSuccess)
        {
            result.SetError("Token alınamadı.");
            return result;
        }
        nm.AddBearerToken(token.Data.access_token);

        var httpResponse = await nm.GetAsync<EInvoiceInboxResponse>($"/e_invoice_inboxes?filter[vkn]={vkn}");
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