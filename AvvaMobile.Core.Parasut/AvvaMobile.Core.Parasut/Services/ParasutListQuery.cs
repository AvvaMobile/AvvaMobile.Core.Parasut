using System.Text;

namespace AvvaMobile.Core.Parasut;

/// <summary>
/// Listeleme servislerinde kullanılan filtre, sıralama ve sayfalama parametreleri.
/// </summary>
public class ParasutListQuery
{
    /// <summary>
    /// Sıralama alanı. Ters sıralama için başına "-" ekleyin (örn. "-created_at").
    /// </summary>
    public string? Sort { get; set; }

    /// <summary>
    /// Kaçıncı sayfa. Paraşüt varsayılanı 1'dir.
    /// </summary>
    public int? PageNumber { get; set; }

    /// <summary>
    /// Sayfa başına kayıt sayısı. Paraşüt varsayılanı 15'tir.
    /// </summary>
    public int? PageSize { get; set; }

    /// <summary>
    /// Yanıta eklenecek ilişkili kayıtlar (örn. "category").
    /// </summary>
    public string? Include { get; set; }

    /// <summary>
    /// Ham filtreler. Tipli özellikler dışında bir filtre göndermeniz gerekirse buraya ekleyin.
    /// </summary>
    public Dictionary<string, string> Filters { get; } = new();

    /// <summary>
    /// Değer boş değilse filtreyi ekler.
    /// </summary>
    public ParasutListQuery Filter(string name, string? value)
    {
        if (!string.IsNullOrWhiteSpace(value))
        {
            Filters[name] = value!;
        }

        return this;
    }

    /// <summary>
    /// Tipli alt sınıflar, kendi özelliklerini filtreye burada aktarır.
    /// </summary>
    protected virtual void OnBuild()
    {
    }

    /// <summary>
    /// Parametreleri "?filter[x]=y&amp;page[number]=1" biçiminde sorgu dizesine çevirir.
    /// Hiç parametre yoksa boş dize döner.
    /// </summary>
    public string ToQueryString()
    {
        OnBuild();

        var parts = new List<string>();

        foreach (var filter in Filters)
        {
            parts.Add($"filter[{filter.Key}]={Uri.EscapeDataString(filter.Value)}");
        }

        if (!string.IsNullOrWhiteSpace(Sort))
        {
            parts.Add($"sort={Uri.EscapeDataString(Sort!)}");
        }

        if (PageNumber.HasValue)
        {
            parts.Add($"page[number]={PageNumber.Value}");
        }

        if (PageSize.HasValue)
        {
            parts.Add($"page[size]={PageSize.Value}");
        }

        if (!string.IsNullOrWhiteSpace(Include))
        {
            parts.Add($"include={Uri.EscapeDataString(Include!)}");
        }

        if (parts.Count == 0)
        {
            return string.Empty;
        }

        var sb = new StringBuilder("?");
        sb.Append(string.Join("&", parts));
        return sb.ToString();
    }
}

/// <summary>
/// Müşteri/tedarikçi listeleme filtreleri.
/// </summary>
public class CustomerListQuery : ParasutListQuery
{
    /// <summary>
    /// Ünvan/ad. API karşılığı: filter[name]
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// E-posta. API karşılığı: filter[email]
    /// </summary>
    public string? Email { get; set; }

    /// <summary>
    /// Vergi numarası. API karşılığı: filter[tax_number]
    /// </summary>
    public string? TaxNumber { get; set; }

    /// <summary>
    /// Vergi dairesi. API karşılığı: filter[tax_office]
    /// </summary>
    public string? TaxOffice { get; set; }

    /// <summary>
    /// Şehir. API karşılığı: filter[city]
    /// </summary>
    public string? City { get; set; }

    /// <summary>
    /// customer veya supplier. API karşılığı: filter[account_type]
    /// </summary>
    public string? AccountType { get; set; }

    protected override void OnBuild()
    {
        Filter("name", Name);
        Filter("email", Email);
        Filter("tax_number", TaxNumber);
        Filter("tax_office", TaxOffice);
        Filter("city", City);
        Filter("account_type", AccountType);
    }
}

/// <summary>
/// Ürün/hizmet listeleme filtreleri.
/// </summary>
public class ProductListQuery : ParasutListQuery
{
    /// <summary>
    /// Ürün/hizmet adı. API karşılığı: filter[name]
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// Ürün/hizmet kodu. API karşılığı: filter[code]
    /// </summary>
    public string? Code { get; set; }

    protected override void OnBuild()
    {
        Filter("name", Name);
        Filter("code", Code);
    }
}

/// <summary>
/// Satış faturası listeleme filtreleri.
/// </summary>
public class InvoiceListQuery : ParasutListQuery
{
    /// <summary>
    /// Düzenleme tarihi. API karşılığı: filter[issue_date]
    /// </summary>
    public string? IssueDate { get; set; }

    /// <summary>
    /// Son tahsilat tarihi. API karşılığı: filter[due_date]
    /// </summary>
    public string? DueDate { get; set; }

    /// <summary>
    /// Müşterinin Paraşüt ID'si. API karşılığı: filter[contact_id]
    /// </summary>
    public string? ContactId { get; set; }

    /// <summary>
    /// Fatura sıra numarası. API karşılığı: filter[invoice_id]
    /// </summary>
    public string? InvoiceId { get; set; }

    /// <summary>
    /// Fatura serisi. API karşılığı: filter[invoice_series]
    /// </summary>
    public string? InvoiceSeries { get; set; }

    /// <summary>
    /// Fatura türü: invoice, export, estimate, cancelled, refund vb. API karşılığı: filter[item_type]
    /// </summary>
    public string? ItemType { get; set; }

    /// <summary>
    /// Yazdırma durumu. API karşılığı: filter[print_status]
    /// </summary>
    public string? PrintStatus { get; set; }

    /// <summary>
    /// Tahsilat durumu: paid, overdue, unpaid, partially_paid. API karşılığı: filter[payment_status]
    /// </summary>
    public string? PaymentStatus { get; set; }

    protected override void OnBuild()
    {
        Filter("issue_date", IssueDate);
        Filter("due_date", DueDate);
        Filter("contact_id", ContactId);
        Filter("invoice_id", InvoiceId);
        Filter("invoice_series", InvoiceSeries);
        Filter("item_type", ItemType);
        Filter("print_status", PrintStatus);
        Filter("payment_status", PaymentStatus);
    }
}
