namespace AvvaMobile.Core.Parasut;

public class EArchiveCreateResponse
{
    public EArchiveCreateResponse_Data data { get; set; }
}

public class EArchiveCreateResponse_Data
{
    public string id { get; set; }
    public string type { get; set; } = "trackable_jobs";
    public EArchiveCreateResponse_Data_Attributes attributes { get; set; }
}

public class EArchiveCreateResponse_Data_Attributes
{
    public string status { get; set; }
    public List<string> errors { get; set; }
}