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
    /// Ünvan/ad
    /// </summary>
    public string? name { get; set; }

    public string? email { get; set; }

    /// <summary>
    /// Vergi numarası
    /// </summary>
    public string? tax_number { get; set; }

    /// <summary>
    /// Vergi dairesi
    /// </summary>
    public string? tax_office { get; set; }

    public string? city { get; set; }

    /// <summary>
    /// customer veya supplier
    /// </summary>
    public string? account_type { get; set; }

    protected override void OnBuild()
    {
        Filter("name", name);
        Filter("email", email);
        Filter("tax_number", tax_number);
        Filter("tax_office", tax_office);
        Filter("city", city);
        Filter("account_type", account_type);
    }
}

/// <summary>
/// Ürün/hizmet listeleme filtreleri.
/// </summary>
public class ProductListQuery : ParasutListQuery
{
    /// <summary>
    /// Ürün/hizmet adı
    /// </summary>
    public string? name { get; set; }

    /// <summary>
    /// Ürün/hizmet kodu
    /// </summary>
    public string? code { get; set; }

    protected override void OnBuild()
    {
        Filter("name", name);
        Filter("code", code);
    }
}
