namespace AvvaMobile.Core.Parasut.Models.Responses;

public class InvoicePaymentResponse
{
    public InvoicePaymentResponse_Data data { get; set; }
}

public class InvoicePaymentResponse_Data
{
    public string id { get; set; }
    public string type { get; set; } = "payments";
    public InvoicePaymentResponse_Data_Attributes attributes { get; set; }
}

public class InvoicePaymentResponse_Data_Attributes
{
    public string created_at { get; set; }
    public string updated_at { get; set; }

    /// <summary>
    /// Ödeme/Tahsilat tarihi
    /// </summary>
    public string date { get; set; }

    /// <summary>
    /// Ödeme/Tahsilat tutarı
    /// </summary>
    public decimal amount { get; set; }

    /// <summary>
    /// Para birimi
    /// </summary>
    public string currency { get; set; }

    public string notes { get; set; }
}