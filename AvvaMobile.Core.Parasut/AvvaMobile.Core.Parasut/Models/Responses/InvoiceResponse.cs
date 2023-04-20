using System.ComponentModel.DataAnnotations;

namespace AvvaMobile.Core.Parasut;

public class InvoiceResponse
{
    public InvoiceResponse_Data data { get; set; }
}

public class InvoiceResponse_Data
{
    public string id { get; set; }
    public string type { get; set; } = "sales_invoices";
    public InvoiceResponse_Data_Attributes attributes { get; set; }
    public InvoiceResponse_Data_Relationships relationships { get; set; }
}

public class InvoiceResponse_Data_Attributes
{
    public bool archived { get; set; }

    /// <summary>
    /// Fatura no
    /// </summary>
    public string invoice_no { get; set; }

    /// <summary>
    /// Genel Toplam
    /// </summary>
    public decimal net_total { get; set; }

    /// <summary>
    /// Ara toplam
    /// </summary>
    public decimal gross_total { get; set; }

    /// <summary>
    /// Stopaj
    /// </summary>
    public decimal withholding { get; set; }

    public decimal total_excise_duty { get; set; }

    public decimal total_communications_tax { get; set; }

    /// <summary>
    /// Toplam KDV
    /// </summary>
    public decimal total_vat { get; set; }

    /// <summary>
    /// Tevkifat
    /// </summary>
    public decimal vat_withholding { get; set; }

    /// <summary>
    /// Toplam indirim
    /// </summary>
    public decimal total_discount { get; set; }

    public decimal total_invoice_discount { get; set; }

    /// <summary>
    /// Vergiler Hariç Toplam
    /// </summary>
    public decimal before_taxes_total { get; set; }

    /// <summary>
    /// Ödenmemiş tutar
    /// </summary>
    public decimal remaining { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public decimal remaining_in_trl { get; set; }

    /// <summary>
    /// Tahsilat durumu: paid, overdue, unpaid, partially_paid
    /// </summary>
    public string payment_status { get; set; }

    public string created_at { get; set; }

    public string updated_at { get; set; }

    /// <summary>
    /// Fatura türü: invoice, export, estimate, cancelled, recurring_invoice, recurring_estimate, recurring_export, refund
    /// </summary>
    public string item_type { get; set; }

    /// <summary>
    /// Fatura açıklaması
    /// </summary>
    public string description { get; set; }

    /// <summary>
    /// Düzenleme tarihi
    /// </summary>
    public string issue_date { get; set; }

    /// <summary>
    /// Son tahsilat tarihi
    /// </summary>
    public string due_date { get; set; }

    /// <summary>
    /// Fatura seri
    /// </summary>
    public string invoice_series { get; set; }

    ///// <summary>
    ///// Fatura sıra
    ///// </summary>
    //public int invoice_id { get; set; }

    ///// <summary>
    ///// Döviz tipi: TRL, USD, EUR, GBP
    ///// </summary>
    //public string currency { get; set; }

    ///// <summary>
    ///// Döviz kuru
    ///// </summary>
    //public decimal exchange_rate { get; set; }

    ///// <summary>
    ///// Stopaj oranı
    ///// </summary>
    //public decimal withholding_rate { get; set; }

    ///// <summary>
    ///// Tevkifat oranı
    ///// </summary>
    //public decimal vat_withholding_rate { get; set; }

    ///// <summary>
    ///// percentage, amount
    ///// </summary>
    //public string invoice_discount_type { get; set; }

    ///// <summary>
    ///// 
    ///// </summary>
    //public decimal invoice_discount { get; set; }

    ///// <summary>
    ///// Gönderim adresi
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

    /// <summary>
    /// 
    /// </summary>
    public string country { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public string city { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public string district { get; set; }

    /// <summary>
    /// Alıcı yurt dışı bilgisi
    /// </summary>
    public bool is_abroad { get; set; } = false;

    /// <summary>
    /// 
    /// </summary>
    public string order_no { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public string order_date { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public string shipment_addres { get; set; }

    /// <summary>
    /// İrsaliyeli fatura
    /// </summary>
    public bool? shipment_included { get; set; }

    ///// <summary>
    ///// Peşin satış
    ///// </summary>
    public bool cash_sale { get; set; }
}

public class InvoiceResponse_Data_Relationships
{
    public InvoiceResponse_Data_Relationships_Contact contact { get; set; }
    public InvoiceResponse_Data_Relationships_Details details { get; set; }
}

public class InvoiceResponse_Data_Relationships_Details
{
    public List<InvoiceResponse_Data_Relationships_Details_Data> data { get; set; }
}

public class InvoiceResponse_Data_Relationships_Details_Data
{
    public string id { get; set; }
    public string type { get; set; } = "sales_invoice_details";
}

public class InvoiceResponse_Data_Relationships_Contact
{
    public InvoiceResponse_Data_Relationships_Contact_Data data { get; set; }
}

public class InvoiceResponse_Data_Relationships_Contact_Data
{
    [Required]
    public string id { get; set; }
    public string type { get; set; } = "contacts";
}