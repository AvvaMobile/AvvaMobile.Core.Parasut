namespace AvvaMobile.Core.Parasut;

public class TrackableJobService : ParasutBaseService
{
    public TrackableJobService(Auth auth, string parasutBaseUrl) : this(auth, parasutBaseUrl, null)
    {
    }

    public TrackableJobService(Auth auth, string parasutBaseUrl, HttpClient? httpClient) : base(auth, parasutBaseUrl, httpClient)
    {
    }

    /// <summary>
    /// Get the status of a trackable job.
    /// </summary>
    public async Task<ParasutServiceResult<TrackableJobResponse>> GetStatus(TrackableJobRequest request, CancellationToken cancellationToken = default)
    {
        var result = new ParasutServiceResult<TrackableJobResponse>();

        var token = await GetAccessTokenAsync(result, cancellationToken).ConfigureAwait(false);
        if (token is null)
        {
            return result;
        }

        // Paraşüt bu kaydı yalnızca GET ile döner; gövde göndermez.
        return await Http.GetAsync<TrackableJobResponse>($"/trackable_jobs/{request.id}", token, cancellationToken).ConfigureAwait(false);
    }
}
