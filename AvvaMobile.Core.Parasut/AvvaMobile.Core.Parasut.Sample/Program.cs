using AvvaMobile.Core.Parasut;
using AvvaMobile.Core.Parasut.Models.Requests;
using AvvaMobile.Core.Parasut.Models.Responses;
using AvvaMobile.Core.Parasut.Services;

// Bu bilgileri nereden alacağınız konusundaki bilgiler için lütfen https://github.com/AvvaMobile/AvvaMobile.Core.Parasut adresini ziyaret ediniz.
var parasut = new Parasut("USERNAME", "PASSWORD", "CLIENT ID", "CLIENT SECRET", "COMPANY ID");

//await Token();
//await CustomerCreate();
//await CustomerEdit();
//await CustomerPayment();
//await ProductCreate();
//await ProductEdit();
//await InvoiceCreate();
//await InvoiceEdit();
//await InvoicePay();
//await EInvoiceInboxList();
//await EInvoiceCreate();

async Task Token()
{
    var response = await parasut.Auth.Token();
    if (response.IsSuccess)
    {
        Console.WriteLine("access_token: " + response.Data.access_token);
    }
    else
    {
        Console.WriteLine("ERROR: " + response.Message);
    }
}

async Task CustomerCreate()
{
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
}

async Task CustomerEdit()
{
    var model = new CustomerRequest()
    {
        data = new CustomerRequest_Data
        {
            id = "117650289",
            attributes = new CustomerRequest_Data_Attributes
            {
                name = "AVVA MOBILE KURUMSAL ÇÖZÜMLER YAZILIM VE DANIŞMANLIK TİC. LTD. ŞTİ. 2222",
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

    var response = await parasut.Customer.Edit(model);
    if (response.IsSuccess)
    {
        Console.WriteLine("Customer ID: " + response.Data.data.id);
    }
    else
    {
        Console.WriteLine("ERROR: " + response.Message);
    }
}

async Task CustomerPayment()
{
    var customerID = "117650289";

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
}

async Task ProductCreate()
{
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
}

async Task ProductEdit()
{
    var model = new ProductRequest
    {
        data = new ProductRequest_Data
        {
            id = "100009111",
            attributes = new ProductRequest_DataAttributes
            {
                name = "Deneme Ürün 2",
                vat_rate = 18,
                unit = "Adet",
                list_price = new decimal(123.45),
                currency = Currencies.TRL
            }
        }
    };

    var response = await parasut.Product.Edit(model);
    if (response.IsSuccess)
    {
        Console.WriteLine("Product ID: " + response.Data.data.id);
    }
    else
    {
        Console.WriteLine("ERROR: " + response.Message);
    }
}

async Task InvoiceCreate()
{
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
                        id = "117675307"
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
                                        id = "36605869"
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
}

async Task InvoiceEdit()
{
    var model = new InvoiceRequest
    {
        data = new InvoiceRequest_Data
        {
            id = "155047996",
            attributes = new InvoiceRequest_Data_Attributes
            {
                description = "Güncellenmiş Fatura Açıklaması",
                issue_date = "2023-04-11",
                due_date = "2023-05-11"
            },
            relationships = new InvoiceRequest_Data_Relationships
            {
                contact = new InvoiceRequest_Data_Relationships_Contact
                {
                    data = new InvoiceRequest_Data_Relationships_Contact_Data
                    {
                        id = "117650289"
                    }
                },
                details = new InvoiceRequest_Data_Relationships_Details
                {
                    data = new List<InvoiceRequest_Data_Relationships_Details_Data>
                    {
                        new InvoiceRequest_Data_Relationships_Details_Data
                        {
                            relationships = new InvoiceRequest_Data_Relationships_Details_Data_Relationships
                            {
                                product = new InvoiceRequest_Data_Relationships_Details_Data_Relationships_Product
                                {
                                    data = new InvoiceRequest_Data_Relationships_Details_Data_Relationships_Product_Data
                                    {
                                        id = "36605869"
                                    }
                                }
                            },
                            attributes = new InvoiceRequest_Data_Relationships_Details_Data_Attributes
                            {
                                quantity = 10,
                                unit_price = new decimal(123.456),
                                vat_rate = 18
                            }
                        }
                    }
                }
            }
        }
    };

    var response = await parasut.Invoice.Edit(model);
    if (response.IsSuccess)
    {
        Console.WriteLine("Invoice ID: " + response.Data.data.id);
    }
    else
    {
        Console.WriteLine("ERROR: " + response.Message);
    }
}

async Task InvoicePay()
{
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
}

async Task EInvoiceInboxList()
{
    var response = await parasut.EInvoiceInbox.List("8170694712");
    if (response.IsSuccess)
    {
        Console.WriteLine("E-Invoice Inbox Count: " + response.Data.data.Count);
    }
    else
    {
        Console.WriteLine("ERROR: " + response.Message);
    }
}

async Task EInvoiceCreate()
{
    var model = new EInvoiceCreateRequest
    {
        data = new EInvoiceCreateRequest_Data
        {
            attributes = new EInvoiceCreateRequest_Data_Attributes
            {
                note = "Fatura Notu Test",
                scenario = "commercial",
                to = "urn:mail:defaultpk@avvamobile.com"
            },
            relationships = new EInvoiceCreateRequest_Data_Relationships
            {
                invoice = new EInvoiceCreateRequest_Data_Relationships_Invoice
                {
                    data = new EInvoiceCreateRequest_Data_Relationships_Invoice_Data
                    {
                        id = "155049858"
                    }
                }
            }
        }
    };

    var response = await parasut.EInvoice.Create(model);
    if (response.IsSuccess)
    {
        Console.WriteLine("EInvoice ID: " + response.Data.data.id);
    }
    else
    {
        Console.WriteLine("ERROR: " + response.Message);
    }
}

Console.ReadKey();