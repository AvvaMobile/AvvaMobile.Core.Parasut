using AvvaMobile.Core.Parasut.Models.Requests;
using AvvaMobile.Core.Parasut.Models.Responses;

namespace AvvaMobile.Core.Parasut.Services;

public class Auth
{
    public string Username { get; set; }
    public string Password { get; set; }
    public string ClientID { get; set; }
    public string ClientSecret { get; set; }
    public string CompanyID { get; set; }

    private string ParasutBaseDomain { get; set; }

    public Auth(string parasutBaseDomain, string username, string password, string clientID, string clientSecret, string companyID)
    {
        Username = username;
        Password = password;
        ClientID = clientID;
        ClientSecret = clientSecret;
        CompanyID = companyID;
        ParasutBaseDomain = parasutBaseDomain;
    }

    /// <summary>
    /// Paraşüt'e bağlanarak, ilgili kullanıcı için token yaratır.
    /// </summary>
    /// <returns></returns>
    public async Task<ServiceResult<TokenResponse>> Token()
    {
        var result = new ServiceResult<TokenResponse>();

        var nm = new NetworkManager(ParasutBaseDomain);
        nm.AddContentTypeJSONHeader();

        var getTokenRequest = new
        {
            grant_type = "password",
            username = Username,
            password = Password,
            client_id = ClientID,
            client_secret = ClientSecret
        };

        var httpResponse = await nm.PostAsync<TokenResponse>("/oauth/token", getTokenRequest);
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