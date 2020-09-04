using Rishvi.Modules.NewUserResponse.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Rishvi.Modules.Users.Models.DTOs
{
    public class GenerateLabelDto
    {
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
        public List<PackageDto> Packages = new List<PackageDto>();
        public string OrderReference { get; set; }
        public string OrderCurrency { get; set; }
        public decimal OrderValue { get; set; }
        public decimal PostageCharges { get; set; }

        public List<ExtendedPropertyDto> OrderExtendedProperties = new List<ExtendedPropertyDto>();

        public Guid ServiceId { get; set; }
        public List<ServiceConfigItemDto> ServiceConfigItems = new List<ServiceConfigItemDto>();
    }

    public class ServiceConfigItemDto
    {
        public string ConfigItemId = "";
        public string SelectedValue = "";
    }


    public class PackageDto
    {
        public int SequenceNumber = 0;
        public decimal PackageWeight { get; set; }
        public decimal PackageWidth { get; set; }
        public decimal PackageHeight { get; set; }
        public decimal PackageDepth { get; set; }

        public string PackageFormat { get; set; }

        public List<Item> Items = new List<Item>();
    }

    public class ItemDo
    {
        public string ItemName { get; set; }
        public string ProductCode { get; set; }
        public int Quantity { get; set; }
        public decimal UnitValue { get; set; }
        public decimal UnitWeight { get; set; }
        public decimal Height { get; set; }
        public decimal Width { get; set; }
        public decimal Length { get; set; }

        public List<ExtendedPropertyDto> ExtendedProperties = new List<ExtendedPropertyDto>();
    }

    public class ExtendedPropertyDto
    {
        public string Name { get; set; }
        public string Value { get; set; }
    }

}