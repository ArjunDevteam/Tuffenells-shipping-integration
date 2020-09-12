using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rishvi.Modules.Users.Models
{
    public class InsertDataDto
    {
        public string AuthorizationToken { get; set; }
        public string Name { get; set; }
        public string CompanyName { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string AddressLine3 { get; set; }
        public string Town { get; set; }
        public string Region { get; set; }
        public string CountryCode { get; set; }
        public string Postalcode { get; set; }
        public string DeliveryNote { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public int OrderId { get; set; }
        public List<Package> Packages { get; set; }
        public string OrderReference { get; set; }
        public string OrderCurrency { get; set; }
        public decimal OrderValue { get; set; }
        public decimal PostageCharges { get; set; }
        public List<ExtendedProperty> OrderExtendedProperties { get; set; }
        public Guid ServiceId { get; set; }
        public List<ServiceConfigItem> ServiceConfigItems { get; set; }
    }
}
