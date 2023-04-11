namespace AvvaMobile.Core.Parasut.Models.Responses;

public class EInvoiceInboxResponse
{
    public List<EInvoiceInboxResponse_Data> data { get; set; }
}

public class EInvoiceInboxResponse_Data
{
    public string id { get; set; }
    public string type { get; set; } = "e_invoice_inboxes";
    public EInvoiceInboxResponse_Data_Attributes attributes { get; set; }
}

public class EInvoiceInboxResponse_Data_Attributes
{
    public string created_at { get; set; }
    public string updated_at { get; set; }
    public string name { get; set; }
    public string vkn { get; set; }
    public string e_invoice_address { get; set; }
    public string inbox_type { get; set; }
    public string address_registered_at { get; set; }
    public string registered_at { get; set; }
}