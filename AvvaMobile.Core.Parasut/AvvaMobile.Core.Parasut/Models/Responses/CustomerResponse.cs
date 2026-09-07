using System.ComponentModel.DataAnnotations;

namespace AvvaMobile.Core.Parasut;

public class CustomerResponse
{
    public CustomerResponse_Data? data { get; set; }
}

public class CustomerResponse_Data
{
    public string? id { get; set; }
    public string type { get; set; } = "contacts";
    public CustomerResponse_Data_Attributes? attributes { get; set; }
}

public class CustomerResponse_Data_Attributes
{
    public string? email { get; set; }
    [Required]
    public string? name { get; set; }
    public string? short_name { get; set; }
    public string contact_type { get; set; } = "company";
    public string? tax_office { get; set; }
    public string? tax_number { get; set; }
    public string? district { get; set; }

    /// <summary>
    /// Posta kodu
    /// </summary>
    public string? postal_code { get; set; }

    public string? city { get; set; }
    public string? country { get; set; }
    public string? address { get; set; }
    public string? phone { get; set; }
    public string? fax { get; set; }
    public bool is_abroad { get; set; } = false;
    public bool archived { get; set; } = false;
    public string? iban { get; set; }
    [Required]
    public string account_type { get; set; } = "customer";
    public bool untrackable { get; set; } = false;

    /// <summary>
    /// Bakiye
    /// </summary>
    public decimal? balance { get; set; }

    /// <summary>
    /// TL bakiye
    /// </summary>
    public decimal? trl_balance { get; set; }

    /// <summary>
    /// USD bakiye
    /// </summary>
    public decimal? usd_balance { get; set; }

    /// <summary>
    /// EUR bakiye
    /// </summary>
    public decimal? eur_balance { get; set; }

    /// <summary>
    /// GBP bakiye
    /// </summary>
    public decimal? gbp_balance { get; set; }

    /// <summary>
    /// Kayıt tarihi
    /// </summary>
    public string? created_at { get; set; }

    /// <summary>
    /// Son güncelleme tarihi
    /// </summary>
    public string? updated_at { get; set; }
}