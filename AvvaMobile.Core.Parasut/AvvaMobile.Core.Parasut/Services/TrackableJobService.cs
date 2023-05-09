namespace AvvaMobile.Core.Parasut;

public class TrackableJobService : ParasutBaseService
{
    public TrackableJobService(Auth auth, string parasutBaseUrl) : base(auth, parasutBaseUrl)
    {
    }

    /// <summary>
    /// Get the status of a trackable job.
    /// </summary>
    /// <returns></returns>
    public async Task<ServiceResult<TrackableJobResponse>> GetStatus(TrackableJobRequest request)
    {
        var result = new ServiceResult<TrackableJobResponse>();

        var nm = new NetworkManager(ParasutBaseUrl);
        nm.AddContentTypeJSONHeader();

        var token = await Auth.Token();
        if (!token.IsSuccess)
        {
            result.SetError("Token alınamadı.");
            return result;
        }
        nm.AddBearerToken(token.Data.access_token);

        var httpResponse = await nm.PostAsync<TrackableJobResponse>("/trackable_jobs/" + request.id, request);
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