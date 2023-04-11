namespace AvvaMobile.Core.Parasut.Models.Responses;

public class CustomerPaymentResponse
{
    public CustomerPaymentResponse_Data data { get; set; }
}

public class CustomerPaymentResponse_Data
{
    public string id { get; set; }
    public string type { get; set; } = "transactions";
    public CustomerPaymentResponse_Data_Attributes attributes { get; set; }
}

public class CustomerPaymentResponse_Data_Attributes
{
    public string created_at { get; set; }

    public string updated_at { get; set; }

    /// <summary>
    /// Açıklama
    /// </summary>
    public string description { get; set; }

    /// <summary>
    /// İşlem türü
    /// </summary>
    public string transaction_type { get; set; }

    /// <summary>
    /// Tarih
    /// </summary>
    public string date { get; set; }

    /// <summary>
    /// TRL karşılığı meblağ
    /// </summary>
    public decimal amount_in_trl { get; set; }

    /// <summary>
    /// Borç meblağ
    /// </summary>
    public decimal debit_amount { get; set; }

    /// <summary>
    /// Borç döviz cinsi: TRL, USD, EUR, GBP
    /// </summary>
    public string debit_currency { get; set; }

    /// <summary>
    /// Alacak meblağ
    /// </summary>
    public decimal credit_amount { get; set; }

    /// <summary>
    /// Alacak döviz cinsi: TRL, USD, EUR, GBP
    /// </summary>
    public string credit_currency { get; set; }
}