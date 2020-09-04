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
                                ConfigItemId = "ADDRESS1",
                                Description="Address line 1",
                                GroupName="Sender Address",
                                MustBeSpecified = true,
                                Name="Address 1",
                                ReadOnly= false,
                                SelectedValue="",
                                SortOrder=3,
                                ValueType = ConfigValueType.STRING
                            },
                            new ConfigItem() {
                                ConfigItemId = "ADDRESS2",
                                Description="Address line 2",
                                GroupName="Sender Address",
                                MustBeSpecified = false,
                                Name="Address 2",
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
                                Description="Contact telephone number",
                                GroupName="Sender Address",
                                MustBeSpecified = true,
                                Name="Contact Telephone",
                                ReadOnly= false,
                                SelectedValue="",
                                SortOrder=8,
                                ValueType = ConfigValueType.STRING
                            },
                            new ConfigItem() {
                                ConfigItemId = "STAGE_SELECT",
                                Description="Which stage do you want to select next",
                                GroupName="Next Config Stage",
                                MustBeSpecified = true,
                                Name="Next Stage",
                                ReadOnly= false,
                                SelectedValue="ValuesStage",
                                SortOrder=9,
                                ValueType = ConfigValueType.LIST,
                                ListValues = new List<ConfigItemListItem>()
                                {
                                    new ConfigItemListItem() {
                                        Display = "Go to Value stage",
                                        Value = "ValuesStage"
                                    },
                                    new ConfigItemListItem() {
                                        Display = "Go to Description stage",
                                        Value = "DescriptionStage"
                                    }
                                }
                            }
                    }
                };
            }
        }
    }
}
