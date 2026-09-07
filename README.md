<div align="center">

# Paraşüt

**Paraşüt API'ı ile uğraşmadan, kolay ve hızlı çalışmanızı sağlayan .NET paketi.**

[![NuGet](https://img.shields.io/nuget/v/AvvaMobile.Core.Parasut.svg)](https://www.nuget.org/packages/AvvaMobile.Core.Parasut)
[![Downloads](https://img.shields.io/nuget/dt/AvvaMobile.Core.Parasut.svg)](https://www.nuget.org/packages/AvvaMobile.Core.Parasut)
![.NET](https://img.shields.io/badge/.NET-6.0%20%7C%207.0%20%7C%208.0%20%7C%2010.0-512BD4)

</div>

Geliştiricilerin Paraşüt API'larını düşük seviyede uğraşmadan kullanabilmesi için hazırlanmıştır. Kimlik doğrulama, token yönetimi, serileştirme ve hata çözümlemesi paket tarafından halledilir; siz yalnızca iş modelinizi doldurup metodu çağırırsınız.

Paraşüt'ün orijinal API dokümanları: [apidocs.parasut.com](https://apidocs.parasut.com)

---

## İçindekiler

- [Kurulum](#kurulum)
- [Gereksinimler](#gereksinimler)
- [Gerekli Bilgiler](#gerekli-bilgiler)
- [Hızlı Başlangıç](#hızlı-başlangıç)
- [Paraşüt Nesnesini Yaratmak](#paraşüt-nesnesini-yaratmak)
- [Token Yönetimi](#token-yönetimi)
- [Servis Sonucu (Envelope)](#servis-sonucu-envelope)
- [Desteklenen Servisler](#desteklenen-servisler)
- [Örnekler](#örnekler)
- [1.1.x → 2.0.0 Geçiş Notları](#11x--200-geçiş-notları)
- [Katkı ve İletişim](#katkı-ve-i̇letişim)

---

## Kurulum

```bash
dotnet add package AvvaMobile.Core.Parasut
```

NuGet paket sayfası: [nuget.org/packages/AvvaMobile.Core.Parasut](https://www.nuget.org/packages/AvvaMobile.Core.Parasut)

Geliştirmeye başlamadan önce namespace tanımını yapın:

```csharp
using AvvaMobile.Core.Parasut;
```

## Gereksinimler

`net6.0`, `net7.0`, `net8.0` ve `net10.0` hedeflerini destekler.

Paketin kendi kodu **harici bir bağımlılık kullanmaz** — HTTP çağrıları `HttpClient`, JSON işlemleri `System.Text.Json` ile yapılır.

## Gerekli Bilgiler

Çalışmaya başlamadan önce aşağıdaki beş bilgiye sahip olmanız gerekir.

| Bilgi | Nereden alınır |
| --- | --- |
| **Company ID** | Paraşüt ekranlarındaki adres satırından. Örneğin `https://uygulama.parasut.com/123456/` adresinde firma numaranız `123456`'dır. Bulmakta zorlanırsanız Paraşüt destek ekibi verecektir. |
| **Username** | Paraşüt'e giriş yaptığınız kullanıcı olabilir; ancak **önerimiz API için ayrı bir kullanıcı oluşturmanızdır**. |
| **Password** | API bağlantısında kullanacağınız kullanıcının parolası. |
| **Client ID** | Paraşüt destek ekibinden. |
| **Client Secret** | Paraşüt destek ekibinden. |

## Hızlı Başlangıç

```csharp
using AvvaMobile.Core.Parasut;

var parasut = new Parasut("USERNAME", "PASSWORD", "CLIENT ID", "CLIENT SECRET", "COMPANY ID");

var model = new CustomerRequest
{
    data = new CustomerRequest_Data
    {
        attributes = new CustomerRequest_Data_Attributes { name = "Örnek Müşteri" }
    }
};

var response = await parasut.Customer.Create(model);
if (response.IsSuccess)
{
    Console.WriteLine("Customer ID: " + response.Data?.data?.id);
}
else
{
    Console.WriteLine("HATA: " + response.Message);
}
```

Token almanıza gerek yoktur — her metod gerektiğinde token'ı kendisi alır.

## Paraşüt Nesnesini Yaratmak

```csharp
var parasut = new Parasut("USERNAME", "PASSWORD", "CLIENT ID", "CLIENT SECRET", "COMPANY ID");
```

`Parasut` nesnesi token önbelleğini içinde tuttuğu için **uygulama ömrü boyunca tek örnek (singleton) olarak tutulması önerilir.**

**Kendi HttpClient'ınız ile.** `IHttpClientFactory` kullanan uygulamalarda:

```csharp
builder.Services.AddHttpClient("parasut");

builder.Services.AddSingleton(sp =>
{
    var http = sp.GetRequiredService<IHttpClientFactory>().CreateClient("parasut");
    return new Parasut("USERNAME", "PASSWORD", "CLIENT ID", "CLIENT SECRET", "COMPANY ID", http);
});
```

**İptal desteği.** Tüm servis metodları isteğe bağlı `CancellationToken` alır:

```csharp
var response = await parasut.Customer.Create(model, cancellationToken);
```

## Token Yönetimi

Token `Parasut` nesnesi içinde önbelleğe alınır ve süresi dolmadan (60 saniye güvenlik payıyla) yeniden istenmez. Servis metodlarını çağırmadan önce ayrıca token almanız gerekmez.

Kimlik bilgilerini çalışma anında değiştirirseniz önbellek otomatik temizlenir. Elle temizlemek için:

```csharp
parasut.Auth.InvalidateToken();
```

Token'a doğrudan erişmek isterseniz:

```csharp
var response = await parasut.Auth.Token();
if (response.IsSuccess)
{
    Console.WriteLine("access_token: " + response.Data?.access_token);
}
else
{
    Console.WriteLine("HATA: " + response.Message);
}
```

## Servis Sonucu (Envelope)

Tüm servisler ortak bir zarf (envelope) nesnesi döner. Bu nesne servisin çalışma sonucu hakkında meta bilgi taşır ve asıl veriyi sarmalar.

```csharp
public class ParasutServiceResult<T>
{
    public bool IsSuccess { get; set; }
    public string? Message { get; set; }
    public List<ParasutError>? Errors { get; set; }
    public T? Data { get; set; }
}
```

| Alan | Açıklama |
| --- | --- |
| `IsSuccess` | Servis başarıyla tamamlandıysa `true`, hata oluştuysa `false` döner. |
| `Message` | Hata oluştuysa hatanın mesajını döndürür. |
| `Errors` | Paraşüt yapılandırılmış hata döndürdüyse (`{"errors":[{"title","detail"}]}`) doldurulur. |
| `Data` | Servis çağrılırken belirtilen tipte veriyi döndürür. |

Hata ayrıntılarına erişim:

```csharp
if (!response.IsSuccess)
{
    Console.WriteLine(response.Message);

    if (response.Errors != null)
    {
        foreach (var error in response.Errors)
        {
            Console.WriteLine(error.title + ": " + error.detail);
        }
    }
}
```

## Desteklenen Servisler

| Servis | Metod | Açıklama |
| --- | --- | --- |
| `parasut.Auth` | `Token()` | Token alır (otomatik, elle çağırmak gerekmez) |
| `parasut.Customer` | `Create()` · `Edit()` | Müşteri oluşturur / günceller |
| `parasut.Product` | `Create()` · `Edit()` | Ürün oluşturur / günceller |
| `parasut.Invoice` | `Create()` · `Edit()` | Satış faturası oluşturur / günceller |
| `parasut.InvoicePayment` | `Pay()` | Faturaya ödeme ekler |
| `parasut.CustomerPayment` | `ContactDebitTransactions()` | Müşteriye ödeme/tahsilat ekler |
| `parasut.EInvoiceInbox` | `List()` | E-fatura gelen kutusu sorgular |
| `parasut.EInvoice` | `Create()` | Faturayı e-faturaya dönüştürür (özel firma gereksinimleri dahil) |
| `parasut.EArchive` | `Create()` | Faturayı e-arşiv faturasına dönüştürür |
| `parasut.TrackableJob` | `GetStatus()` | E-fatura/e-arşiv işlem durumunu sorgular |

---

## Örnekler

Aşağıdaki örneklerin tamamı, çalıştırılabilir hâlde [`AvvaMobile.Core.Parasut.Sample`](https://github.com/AvvaMobile/AvvaMobile.Core.Parasut) konsol projesinde bulunur. Her metodun kendine özel parametreleri olduğu için örnekleri sırayla inceleyerek ilerlemeniz önerilir.

Örneklerde tekrarı azaltmak için `parasut` nesnesinin ve `using` satırının bir kez tanımlandığı varsayılmıştır:

```csharp
using AvvaMobile.Core.Parasut;

var parasut = new Parasut("USERNAME", "PASSWORD", "CLIENT ID", "CLIENT SECRET", "COMPANY ID");
```

- [Yeni Müşteri Yaratmak](#yeni-müşteri-yaratmak)
- [Müşteriye Ödeme Eklemek](#müşteriye-ödeme-eklemek)
- [Yeni Ürün Eklemek](#yeni-ürün-eklemek)
- [Müşteriye Fatura Kesmek](#müşteriye-fatura-kesmek)
- [Faturaya Ödeme Eklemek](#faturaya-ödeme-eklemek)
- [Müşterinin E-Fatura Gelen Kutusu Sorgulaması Yapmak](#müşterinin-e-fatura-gelen-kutusu-sorgulaması-yapmak)
- [Faturayı E-Faturaya Dönüştürmek](#faturayı-e-faturaya-dönüştürmek)
- [Faturayı E-Arşiv Faturasına Dönüştürmek](#faturayı-e-arşiv-faturasına-dönüştürmek)
- [Fatura İşlem Durumunu Sorgulamak](#fatura-i̇şlem-durumunu-sorgulamak)
- [Belirli Firmalar İçin Özel Gereksinimler (SGK vb.)](#belirli-firmalar-i̇çin-özel-gereksinimler-sgk-vb)

### Yeni Müşteri Yaratmak

```csharp
var model = new CustomerRequest
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
    Console.WriteLine("Customer ID: " + response.Data?.data?.id);
}
else
{
    Console.WriteLine("HATA: " + response.Message);
}
```

### Müşteriye Ödeme Eklemek

```csharp
var customerID = "117650289"; // Müşterinin Paraşüt'teki ID'si

var model = new CustomerPaymentRequest
{
    data = new CustomerPaymentRequest_Data
    {
        attributes = new CustomerPaymentRequest_Data_Attributes
        {
            description = "Ödeme açıklaması",
            account_id = 1009901,
            date = "2023-04-13",
            amount = 123.44m
        }
    }
};

var response = await parasut.CustomerPayment.ContactDebitTransactions(model, customerID);
if (response.IsSuccess)
{
    Console.WriteLine("Payment ID: " + response.Data?.data?.id);
}
else
{
    Console.WriteLine("HATA: " + response.Message);
}
```

### Yeni Ürün Eklemek

```csharp
var model = new ProductRequest
{
    data = new ProductRequest_Data
    {
        attributes = new ProductRequest_DataAttributes
        {
            name = "Yeni Ürün",
            vat_rate = 18,
            unit = "Adet",
            list_price = 123.45m,
            currency = Currencies.TRL
        }
    }
};

var response = await parasut.Product.Create(model);
if (response.IsSuccess)
{
    Console.WriteLine("Product ID: " + response.Data?.data?.id);
}
else
{
    Console.WriteLine("HATA: " + response.Message);
}
```

### Müşteriye Fatura Kesmek

> Fatura kesme işlemi görece karmaşıktır; gönderilecek parametrelerin doğruluğundan emin olmanızı öneririz.

```csharp
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
                    id = "117675307" // Müşterinin Paraşüt'teki ID'si
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
                            unit_price = 1m,
                            vat_rate = 18,
                            description = "Danışmanlık"
                            // Tevkifatlı fatura için: vat_withholding_rate = 20
                        },
                        relationships = new InvoiceRequest_Data_Relationships_Details_Data_Relationships
                        {
                            product = new InvoiceRequest_Data_Relationships_Details_Data_Relationships_Product
                            {
                                data = new InvoiceRequest_Data_Relationships_Details_Data_Relationships_Product_Data
                                {
                                    id = "36605869" // Kalemde kullanılacak ürünün Paraşüt'teki ID'si
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
    Console.WriteLine("Invoice ID: " + response.Data?.data?.id);
}
else
{
    Console.WriteLine("HATA: " + response.Message);
}
```

### Faturaya Ödeme Eklemek

```csharp
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
            amount = 123.44m
        }
    }
};

var response = await parasut.InvoicePayment.Pay(model, invoiceID);
if (response.IsSuccess)
{
    Console.WriteLine("Payment ID: " + response.Data?.data?.id);
}
else
{
    Console.WriteLine("HATA: " + response.Message);
}
```

### Müşterinin E-Fatura Gelen Kutusu Sorgulaması Yapmak

> Bir müşteriye e-fatura kesmeden önce **mutlaka** o müşterinin e-fatura abonesi olup olmadığını kontrol edin. Bu metod ilgili müşterinin tüm fatura gelen kutularını listeler. Kayıt yoksa e-fatura kesemezsiniz — e-arşiv fatura kesmelisiniz. Birden fazla kutu varsa, hangisine göndereceğinizi belirlemek için dönen kayıtlardaki gelen kutusu adresini kullanın.

```csharp
var response = await parasut.EInvoiceInbox.List("VERGİ NUMARASI");
if (response.IsSuccess)
{
    var inboxes = response.Data?.data;
    Console.WriteLine("E-Invoice Inbox Count: " + (inboxes?.Count ?? 0));

    if (inboxes != null)
    {
        foreach (var inbox in inboxes)
        {
            Console.WriteLine("Adres: " + inbox.attributes?.e_invoice_address);
        }
    }
}
else
{
    Console.WriteLine("HATA: " + response.Message);
}
```

### Faturayı E-Faturaya Dönüştürmek

> Müşterinin e-fatura üyeliği varsa bu yöntemi kullanın. Üyeliği yoksa e-arşiv fatura kesmelisiniz.

> **e-Fatura / e-Arşiv / e-SMM oluşturma işlemi senkron değildir.** İstek arka planda yerine getirilir. Bu yüzden oluşturma endpoint'leri cevap olarak, işlemin durumunu takip edebileceğiniz bir işlem ID'si (trackable job) döner. Bu ID'yi [durum sorgulama](#fatura-i̇şlem-durumunu-sorgulamak) metodunda belirli aralıklarla kullanarak sonucu takip etmeniz gerekir. **ID'nin kullanım süresi oluşturulduktan sonra 15 dakikadır.**

```csharp
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
                    id = "FATURA NO" // Paraşüt'te daha önce eklenmiş, e-faturaya dönüştürülecek faturanın ID'si
                }
            }
        }
    }
};

var response = await parasut.EInvoice.Create(model);
if (response.IsSuccess)
{
    Console.WriteLine("E-Invoice ID: " + response.Data?.data?.id);
}
else
{
    Console.WriteLine("HATA: " + response.Message);
}
```

### Faturayı E-Arşiv Faturasına Dönüştürmek

> Bu bölümden yalnızca e-arşiv faturası kesebilirsiniz. Müşterinin e-fatura üyeliği varsa [yukarıdaki yöntemi](#faturayı-e-faturaya-dönüştürmek) kullanmalısınız.

> Bu işlem de senkron değildir; dönen işlem ID'sini durum sorgulama metoduyla takip edin (ID ömrü 15 dakika).

```csharp
var model = new EArchiveCreateRequest
{
    data = new EArchiveCreateRequest_Data
    {
        attributes = new EArchiveCreateRequest_Data_Attributes
        {
            note = "Fatura Notu"
        },
        relationships = new EArchiveCreateRequest_Data_Relationships
        {
            sales_invoice = new EArchiveCreateRequest_Data_Relationships_Invoice
            {
                data = new EArchiveCreateRequest_Data_Relationships_Invoice_Data
                {
                    id = "FATURA NO" // Paraşüt'te daha önce eklenmiş, e-arşive dönüştürülecek faturanın ID'si
                }
            }
        }
    }
};

var response = await parasut.EArchive.Create(model);
if (response.IsSuccess)
{
    Console.WriteLine("E-Archive ID: " + response.Data?.data?.id);
}
else
{
    Console.WriteLine("HATA: " + response.Message);
}
```

### Fatura İşlem Durumunu Sorgulamak

E-fatura veya e-arşiv olarak resmileştirilmiş bir faturanın durumunu sorgulamak için kullanılır.

```csharp
var model = new TrackableJobRequest
{
    id = 123,        // E-arşiv / e-fatura kesme işlemi sonucunda dönen trackable job ID'si
    company_id = 456 // Şirket ID'si
};

var response = await parasut.TrackableJob.GetStatus(model);
if (response.IsSuccess)
{
    Console.WriteLine("Status: " + response.Data?.data?.attributes?.status);

    var errors = response.Data?.data?.attributes?.errors;
    if (errors != null)
    {
        foreach (var error in errors)
        {
            Console.WriteLine("İşlem hatası: " + error);
        }
    }
}
else
{
    Console.WriteLine("HATA: " + response.Message);
}
```

### Belirli Firmalar İçin Özel Gereksinimler (SGK vb.)

Özel gereksinim duyan bir firmaya e-fatura keserken `custom_requirement_params` alanını doldurmanız gerekir. Aşağıda SGK için gereken alanlar gösterilmiştir.

```csharp
var model = new EInvoiceCreateRequest
{
    data = new EInvoiceCreateRequest_Data
    {
        attributes = new EInvoiceCreateRequest_Data_Attributes
        {
            scenario = "commercial",
            to = "urn:mail:defaultpk@ornek.com",
            custom_requirement_params = new EInvoiceCreateRequest_Data_Attributes_CustomRequirementParams
            {
                integration = new EInvoiceCreateRequest_Data_Attributes_CustomRequirementParams_Integration
                {
                    data = new EInvoiceCreateRequest_Data_Attributes_CustomRequirementParams_Integration_Data
                    {
                        additional_invoice_type = AdditionalInvoiceTypes.SAGLIK_ECZ,
                        tax_payer_code = "Mükellef kodu",
                        tax_payer_name = "Mükellef adı",
                        file_number = "Dosya numarası",
                        term_start_date = "2021-01-02",
                        term_end_date = "2021-01-04"
                    }
                }
            }
        },
        relationships = new EInvoiceCreateRequest_Data_Relationships
        {
            invoice = new EInvoiceCreateRequest_Data_Relationships_Invoice
            {
                data = new EInvoiceCreateRequest_Data_Relationships_Invoice_Data { id = "FATURA NO" }
            }
        }
    }
};

var response = await parasut.EInvoice.Create(model);
```

`additional_invoice_type` için geçerli değerler `AdditionalInvoiceTypes` sınıfındadır: `SAGLIK_ECZ`, `SAGLIK_HAS`, `SAGLIK_OPT`, `SAGLIK_MED`, `ABONELIK`, `MAL_HIZMET`, `DIGER`.

Paraşüt bu alanın içeriğini serbest biçimli bir obje olarak tanımlar ve desteklenen firma listesi zamanla değişebilir. Yukarıdaki alanlar dışında bir alan göndermeniz gerekirse `additional_params` sözlüğünü kullanın; içeriği gövdeye aynı seviyede yazılır:

```csharp
var data = new EInvoiceCreateRequest_Data_Attributes_CustomRequirementParams_Integration_Data
{
    additional_invoice_type = AdditionalInvoiceTypes.MAL_HIZMET,
    additional_params = new Dictionary<string, object>
    {
        ["yeni_alan"] = "değer"
    }
};
```

Ayrıntılı bilgi: [Paraşüt dokümanı — Belirli Firmalar İçin Özel Gereksinimler](https://apidocs.parasut.com/#section/SIK-KULLANILAN-ISLEMLER/Belirli-Firmalar-Icin-Ozel-Gereksinimler)

---

## 1.1.x → 2.0.0 Geçiş Notları

1.1.x'ten gelen kod, paketin kendi API'si açısından **değişiklik gerektirmeden derlenir**. Yine de aşağıdaki noktalara dikkat edin.

**0. Newtonsoft.Json artık bağımlılık değil.** Paket sıfır bağımlılıkla gelir; JSON işlemleri `System.Text.Json` ile yapılır. 1.1.x'te Newtonsoft'u bu paket üzerinden **transitive** olarak alan projeler `Newtonsoft could not be found` hatası alır. Çözüm: kendi projenize açıkça ekleyin.

```bash
dotnet add package Newtonsoft.Json
```

Bu, major sürüm çıkarmamızın sebebidir; paketin genel API'sinde başka bir kırılma yoktur.

**1. Düzeltilen alan adları.** Paraşüt API'ında karşılığı olmayan iki alan yüzünden bazı veriler sessizce boş geliyordu. Eski adlar çalışmaya devam ediyor (yeni alana yönlendiriliyor), ancak yenilerini kullanın:

| Eski (1.1.x) | Yeni | 1.1.x'teki davranış |
| --- | --- | --- |
| `InvoiceResponse...vat_withholding` | `total_vat_withholding` | her zaman `0` dönüyordu |
| `ProductResponse...updated_ay` | `updated_at` | her zaman `null` dönüyordu |

> Bu alanlar artık gerçek değerlerini döndürüyor. Daha önce bu hatanın etrafından dolanmak için tevkifatı kendiniz hesaplayıp eklediyseniz, çift saymamak için kontrol edin.

**2. Tevkifat oranı kalem seviyesindedir.** `InvoiceRequest_Data_Attributes.vat_withholding_rate` Paraşüt API'ında bulunmadığı için etkisizdir; geriye dönük uyumluluk adına korunmuştur. Tevkifat oranını kalem bazında verin:

```csharp
new InvoiceRequest_Data_Relationships_Details_Data_Attributes
{
    quantity = 1,
    unit_price = 1000m,
    vat_rate = 20,
    vat_withholding_rate = 20 // Tevkifat oranı burada
}
```

**3. Token artık önbelleğe alınıyor.** Her servis çağrısı ayrı token isteği atmıyor. Ayrıntı için [Token Yönetimi](#token-yönetimi) bölümüne bakın.

Ayrıca bu sürümde: `CancellationToken` desteği eklendi, kendi `HttpClient`'ınızı verebilir hâle geldiniz, hata gövdeleri `Errors` listesine çözümleniyor, `TrackableJob.GetStatus` artık doğru HTTP metodunu (`GET`) kullanıyor ve özel firma gereksinimleri için [`custom_requirement_params`](#belirli-firmalar-i̇çin-özel-gereksinimler-sgk-vb) eklendi.

---

## Katkı ve İletişim

Desteğinize her zaman ihtiyacımız var. Geliştirme ekibine katılmak için <opensource@avvamobile.com> adresinden bizimle iletişime geçin.

### Geliştiriciler

- [@jackmuratyilmaz](https://www.github.com/jackmuratyilmaz)
- [@Onurryilmazz](https://www.github.com/Onurryilmazz)
- [@ocalesmer](https://www.github.com/ocalesmer)
- [@cativ3](https://www.github.com/cativ3)
- [@avvamobiledogukan](https://github.com/orgs/AvvaMobile/people/avvamobiledogukan)
