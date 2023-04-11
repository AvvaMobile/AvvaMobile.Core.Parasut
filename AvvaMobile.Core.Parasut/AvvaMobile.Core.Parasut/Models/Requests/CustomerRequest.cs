﻿using System.ComponentModel.DataAnnotations;

namespace AvvaMobile.Core.Parasut.Models.Requests;

public class CustomerRequest
{
    public CustomerRequest_Data data { get; set; }
}

public class CustomerRequest_Data
{
    public string id { get; set; }
    public string type { get; set; } = "contacts";
    public CustomerRequest_Data_Attributes attributes { get; set; }
}

public class CustomerRequest_Data_Attributes
{
    public string email { get; set; }
    [Required]
    public string name { get; set; }
    public string short_name { get; set; }
    public string contact_type { get; set; } = "company";
    public string tax_office { get; set; }
    public string tax_number { get; set; }
    public string district { get; set; }
    public string city { get; set; }
    public string country { get; set; }
    public string address { get; set; }
    public string phone { get; set; }
    public string fax { get; set; }
    public bool is_abroad { get; set; } = false;
    public bool archived { get; set; } = false;
    public string iban { get; set; }
    [Required]
    public string account_type { get; set; } = "customer";
    public bool untrackable { get; set; } = false;
}