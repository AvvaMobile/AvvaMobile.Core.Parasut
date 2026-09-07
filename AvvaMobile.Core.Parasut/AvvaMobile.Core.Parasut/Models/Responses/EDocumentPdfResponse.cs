namespace AvvaMobile.Core.Parasut;

/// <summary>
/// e-Fatura / e-Arşiv PDF bağlantısı.
/// </summary>
public class EDocumentPdfResponse
{
    public EDocumentPdfResponse_Data? data { get; set; }
}

public class EDocumentPdfResponse_Data
{
    public string? id { get; set; }
    public string type { get; set; } = "e_document_pdfs";
    public EDocumentPdfResponse_Data_Attributes? attributes { get; set; }
}

public class EDocumentPdfResponse_Data_Attributes
{
    /// <summary>
    /// PDF'in indirilebileceği adres
    /// </summary>
    public string? url { get; set; }

    /// <summary>
    /// Bağlantının geçerlilik süresi
    /// </summary>
    public string? expires_at { get; set; }
}
