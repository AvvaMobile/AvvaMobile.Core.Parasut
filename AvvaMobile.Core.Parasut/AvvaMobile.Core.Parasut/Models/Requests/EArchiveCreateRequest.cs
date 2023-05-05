namespace AvvaMobile.Core.Parasut;

public class EArchiveCreateRequest
{
    public EArchiveCreateRequest_Data data { get; set; }
}

public class EArchiveCreateRequest_Data
{
    public string id { get; set; }
    public string type { get; set; } = "e_invoices";
    public EArchiveCreateRequest_Data_Attributes attributes { get; set; }
    public EArchiveCreateRequest_Data_Relationships relationships { get; set; }
}

public class EArchiveCreateRequest_Data_Attributes
{
    /// <summary>
    /// Tevkifat oranına ait vergi kodu. - Bu dosyada ilgili kodları bulabilirsiniz: https://ebelge.gib.gov.tr/dosyalar/kilavuzlar/UBLTR_1.2.1_Kilavuzlar.zip
    /// </summary>
    public string vat_withholding_code { get; set; }

    /// <summary>
    /// KDV'si %0 olan hizmet ve ürünlerin KDV muafiyet sebebi kodu. - Bu dosyada ilgili kodları bulabilirsiniz: https://ebelge.gib.gov.tr/dosyalar/kilavuzlar/UBLTR_1.2.1_Kilavuzlar.zip
    /// </summary>
    public string vat_exemption_reason_code { get; set; }

    /// <summary>
    /// Eğer KDV muafiyet sebebi kodu 250 veya 350 ise KDV muafiyet sebebi açıklaması.
    /// </summary>
    public string vat_exemption_reason { get; set; }

    /// <summary>
    /// Fatura notu
    /// </summary>
    public string note { get; set; }

    /// <summary>
    /// ÖTV kodları - Özel tüketim vergisi faturadaki her kalem için ayrıdır. ÖTV uygulanan her ürün için ÖTV kodu göndermeniz gerekmektedir.
    /// </summary>
    public List<EArchiveCreateRequest_Data_Attributes_ExciseDutyCode> excise_duty_codes { get; set; }
}

public class EArchiveCreateRequest_Data_Attributes_ExciseDutyCode
{
    public int product { get; set; }
    public string sales_excise_duty_code { get; set; }
}

public class EArchiveCreateRequest_Data_Relationships
{
    public EArchiveCreateRequest_Data_Relationships_Invoice sales_invoice { get; set; }
}

public class EArchiveCreateRequest_Data_Relationships_Invoice
{
    public EArchiveCreateRequest_Data_Relationships_Invoice_Data data { get; set; }
}

public class EArchiveCreateRequest_Data_Relationships_Invoice_Data

{
    public string id { get; set; }
    public string type { get; set; } = "sales_invoices";
}