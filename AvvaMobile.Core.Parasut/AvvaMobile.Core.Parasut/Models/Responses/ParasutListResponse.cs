namespace AvvaMobile.Core.Parasut;

/// <summary>
/// Listeleme servislerinin ortak yanıt zarfı.
/// </summary>
public class ParasutListResponse<T>
{
    public List<T>? data { get; set; }

    public ParasutListMeta? meta { get; set; }
}

/// <summary>
/// Sayfalama bilgileri.
/// </summary>
public class ParasutListMeta
{
    /// <summary>
    /// Bulunulan sayfa
    /// </summary>
    public int current_page { get; set; }

    /// <summary>
    /// Toplam sayfa sayısı
    /// </summary>
    public int total_pages { get; set; }

    /// <summary>
    /// Toplam kayıt sayısı
    /// </summary>
    public int total_count { get; set; }
}
