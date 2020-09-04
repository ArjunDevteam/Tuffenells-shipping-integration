using Rishvi.Modules.Core.Filters;
using System;

namespace Rishvi.Modules.AddNewRequest.Models.DTOs
{
	public class AddNewRequestDto 
	{
        public string Email { get; set; }
        public string LinnworksUniqueIdentifier { get; set; }
        public string AccountName { get; set; }
        public DateTime IntegratedDateTime { get; set; }
        public Guid AuthorizationToken { get; set; }
        public bool IsConfigActive { get; set; }
        public string ConfigStatus { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string AddressLine3 { get; set; }
        public string City { get; set; }
        public string County { get; set; }
        public string ContactName { get; set; }
        public string ContactPhoneNo { get; set; }
        public string CountryCode { get; set; }
        public string Postcode { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}