namespace AvvaMobile.Core.Parasut;

public class ProductResponse
{
    public ProductResponse_Data? data { get; set; }
}

public class ProductResponse_Data
{
    public string? id { get; set; }
    public string type { get; set; } = "products";
    public ProductResponse_DataAttributes? attributes { get; set; }
}

public class ProductResponse_DataAttributes
{
    public string? created_at { get; set; }

    /// <summary>
    /// Son güncelleme tarihi.
    /// </summary>
    public string? updated_at { get; set; }

    /// <summary>
    /// KULLANMAYIN - yazım hatası. Paraşüt alanı "updated_at" olduğu için 1.1.x sürümlerinde
    /// bu alan her zaman null dönüyordu. updated_at kullanın.
    /// </summary>
    [System.Text.Json.Serialization.JsonIgnore]
    public string? updated_ay
    {
        get => updated_at;
        set => updated_at = value;
    }

    /// <summary>
    /// Ürün/hizmet kodu
    /// </summary>
    public string? code { get; set; }

    /// <summary>
    /// Ürün/hizmet ismi
    /// </summary>
    public string? name { get; set; }

    /// <summary>
    /// KDV oranı
    /// </summary>
    public decimal vat_rate { get; set; }

    /// <summary>
    /// Satış ÖTV
    /// </summary>
    public decimal sales_excise_duty { get; set; }

    /// <summary>
    /// Satış ÖTV tipi
    /// </summary>
    public string? sales_excise_duty_type { get; set; }

    public string? sales_excise_duty_code { get; set; }

    /// <summary>
    /// Alış ÖTV
    /// </summary>
    public decimal purchase_excise_duty { get; set; }

    /// <summary>
    /// Alış ÖTV tipi
    /// </summary>
    public string? purchase_excise_duty_type { get; set; }

    /// <summary>
    /// Birim
    /// </summary>
    public string? unit { get; set; }

    /// <summary>
    /// ÖİV oranı
    /// </summary>
    public decimal communications_tax_rate { get; set; }

    public bool archived { get; set; } = false;

    /// <summary>
    /// Satış fiyatı
    /// </summary>
    public decimal list_price { get; set; }

    /// <summary>
    /// Satış döviz
    /// </summary>
    public string? currency { get; set; }

    /// <summary>
    /// Alış fiyatı
    /// </summary>
    public decimal buying_price { get; set; }

    /// <summary>
    /// Alış döviz
    /// </summary>
    public string? buying_currency { get; set; }

    public bool inventory_tracking { get; set; } = true;

    /// <summary>
    /// Başlangıç Stok Miktarı. Birden fazla deponuz varsa başlangıç stok miktarını, depo stok seviyesi (inventory_level) relationship'i üzerinden depo bazında göndermelisiniz.
    /// </summary>
    public decimal initial_stock_count { get; set; }

    /// <summary>
    /// Ürünün GTIP kodu - https://uygulama.gtb.gov.tr/Tara adresinden öğrenebilirsiniz
    /// </summary>
    public string? gtip { get; set; }

    public string? barcode { get; set; }

    /// <summary>
    /// Stok adedi.
    /// </summary>
    public decimal? stock_count { get; set; }

    /// <summary>
    /// Satış fiyatının TL karşılığı.
    /// </summary>
    public decimal? list_price_in_trl { get; set; }

    /// <summary>
    /// Alış fiyatının TL karşılığı.
    /// </summary>
    public decimal? buying_price_in_trl { get; set; }

    public int? sales_invoice_details_count { get; set; }

    public int? purchase_invoice_details_count { get; set; }
}