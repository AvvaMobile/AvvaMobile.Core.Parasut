

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


## Geliştirme Ekibine Katılın

Desteğinize her zaman ihtiyacımız var. Geliştirme ekibine katılmak için lütfen opensource@avvamobile.com e-posta adresinden bizimle iletişime geçin.

## Geliştiriciler

- [@jackmuratyilmaz](https://www.github.com/jackmuratyilmaz)
- [@Onurryilmazz](https://www.github.com/Onurryilmazz)
- [@ocalesmer](https://www.github.com/ocalesmer)
- [@cativ3](https://www.github.com/cativ3)
- [avvamobiledogukan](https://github.com/orgs/AvvaMobile/people/avvamobiledogukan)
- [AbdulbakiBayansalduz](https://github.com/orgs/AvvaMobile/people/AbdulbakiBayansalduz) 

## Namespace
Geliştirmeye başlamadan önce aşağıdaki namespace tanımını yapmalısınız.
```csharp
using AvvaMobile.Core.Parasut;
using AvvaMobile.Core.Parasut.Models;
```
## Servislerden Dönen Envelope (Zarf) Kullanımı
Tüm servislerden ortak olarak bir Envelope objesi dönmektedir. Bu obje size servisin çalışma sonucu hakkında meta data bilgiler içermekte ve dönecek olan veriyi de sarmalamaktadır.

**IsSuccess**: Servis çalıştıktan sonra başsarıyla tamamlandı ise true döner. Eğer bir hata oluşmuş ise false döner:
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

## Parasut Nesnesini Yaratmak
```csharp
var parasut = new Parasut
{
    CompanyID = "Company ID",
    Username = "Username",
    Password = "Password",
    ClientID = "Client ID",
    ClientSecret = "Client Secret"
};
```
## Token Almak
```csharp
using AvvaMobile.Core.Parasut;
using AvvaMobile.Core.Parasut.Models;

var parasut = new Parasut
{
    CompanyID = "Company ID",
    Username = "Username",
    Password = "Password",
    ClientID = "Client ID",
    ClientSecret = "Client Secret"
};

var getTokenResponse = await parasut.GetTokenAsync();
if (getTokenResponse.IsSuccess)
{
    Console.WriteLine("access_token: " + getTokenResponse.Data.access_token);
}
else
{
    Console.WriteLine("ERROR: " + getTokenResponse.Message);
}
```