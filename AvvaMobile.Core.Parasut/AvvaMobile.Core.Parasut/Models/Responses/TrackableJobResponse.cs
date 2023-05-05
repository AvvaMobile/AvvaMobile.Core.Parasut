namespace AvvaMobile.Core.Parasut;

public class TrackableJobResponse
{
    public TrackableJobResponse_Data data { get; set; }
}

public class TrackableJobResponse_Data
{
    public string id { get; set; }
    public string type { get; set; } = "trackable_jobs";
    public TrackableJobResponse_Data_Attributes attributes { get; set; }
}

public class TrackableJobResponse_Data_Attributes
{
    public string status { get; set; }
    public List<string> errors { get; set; }
}