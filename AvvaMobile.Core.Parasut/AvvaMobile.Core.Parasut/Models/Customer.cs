using System;

namespace AvvaMobile.Core.Parasut.Models
{
    public class Customer
    {
        public string Email { get; set; }
        public string Name { get; set; }
        public string ShortName { get; set; }
        public string TaxOffice { get; set; }
        public string TaxNumber { get; set; }
        public string IBAN { get; set; }
    }
}