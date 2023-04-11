namespace AvvaMobile.Core.Parasut.Models.Requests;

public class CustomerPaymentRequest
{
    public CustomerPaymentRequest_Data data { get; set; }
}

public class CustomerPaymentRequest_Data
{
    public string id { get; set; }
    public string type { get; set; } = "transactions";
    public CustomerPaymentRequest_Data_Attributes attributes { get; set; }
}

public class CustomerPaymentRequest_Data_Attributes
{
    /// <summary>
    /// Ödeme/Tahsilat Açıklaması
    /// </summary>
    public string description { get; set; }

    /// <summary>
    /// Kasa veya Banka - Bu parametre ayrıca ödemenin/tahsilatın hangi döviz kuru ile yapılacağını belirler.
    /// </summary>
    public int account_id { get; set; }

    /// <summary>
    /// Ödeme/Tahsilat tarihi
    /// </summary>
    public string date { get; set; }

    /// <summary>
    /// Ödeme/Tahsilat tutarı
    /// </summary>
    public decimal amount { get; set; }

    /// <summary>
    /// Döviz kuru - Eğer ödeme/tahsilat, faturadan farklı bir döviz kuru ile yapılacaksa; döviz kurunun TL karşılığını belirtin. Eğer ödeme/tahsilat, fatura ile aynı döviz kuru ile yapılacaksa; "1.0" değerini girin veya boş bırakın.
    /// </summary>
    public decimal? exchange_rate { get; set; }

    /// <summary>
    /// Satış Faturası ID'leri - Tahsilatınızın öncelikli olarak eşleşmesini istediğiniz faturalar
    /// </summary>
    public List<int> payable_ids { get; set; }
}