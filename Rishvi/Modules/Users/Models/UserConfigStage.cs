using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rishvi.Modules.Users.Models
{
    public static class UserConfigStage
    {
        public static ConfigStage GetUserConfigStage(AuthorizationConfig authConfig)
        {
            return new ConfigStage()
            {
                WizardStepDescription = "Customer config can be changed",
                WizardStepTitle = "Customer Details",
                ConfigItems = new List<ConfigItem>() {
                        new ConfigItem() {
                                ConfigItemId = "NAME",
                                Description="Contact name",
                                GroupName="Sender Address",
                                MustBeSpecified = true,
                                Name="Contact Name",
                                ReadOnly= false,
                                SelectedValue=authConfig.AccountName,
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
                                SelectedValue=authConfig.AddressLine1,
                                SortOrder=3,
                                ValueType = ConfigValueType.STRING
                            },
                            new ConfigItem() {
                                ConfigItemId = "COUNTRY",
                                Description="Country",
                                GroupName="Sender Address",
                                MustBeSpecified = true,
                                Name="Country",
                                ReadOnly= true,
                                SelectedValue=authConfig.CountryCode,
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
                            }
                    }
            };
        }
    }
}
