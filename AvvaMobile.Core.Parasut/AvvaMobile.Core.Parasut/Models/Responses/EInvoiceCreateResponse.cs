namespace AvvaMobile.Core.Parasut;

public class EInvoiceCreateResponse
{
    public EInvoiceCreateResponse_Data data { get; set; }
}

public class EInvoiceCreateResponse_Data
{
    public string id { get; set; }
    public string type { get; set; } = "trackable_jobs";
    public EInvoiceCreateResponse_Data_Attributes attributes { get; set; }
}

public class EInvoiceCreateResponse_Data_Attributes
{
    public string status { get; set; }
    public List<string> errors { get; set; }
}