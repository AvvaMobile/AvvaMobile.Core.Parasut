using AvvaMobile.Core.Parasut.Services;

namespace AvvaMobile.Core.Parasut.Models.Requests;

public class ProductRequest
{
    public ProductRequest_Data data { get; set; }
}

public class ProductRequest_Data
{
    public string id { get; set; }
    public string type { get; set; } = "products";
    public ProductRequest_DataAttributes attributes { get; set; }
}

public class ProductRequest_DataAttributes
{
    /// <summary>
    /// Ürün/hizmet kodu
    /// </summary>
    public string code { get; set; }

    /// <summary>
    /// Ürün/hizmet ismi
    /// </summary>
    public string name { get; set; }

    /// <summary>
    /// KDV oranı
    /// </summary>
    public int vat_rate { get; set; }

    /// <summary>
    /// Satış ÖTV
    /// </summary>
    public int sales_excise_duty { get; set; } = 0;

    /// <summary>
    /// Satış ÖTV tipi
    /// </summary>
    public string sales_excise_duty_type { get; set; } = "percentage";

    /// <summary>
    /// Alış ÖTV
    /// </summary>
    public int purchase_excise_duty { get; set; } = 0;

    /// <summary>
    /// Alış ÖTV tipi
    /// </summary>
    public string purchase_excise_duty_type { get; set; } = "percentage";

    /// <summary>
    /// Birim
    /// </summary>
    public string unit { get; set; }

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
    /// Satış döviz: Default TRL
    /// </summary>
    public string currency { get; set; } = Currencies.TRL;

    /// <summary>
    /// Alış fiyatı
    /// </summary>
    public decimal buying_price { get; set; }

    /// <summary>
    /// Alış döviz: Default TRL
    /// </summary>
    public string buying_currency { get; set; } = Currencies.TRL;

    public bool inventory_tracking { get; set; } = true;

    /// <summary>
    /// Başlangıç Stok Miktarı. Birden fazla deponuz varsa başlangıç stok miktarını, depo stok seviyesi (inventory_level) relationship'i üzerinden depo bazında göndermelisiniz.
    /// </summary>
    public int initial_stock_count { get; set; } = 0;

    /// <summary>
    /// Ürünün GTIP kodu - https://uygulama.gtb.gov.tr/Tara adresinden öğrenebilirsiniz
    /// </summary>
    public string gtip { get; set; }

    public string barcode { get; set; }
}