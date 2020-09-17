using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rishvi.Modules.Users.Models
{
    public static class ContactStage
    {
        public static ConfigStage GetContactStage
        {
            get
            {
                return new ConfigStage()
                {
                    WizardStepDescription = "Customer enters some details at this stage",
                    WizardStepTitle = "Customer Details",
                    ConfigItems = new List<ConfigItem>() {
                        new ConfigItem() {
                                ConfigItemId = "NAME",
                                Description="Contact name",
                                GroupName="Sender Address",
                                MustBeSpecified = true,
                                Name="Contact Name",
                                ReadOnly= false,
                                SelectedValue="",
                                SortOrder=1,
                                ValueType = ConfigValueType.STRING
                            },
                            new ConfigItem() {
                                ConfigItemId = "COMPANY",
                                Description="Company name",
                                GroupName="Sender Address",
                                MustBeSpecified = true,
                                Name="Company Name",
                                ReadOnly= false,
                                SelectedValue="",
                                SortOrder=1,
                                ValueType = ConfigValueType.STRING
                            },
                            new ConfigItem() {
                                ConfigItemId = "ADDRESS1",
                                Description="Street Name",
                                GroupName="Sender Address",
                                MustBeSpecified = true,
                                Name="Street Name",
                                ReadOnly= false,
                                SelectedValue="",
                                SortOrder=3,
                                ValueType = ConfigValueType.STRING
                            },
                            new ConfigItem() {
                                ConfigItemId = "ADDRESS2",
                                Description="Street Number",
                                GroupName="Sender Address",
                                MustBeSpecified = false,
                                Name="Street Number",
                                ReadOnly= false,
                                SelectedValue="",
                                SortOrder=3,
                                ValueType = ConfigValueType.STRING
                            },
                            new ConfigItem() {
                                ConfigItemId = "ADDRESS3",
                                Description="Address line 3",
                                GroupName="Sender Address",
                                MustBeSpecified = false,
                                Name="Address 3",
                                ReadOnly= false,
                                SelectedValue="",
                                SortOrder=4,
                                ValueType = ConfigValueType.STRING
                            },
                            new ConfigItem() {
                                ConfigItemId = "CITY",
                                Description="Town/City name",
                                GroupName="Sender Address",
                                MustBeSpecified = true,
                                Name="Town/City",
                                ReadOnly= false,
                                SelectedValue="",
                                SortOrder=5,
                                ValueType = ConfigValueType.STRING
                            },
                            new ConfigItem() {
                                ConfigItemId = "REGION",
                                Description="Region",
                                GroupName="Sender Address",
                                MustBeSpecified = true,
                                Name="Region",
                                ReadOnly= false,
                                SelectedValue="",
                                SortOrder=6,
                                ValueType = ConfigValueType.STRING
                            },
                            new ConfigItem() {
                                ConfigItemId = "COUNTRY",
                                Description="Country",
                                GroupName="Sender Address",
                                MustBeSpecified = true,
                                Name="Country",
                                ReadOnly= false,
                                SelectedValue="GB",
                                SortOrder=7,
                                ValueType = ConfigValueType.LIST,
                                ListValues = new List<ConfigItemListItem>()
                                {
                                    new ConfigItemListItem() {
                                        Display = "United Kingdom",
                                        Value = "GB"
                                    },
                                    new ConfigItemListItem() {
                                        Display = "Germany",
                                        Value = "DE"
                                    },
                                    new ConfigItemListItem() {
                                        Display = "France",
                                        Value = "FR"
                                    },
                                    new ConfigItemListItem() {
                                        Display = "United States",
                                        Value = "US"
                                    }
                                }
                            },
                             new ConfigItem() {
                                ConfigItemId = "TELEPHONE",
                                Description="Telephone",
                                GroupName="Sender Address",
                                MustBeSpecified = true,
                                Name="Telephone",
                                ReadOnly= false,
                                SelectedValue="",
                                SortOrder=8,
                                ValueType = ConfigValueType.STRING
                            },
                            new ConfigItem() {
                                ConfigItemId = "POSTCODE",
                                Description="Postal Code",
                                GroupName="Sender Address",
                                MustBeSpecified = true,
                                Name="Postal Code",
                                ReadOnly= false,
                                SelectedValue="",
                                SortOrder=8,
                                ValueType = ConfigValueType.STRING
                            },
                            new ConfigItem() {
                                ConfigItemId = "USERNAME",
                                Description="DHL Username",
                                GroupName="Sender Address",
                                MustBeSpecified = true,
                                Name="DHL Username",
                                ReadOnly= false,
                                SelectedValue="",
                                SortOrder=9,
                                ValueType = ConfigValueType.STRING
                            },
                            new ConfigItem() {
                                ConfigItemId = "PASSWORD",
                                Description="DHL password",
                                GroupName="Sender Address",
                                MustBeSpecified = true,
                                Name="DHL Password",
                                ReadOnly= false,
                                SelectedValue="",
                                SortOrder=10,
                                ValueType = ConfigValueType.PASSWORD
                            },
                             new ConfigItem() {
                                ConfigItemId = "ACCOUNTNUMBER",
                                Description="DHL Account Number",
                                GroupName="Sender Address",
                                MustBeSpecified = true,
                                Name="DHL Account Number",
                                ReadOnly= false,
                                SelectedValue="",
                                SortOrder=11,
                                ValueType = ConfigValueType.STRING
                            }
                    }
                };
            }
        }
    }
}
