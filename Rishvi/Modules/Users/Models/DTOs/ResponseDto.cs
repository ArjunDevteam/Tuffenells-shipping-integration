using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rishvi.Modules.NewUserResponse.Models.DTOs
{
    public class ResponseDto
    {
        public Guid AuthorizationToken;
        public string ErrorMessage { get; set; }
        public string error { get; set; }
        public bool IsError { get; set; }

        public bool IsConfigActive;
        public string ConfigStatus;
        public ConfigStage ConfigStage;
    }

    public class ConfigStage
    {
        public string WizardStepDescription;
        public string WizardStepTitle;
        public List<ConfigItem> ConfigItems = new List<ConfigItem>();
    }

    public class ConfigItem
    {
        public string ConfigItemId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string GroupName { get; set; }
        public int SortOrder { get; set; }
        public string SelectedValue { get; set; }

        public string RegExValidation { get; set; }
        public string RegExError { get; set; }
        public bool MustBeSpecified { get; set; }
        public bool ReadOnly { get; set; }
        public List<ConfigItemListItem> ListValues = new List<ConfigItemListItem>();
        public ConfigValueType ValueType = ConfigValueType.STRING;
    }

    public class ConfigItemListItem
    {
        public string Display;
        public string Value;
        public ConfigItemListItem() { }
        public ConfigItemListItem(string display, string value)
        {
            Display = display;
            Value = value;
        }
        public ConfigItemListItem(string value)
        {
            Display = value;
            Value = value;
        }
    }

    public enum ConfigValueType
    {
        STRING = 0,
        INT = 1,
        DOUBLE = 2,
        BOOLEAN = 3,
        PASSWORD = 4,
        LIST = 5
    }

    public class UpdateConfigResponse
    {
        public string Error { get; set; }
    }

    public class GenerateLabelResponse
    {
        public string LeadTrackingNumber = "";
        public decimal Cost = 0;
        public string Currency = "GBP";

        public List<PackageResponse> Package = new List<PackageResponse>();
    }

    public class PackageResponse
    {
        public int SequenceNumber { get; set; }
        public string TrackingNumber { get; set; }
        public string PNGLabelDataBase64 { get; set; }
        public string[] PDFBytesDocumentationBase64 { get; set; }
        public decimal LabelWidth { get; set; }
        public decimal LabelHeight { get; set; }
    }

    public class CancelLabelResponse 
    {
        public string Error { get; set; }
    }

    public class PrintManifestResponseDto
    {
        public string ManifestReference { get; set; }

        public string error { get; set; }
    }

    public class CreateManifestResponse
    {
        public Guid ManifestReference { get; set; }

        public string error { get; set; }
    }

    public class PrintManifestResponse
    {
        public string PDFbase64 { get; set; }

        public string  error { get; set; }
    }
}
