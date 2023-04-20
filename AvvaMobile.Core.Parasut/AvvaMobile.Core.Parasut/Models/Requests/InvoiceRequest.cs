using System.ComponentModel.DataAnnotations;

namespace AvvaMobile.Core.Parasut;

public class InvoiceRequest
{
    public InvoiceRequest_Data data { get; set; }
}

public class InvoiceRequest_Data
{
    public string id { get; set; }
    public string type { get; set; } = "sales_invoices";
    public InvoiceRequest_Data_Attributes attributes { get; set; }
    public InvoiceRequest_Data_Relationships relationships { get; set; }
}

public class InvoiceRequest_Data_Attributes
{
    /// <summary>
    /// Fatura türü
    /// </summary>
    [Required]
    public string item_type { get; set; } = "invoice";

    /// <summary>
    /// Fatura açıklaması
    /// </summary>
    public string description { get; set; }

    /// <summary>
    /// Düzenleme tarihi
    /// </summary>
    [Required]
    public string issue_date { get; set; }

    /// <summary>
    /// Son tahsilat tarihi
    /// </summary>
    public string due_date { get; set; }

    /// <summary>
    /// Fatura seri
    /// </summary>
    public string invoice_series { get; set; }

    /// <summary>
    /// Fatura sıra
    /// </summary>
    public int? invoice_id { get; set; }

    /// <summary>
    /// Döviz tipi: TRL, USD, EUR, GBP
    /// </summary>
    public string currency { get; set; } = "TRL";

    /// <summary>
    /// Döviz kuru
    /// </summary>
    public decimal exchange_rate { get; set; }

    /// <summary>
    /// Stopaj oranı
    /// </summary>
    public decimal withholding_rate { get; set; }

    /// <summary>
    /// Tevkifat oranı
    /// </summary>
    public decimal vat_withholding_rate { get; set; }

    /// <summary>
    /// percentage, amount
    /// </summary>
    public string invoice_discount_type { get; set; } = DiscountTypes.percentage;

    public decimal invoice_discount { get; set; }

    /// <summary>
    /// Gönderim adresi
    /// </summary>
    public string billing_address { get; set; }

    /// <summary>
    /// Gönderim adresi telefonu
    /// </summary>
    public string billing_phone { get; set; }

    /// <summary>
    /// Gönderim adresi faksı
    /// </summary>
    public string billing_fax { get; set; }

    /// <summary>
    /// Müşteri vergi dairesi
    /// </summary>
    public string tax_office { get; set; }

    /// <summary>
    /// Müşteri vergi numarası
    /// </summary>
    public string tax_number { get; set; }

    public string country { get; set; }

    public string city { get; set; }

    public string district { get; set; }

    /// <summary>
    /// Alıcı yurt dışı bilgisi
    /// </summary>
    public bool is_abroad { get; set; } = false;

    public string order_no { get; set; }

    public string order_date { get; set; }

    public string shipment_addres { get; set; }

    /// <summary>
    /// İrsaliyeli fatura
    /// </summary>
    public bool? shipment_included { get; set; }

    /// <summary>
    /// Peşin satış
    /// </summary>
    public bool? cash_sale { get; set; } = false;

    /// <summary>
    /// Peşin faturalar için ödeme oluşturma / güncellemede kullanılır.
    /// </summary>
    public decimal? payment_account_id { get; set; }

    /// <summary>
    /// Peşin faturalar için ödeme oluşturma / güncellemede kullanılır.
    /// </summary>
    public string payment_date { get; set; }

    public string payment_description { get; set; }
}

public class InvoiceRequest_Data_Relationships
{
    public InvoiceRequest_Data_Relationships_Contact contact { get; set; }
    public InvoiceRequest_Data_Relationships_Details details { get; set; }
}

public class InvoiceRequest_Data_Relationships_Details
{
    public List<InvoiceRequest_Data_Relationships_Details_Data> data { get; set; }
}

public class InvoiceRequest_Data_Relationships_Details_Data
{
    public string id { get; set; }
    public string type { get; set; } = "sales_invoice_details";
    public InvoiceRequest_Data_Relationships_Details_Data_Attributes attributes { get; set; }
    public InvoiceRequest_Data_Relationships_Details_Data_Relationships relationships { get; set; }
}

public class InvoiceRequest_Data_Relationships_Details_Data_Attributes
{
    /// <summary>
    /// Miktar
    /// </summary>
    [Required]
    public decimal quantity { get; set; }

    /// <summary>
    /// Birim fiyatı
    /// </summary>
    [Required]
    public decimal unit_price { get; set; }

    /// <summary>
    /// KDV oranı
    /// </summary>
    [Required]
    public decimal vat_rate { get; set; }

    /// <summary>
    /// İndirim türü: percentage, amount
    /// </summary>
    public string discount_type { get; set; } = DiscountTypes.percentage;

    public decimal discount_value { get; set; }

    /// <summary>
    /// ÖTV tipi: percentage, amount
    /// </summary>
    public string excise_duty_type { get; set; } = DiscountTypes.percentage;

    public decimal excise_duty_value { get; set; }

    /// <summary>
    /// ÖİV oranı
    /// </summary>
    public decimal communications_tax_rate { get; set; }

    /// <summary>
    /// Açıklama
    /// </summary>
    public string description { get; set; }

    /// <summary>
    /// Teslim Şartı: CFR, CIF, CIP, CPT, DAF, DDP, DDU, DEQ, DES, EXW, FAS, FCA, FOB, DAP, DAT
    /// </summary>
    public string delivery_method { get; set; }

    /// <summary>
    /// Gönderim Şekli: Belirtilmedi, Denizyolu, Demiryolu, Karayolu, Havayolu, Posta, Çok araçlı, Sabit taşıma tesisleri, İç su taşımacılığı
    /// </summary>
    public string shipping_method { get; set; }
}

public class InvoiceRequest_Data_Relationships_Details_Data_Relationships
{
    public InvoiceRequest_Data_Relationships_Details_Data_Relationships_Product product { get; set; }
}

public class InvoiceRequest_Data_Relationships_Details_Data_Relationships_Product
{
    public InvoiceRequest_Data_Relationships_Details_Data_Relationships_Product_Data data { get; set; }
}

public class InvoiceRequest_Data_Relationships_Details_Data_Relationships_Product_Data
{
    public string id { get; set; }
    public string type { get; set; } = "products";
}

public class InvoiceRequest_Data_Relationships_Contact
{
    public InvoiceRequest_Data_Relationships_Contact_Data data { get; set; }
}

public class InvoiceRequest_Data_Relationships_Contact_Data
{
    [Required]
    public string id { get; set; }
    public string type { get; set; } = "contacts";
}