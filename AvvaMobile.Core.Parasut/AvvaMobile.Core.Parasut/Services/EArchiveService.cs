namespace AvvaMobile.Core.Parasut;

public class EArchiveService : ParasutBaseService
{
    public EArchiveService(Auth auth, string parasutBaseUrl) : base(auth, parasutBaseUrl)
    {
    }

    /// <summary>
    /// Creates a invoice on Paraşüt.
    /// </summary>
    /// <returns></returns>
    public async Task<ParasutServiceResult<EArchiveCreateResponse>> Create(EArchiveCreateRequest invoice)
    {
        var result = new ParasutServiceResult<EArchiveCreateResponse>();

        var nm = new NetworkManager(ParasutBaseUrl);
        nm.AddContentTypeJSONHeader();

        var token = await Auth.Token();
        if (!token.IsSuccess)
        {
            result.SetError("Token alınamadı.");
            return result;
        }
        nm.AddBearerToken(token.Data.access_token);

        var httpResponse = await nm.PostAsync<EArchiveCreateResponse>("/e_archives", invoice);
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