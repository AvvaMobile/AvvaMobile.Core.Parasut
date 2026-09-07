namespace AvvaMobile.Core.Parasut;

/// <summary>
/// Resmileşmiş e-arşiv faturasının bilgileri.
/// </summary>
public class EArchiveResponse
{
    public EArchiveResponse_Data? data { get; set; }
}

public class EArchiveResponse_Data
{
    public string? id { get; set; }
    public string type { get; set; } = "e_archives";
    public EArchiveResponse_Data_Attributes? attributes { get; set; }
}

public class EArchiveResponse_Data_Attributes
{
    public string? uuid { get; set; }

    /// <summary>
    /// Vergi numarası
    /// </summary>
    public string? vkn { get; set; }

    /// <summary>
    /// Fatura numarası
    /// </summary>
    public string? invoice_number { get; set; }

    public string? note { get; set; }

    public bool is_printed { get; set; }

    public string? status { get; set; }

    public string? printed_at { get; set; }

    /// <summary>
    /// Bu tarihe kadar iptal edilebilir
    /// </summary>
    public string? cancellable_until { get; set; }

    public bool is_signed { get; set; }

    public string? created_at { get; set; }

    public string? updated_at { get; set; }
}
