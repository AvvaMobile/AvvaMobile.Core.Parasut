namespace AvvaMobile.Core.Parasut;

public class Parasut
{
    /// <param name="httpClient">
    /// İsteğe bağlı. Verilmezse paket kendi paylaşımlı HttpClient'ını kullanır.
    /// IHttpClientFactory kullanan uygulamalar kendi istemcilerini geçebilir.
    /// </param>
    public Parasut(string username, string password, string clientID, string clientSecret, string companyID)
        : this(username, password, clientID, clientSecret, companyID, null)
    {
    }

    public Parasut(string username, string password, string clientID, string clientSecret, string companyID, HttpClient? httpClient)
    {
        ParasutBaseDomain = "https://api.parasut.com";
        ParasutBaseUrl = $"{ParasutBaseDomain}/v4/{companyID}";
        Auth = new Auth(ParasutBaseDomain, username, password, clientID, clientSecret, companyID, httpClient);
        Customer = new CustomerService(Auth, ParasutBaseUrl, httpClient);
        Product = new ProductService(Auth, ParasutBaseUrl, httpClient);
        Invoice = new InvoiceService(Auth, ParasutBaseUrl, httpClient);
        InvoicePayment = new InvoicePaymentService(Auth, ParasutBaseUrl, httpClient);
        CustomerPayment = new CustomerPaymentService(Auth, ParasutBaseUrl, httpClient);
        EInvoiceInbox = new EInvoiceInboxService(Auth, ParasutBaseUrl, httpClient);
        EInvoice = new EInvoiceService(Auth, ParasutBaseUrl, httpClient);
        EArchive = new EArchiveService(Auth, ParasutBaseUrl, httpClient);
        TrackableJob = new TrackableJobService(Auth, ParasutBaseUrl, httpClient);
    }

    private string ParasutBaseDomain { get; set; }
    private string ParasutBaseUrl { get; set; }

    public Auth Auth { get; set; }
    public CustomerService Customer { get; set; }
    public ProductService Product { get; set; }
    public InvoiceService Invoice { get; set; }
    public InvoicePaymentService InvoicePayment { get; set; }
    public CustomerPaymentService CustomerPayment { get; set; }
    public EInvoiceInboxService EInvoiceInbox { get; set; }
    public EInvoiceService EInvoice { get; set; }
    public EArchiveService EArchive { get; set; }
    public TrackableJobService TrackableJob { get; set; }
}
