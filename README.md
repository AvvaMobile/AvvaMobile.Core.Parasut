

# Paraşüt

Geliştiriciler için Paraşüt API'ları ile uğraşmadan, kolay ve hızlı şekilde kullanabilmelerini sağlamak amacıyla geliştirilmiştir.

Paraşüt'ün orijinal API dokümanlarına ulaşmak için [https://apidocs.parasut.com](https://apidocs.parasut.com) adresini kullanabilirsiniz.

## Gerekli Bilgiler

Bu paket ile çalışmaya başlamadan önce mutlaka aşağıdaki bilgilere sahip olmanız gerekmektedir. Bilgileri Paraşüt destek ekibinden edinebilirsiniz.

`Company ID`: Bu bilgiyi kendinizde Paraşüt ekranlarındaki adres satırından edinebilirsiniz. Örnek olarak "https://uygulama.parasut.com/123456/" adresindeki "123456" sizin firma numaranızdır. Eğer numarayı bulmakta zorluk yaşıyorsanız yine Paraşüt destek ekibi size bu bilgiyi verecektir.

`Username`: Bu bilgi halihazırda Paraşüt'e giriş yapmak için kullandığınız kullanıcı olabilir ancak önerimiz sadece API için ayrı bir kullanıcı olurşturmanızdır.

`Password`: API bağlantısında kullanacağınız kullanıcının parolasıdır.

`Client ID`: Bu bilgiyi Paraşüt destek ekibinden edinebilirsiniz.

`Client Secret`: Bu bilgiyi Paraşüt destek ekibinden edinebilirsiniz.

## Örnekler
https://github.com/AvvaMobile/AvvaMobile.Core.Parasut ortamındaki Console projesi (AvvaMobile.Core.Parasut.Sample) içerisinde her bir metod için kullanım örnekleri bulunmaktadır. Her metodun kendine özel parametreleri olduğu için sırayla kullanarak incelemeniz önerilir.


## Geliştirme Ekibine Katılın

Desteğinize her zaman ihtiyacımız var. Geliştirme ekibine katılmak için lütfen opensource@avvamobile.com e-posta adresinden bizimle iletişime geçin.

## Geliştiriciler

- [@jackmuratyilmaz](https://www.github.com/jackmuratyilmaz)
- [@Onurryilmazz](https://www.github.com/Onurryilmazz)
- [@ocalesmer](https://www.github.com/ocalesmer)
- [@cativ3](https://www.github.com/cativ3)
- [@avvamobiledogukan](https://github.com/orgs/AvvaMobile/people/avvamobiledogukan)

## NuGet Paketi
NuGet kullanımı için [https://www.nuget.org/packages/AvvaMobile.Core.Parasut](https://www.nuget.org/packages/AvvaMobile.Core.Parasut) adresini ziyaret ediniz.

## Namespace
Geliştirmeye başlamadan önce aşağıdaki namespace tanımını yapmalısınız.
```csharp
using AvvaMobile.Core.Parasut;
```
## Servislerden Dönen Envelope (Zarf) Kullanımı
Tüm servislerden ortak olarak bir Envelope objesi dönmektedir. Bu obje size servisin çalışma sonucu hakkında meta data bilgiler içermekte ve dönecek olan veriyi de sarmalamaktadır.

**IsSuccess**: Servis çalıştıktan sonra başsarıyla tamamlandı ise true döner. Eğer bir hata oluşmuş ise false döner.

**Message**: Eğer serviste bir hata oluşmuş ise hatanın mesajını döndürür.

**Data**: Servis çağırırken hangi tipte olacağı belirtilir ise o tipte veriyi döndürür.

```csharp
public class ServiceResult<T>
{
    public bool IsSuccess { get; set; }
    public string Message { get; set; }
    public T Data { get; set; }
}
```

## Örnekler
- [Paraşüt Nesnesini Yaratmak](#parasut-nesnesini-yaratmak)
- [Token Almak](#token-almak)
- [Yeni Müşteri Yaratmak](#yeni-m%C3%BC%C5%9Fteri-yaratmak)
- [Müşteriye Ödeme Eklemek](#m%C3%BC%C5%9Fteriye-%C3%B6deme-eklemek)
- [Yeni Ürün Eklemek](#yeni-%C3%BCr%C3%BCn-eklemek)
- [Müşteriye Fatura Kesmek](#m%C3%BC%C5%9Fteriye-fatura-kesmek)
- [Faturaya Ödeme Eklemek](#faturaya-%C3%B6deme-eklemek)
- [Müşterinin E-Fatura Gelen Kutusu Sorgulaması Yapmak](#m%C3%BC%C5%9Fterinin-e-fatura-gelen-kutusu-sorgulamas%C4%B1-yapmak)
- [Faturayı E-Faturaya Dönüştür](#faturay%C4%B1-e-faturaya-d%C3%B6n%C3%BC%C5%9Ft%C3%BCr)


## Parasut Nesnesini Yaratmak
```csharp
var parasut = new Parasut("USERNAME", "PASSWORD", "CLIENT ID", "CLIENT SECRET", "COMPANY ID");
```

## Token Almak
```csharp
using AvvaMobile.Core.Parasut;

var parasut = new Parasut("USERNAME", "PASSWORD", "CLIENT ID", "CLIENT SECRET", "COMPANY ID");

var response = await parasut.Auth.Token();
if (response.IsSuccess)
{
    Console.WriteLine("access_token: " + response.Data.access_token);
}
else
{
    Console.WriteLine("ERROR: " + response.Message);
}
```

## Yeni Müşteri Yaratmak
```csharp
using AvvaMobile.Core.Parasut;

var parasut = new Parasut("USERNAME", "PASSWORD", "CLIENT ID", "CLIENT SECRET", "COMPANY ID");

var model = new CustomerRequest()
{
    data = new CustomerRequest_Data
    {
        attributes = new CustomerRequest_Data_Attributes
        {
            name = "AVVA MOBILE KURUMSAL ÇÖZÜMLER YAZILIM VE DANIŞMANLIK TİC. LTD. ŞTİ.",
            short_name = "Avva Mobile",
            tax_number = "1061332146",
            tax_office = "Kağıthane",
            address = "S Ofis",
            city = "İstanbul",
            country = "Türkiye",
            district = "Merkez"
        }
    }
};

var response = await parasut.Customer.Create(model);
if (response.IsSuccess)
{
    Console.WriteLine("Customer ID: " + response.Data.data.id);
}
else
{
    Console.WriteLine("ERROR: " + response.Message);
}
```

## Müşteriye Ödeme Eklemek
```csharp
using AvvaMobile.Core.Parasut;

var parasut = new Parasut("USERNAME", "PASSWORD", "CLIENT ID", "CLIENT SECRET", "COMPANY ID");

var customerID = "117650289"; //Müşterinin Paraşüt'teki ID'si

var model = new CustomerPaymentRequest
{
    data = new CustomerPaymentRequest_Data
    {
        attributes = new CustomerPaymentRequest_Data_Attributes
        {
            description = "Ödeme açıklaması",
            account_id = 1009901,
            date = "2023-04-13",
            amount = new decimal(123.44)
        }
    }
};

var response = await parasut.CustomerPayment.ContactDebitTransactions(model, customerID);
if (response.IsSuccess)
{
    Console.WriteLine("Payment ID: " + response.Data.data.id);
}
else
{
    Console.WriteLine("ERROR: " + response.Message);
}
```

## Yeni Ürün Eklemek
```csharp
using AvvaMobile.Core.Parasut;

var parasut = new Parasut("USERNAME", "PASSWORD", "CLIENT ID", "CLIENT SECRET", "COMPANY ID");

var model = new ProductRequest
{
    data = new ProductRequest_Data
    {
        attributes = new ProductRequest_DataAttributes
        {
            name = "Yeni Ürün",
            vat_rate = 18,
            unit = "Adet",
            list_price = new decimal(123.45),
            currency = Currencies.TRL
        }
    }
};

var response = await parasut.Product.Create(model);
if (response.IsSuccess)
{
    Console.WriteLine("Product ID: " + response.Data.data.id);
}
else
{
    Console.WriteLine("ERROR: " + response.Message);
}
```

## Müşteriye Fatura Kesmek

Fatura kesme işlemi biraz karışık olması sebebiyle, gönderilecek parametlerin doğruluğundan emin olmanızı öneririz.

```csharp
using AvvaMobile.Core.Parasut;

var parasut = new Parasut("USERNAME", "PASSWORD", "CLIENT ID", "CLIENT SECRET", "COMPANY ID");

var model = new InvoiceRequest
{
    data = new InvoiceRequest_Data
    {
        attributes = new InvoiceRequest_Data_Attributes
        {
            description = "Test Faturası",
            issue_date = "2023-04-26",
            due_date = "2023-04-26",
            currency = Currencies.TRL
        },
        relationships = new InvoiceRequest_Data_Relationships
        {
            contact = new InvoiceRequest_Data_Relationships_Contact
            {
                data = new InvoiceRequest_Data_Relationships_Contact_Data
                {
                    id = "117675307" // Müşterinin Paraşütteki ID'si
                }
            },
            details = new InvoiceRequest_Data_Relationships_Details
            {
                data = new List<InvoiceRequest_Data_Relationships_Details_Data>
                {
                    new InvoiceRequest_Data_Relationships_Details_Data
                    {
                        attributes = new InvoiceRequest_Data_Relationships_Details_Data_Attributes
                        {
                            quantity = 1,
                            unit_price = new decimal(1),
                            vat_rate = 18,
                            description = "Danışmanlık"
                        },
                        relationships = new InvoiceRequest_Data_Relationships_Details_Data_Relationships
                        {
                            product = new InvoiceRequest_Data_Relationships_Details_Data_Relationships_Product
                            {
                                data = new InvoiceRequest_Data_Relationships_Details_Data_Relationships_Product_Data
                                {
                                    id = "36605869" // Fatura kaleminde kullanılacak olan ürünün Paraşütteki ID'si.
                                }
                            }
                        }
                    }
                }
            }
        }
    }
};

var response = await parasut.Invoice.Create(model);
if (response.IsSuccess)
{
    Console.WriteLine("Invoice ID: " + response.Data.data.id);
}
else
{
    Console.WriteLine("ERROR: " + response.Message);
}
```

## Faturaya Ödeme Eklemek
```csharp
using AvvaMobile.Core.Parasut;

var parasut = new Parasut("USERNAME", "PASSWORD", "CLIENT ID", "CLIENT SECRET", "COMPANY ID");

var invoiceID = "155047996";

var model = new InvoicePaymentRequest
{
    data = new InvoicePaymentRequest_Data
    {
        attributes = new InvoicePaymentRequest_Data_Attributes
        {
            description = "Ödeme açıklaması",
            account_id = 1009901,
            date = "2023-04-13",
            amount = new decimal(123.44)
        }
    }
};

var response = await parasut.InvoicePayment.Pay(model, invoiceID);
if (response.IsSuccess)
{
    Console.WriteLine("Payment ID: " + response.Data.data.id);
}
else
{
    Console.WriteLine("ERROR: " + response.Message);
}
```

## Müşterinin E-Fatura Gelen Kutusu Sorgulaması Yapmak
Bir müşteriye E-Fatura kesmeden önce mutlaka o müşterinin e-fatura abonesi olup olmadığını kontrol etmelisiniz. Bu metod ile ilgili müşterinin tüm fatura gelen kutularının listesini çekebilirsiniz. Eğer yok ise e-fatura kesemezsiniz. Eğer birden fazla var ise hangi gelen kutusuna göndereceğinizi bilmek için dönünen bilgiler içerisindeki gelen kutusu adresini kullanmalısınız.

```csharp
using AvvaMobile.Core.Parasut;

var parasut = new Parasut("USERNAME", "PASSWORD", "CLIENT ID", "CLIENT SECRET", "COMPANY ID");

var response = await parasut.EInvoiceInbox.List("VERGİ NUMARASI");
if (response.IsSuccess)
{
    Console.WriteLine("E-Invoice Inbox Count: " + response.Data.data.Count);
    Console.WriteLine("E-Invoice Inbox Name: " + response.Data.data.e_invoice_address);
}
else
{
    Console.WriteLine("ERROR: " + response.Message);
}
```

## Faturayı E-Faturaya Dönüştür

```csharp
using AvvaMobile.Core.Parasut;

var parasut = new Parasut("USERNAME", "PASSWORD", "CLIENT ID", "CLIENT SECRET", "COMPANY ID");

var invoiceID = "123456";

var model = new EInvoiceCreateRequest
{
    data = new EInvoiceCreateRequest_Data
    {
        attributes = new EInvoiceCreateRequest_Data_Attributes
        {
            note = "Fatura Notu",
            scenario = "commercial",
            to = "urn:mail:defaultpk@avvamobile.com"
        },
        relationships = new EInvoiceCreateRequest_Data_Relationships
        {
            invoice = new EInvoiceCreateRequest_Data_Relationships_Invoice
            {
                data = new EInvoiceCreateRequest_Data_Relationships_Invoice_Data
                {
                    id = "155049858" // Paraşütte daha önce eklenmiş ve e-faturaya dönüştürülecek olan faturanın ID'si.
                }
            }
        }
    }
};

var response = await parasut.EInvoice.Create(model);
if (response.IsSuccess)
{
    Console.WriteLine("E-Invoice ID: " + response.Data.data.id);
}
else
{
    Console.WriteLine("ERROR: " + response.Message);
}
```