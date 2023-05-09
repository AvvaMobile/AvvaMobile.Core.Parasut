namespace AvvaMobile.Core.Parasut;

public class CustomerPaymentService : ParasutBaseService
{
    public CustomerPaymentService(Auth auth, string parasutBaseUrl) : base(auth, parasutBaseUrl)
    {
    }

    /// <summary>
    /// Creates a payment for an customer on Paraşüt.
    /// </summary>
    /// <returns></returns>
    public async Task<ServiceResult<CustomerPaymentResponse>> ContactDebitTransactions(CustomerPaymentRequest payment, string customerId)
    {
        var result = new ServiceResult<CustomerPaymentResponse>();

        var nm = new NetworkManager(ParasutBaseUrl);
        nm.AddContentTypeJSONHeader();

        var token = await Auth.Token();
        if (!token.IsSuccess)
        {
            result.SetError("Token alınamadı.");
            return result;
        }
        nm.AddBearerToken(token.Data.access_token);

        var httpResponse = await nm.PostAsync<CustomerPaymentResponse>($"/contacts/{customerId}/contact_debit_transactions", payment);
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