namespace AvvaMobile.Core.Parasut.Services;

public class BaseService
{
    public string ParasutBaseUrl { get; set; }
    public Auth Auth { get; set; }

    public BaseService(Auth auth, string parasutBaseUrl)
    {
        ParasutBaseUrl = parasutBaseUrl;
        Auth = auth;
    }
}

public static class Currencies
{
    public const string TRL = "TRL";
    public const string EUR = "EUR";
    public const string USD = "USD";
    public const string GBP = "GBP";
}

public static class DiscountTypes
{
    public const string percentage = "percentage";
    public const string amount = "amount";
}