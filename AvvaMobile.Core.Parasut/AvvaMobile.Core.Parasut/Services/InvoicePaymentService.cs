namespace AvvaMobile.Core.Parasut;

public class InvoicePaymentService : ParasutBaseService
{
    public InvoicePaymentService(Auth auth, string parasutBaseUrl) : base(auth, parasutBaseUrl)
    {
    }

    /// <summary>
    /// Creates a payment for an invoice on Paraşüt.
    /// </summary>
    /// <returns></returns>
    public async Task<ParasutServiceResult<InvoicePaymentResponse>> Pay(InvoicePaymentRequest payment, string invoiceId)
    {
        var result = new ParasutServiceResult<InvoicePaymentResponse>();

        var nm = new NetworkManager(ParasutBaseUrl);
        nm.AddContentTypeJSONHeader();

        var token = await Auth.Token();
        if (!token.IsSuccess)
        {
            result.SetError("Token alınamadı.");
            return result;
        }
        nm.AddBearerToken(token.Data.access_token);

        var httpResponse = await nm.PostAsync<InvoicePaymentResponse>($"/sales_invoices/{invoiceId}/payments", payment);
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