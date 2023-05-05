namespace AvvaMobile.Core.Parasut;

public class Parasut
{
    public Parasut(string username, string password, string clientID, string clientSecret, string companyID)
    {
        ParasutBaseDomain = "https://api.parasut.com";
        ParasutBaseUrl = $"{ParasutBaseDomain}/v4/{companyID}";
        Auth = new Auth(ParasutBaseDomain, username, password, clientID, clientSecret, companyID);
        Customer = new CustomerService(Auth, ParasutBaseUrl);
        Product = new ProductService(Auth, ParasutBaseUrl);
        Invoice = new InvoiceService(Auth, ParasutBaseUrl);
        InvoicePayment = new InvoicePaymentService(Auth, ParasutBaseUrl);
        CustomerPayment = new CustomerPaymentService(Auth, ParasutBaseUrl);
        EInvoiceInbox = new EInvoiceInboxService(Auth, ParasutBaseUrl);
        EInvoice = new EInvoiceService(Auth, ParasutBaseUrl);
        EArchive = new EArchiveService(Auth, ParasutBaseUrl);
        TrackableJob = new TrackableJobService(Auth, ParasutBaseUrl);
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