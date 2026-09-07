namespace AvvaMobile.Core.Parasut;

/// <summary>
/// Resmileşmiş e-faturanın bilgileri.
/// </summary>
public class EInvoiceResponse
{
    public EInvoiceResponse_Data? data { get; set; }
}

public class EInvoiceResponse_Data
{
    public string? id { get; set; }
    public string type { get; set; } = "e_invoices";
    public EInvoiceResponse_Data_Attributes? attributes { get; set; }
}

public class EInvoiceResponse_Data_Attributes
{
    public string? external_id { get; set; }
    public string? uuid { get; set; }
    public string? env_uuid { get; set; }

    /// <summary>
    /// Gönderen e-fatura adresi
    /// </summary>
    public string? from_address { get; set; }

    /// <summary>
    /// Gönderen vergi numarası
    /// </summary>
    public string? from_vkn { get; set; }

    /// <summary>
    /// Alıcı e-fatura adresi
    /// </summary>
    public string? to_address { get; set; }

    /// <summary>
    /// Alıcı vergi numarası
    /// </summary>
    public string? to_vkn { get; set; }

    /// <summary>
    /// Yön: inbound, outbound
    /// </summary>
    public string? direction { get; set; }

    public string? note { get; set; }

    public string? response_type { get; set; }

    public string? contact_name { get; set; }

    /// <summary>
    /// Gönderim senaryosu: basic, commercial
    /// </summary>
    public string? scenario { get; set; }

    public string? status { get; set; }

    public string? gtb_ref_no { get; set; }

    public string? gtb_registration_no { get; set; }

    public string? gtb_export_date { get; set; }

    public string? response_note { get; set; }

    /// <summary>
    /// Düzenleme tarihi
    /// </summary>
    public string? issue_date { get; set; }

    public bool is_expired { get; set; }

    public bool is_answerable { get; set; }

    /// <summary>
    /// Genel toplam
    /// </summary>
    public decimal net_total { get; set; }

    public string? currency { get; set; }

    public string? item_type { get; set; }

    public string? created_at { get; set; }

    public string? updated_at { get; set; }
}
