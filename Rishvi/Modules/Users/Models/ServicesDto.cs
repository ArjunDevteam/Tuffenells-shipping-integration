using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rishvi.Modules.Users.Models
{
    public static class ServicesDto
    {
        public static List<CourierService> GetServices
        {
            get
            {
                return new List<CourierService>() {
                    new CourierService() {
                        ServiceCode="CODE1",
                        ServiceGroup="Fast2Door",
                        ServiceName="Fast2Door Next Day",
                        ServiceTag = "SOMETAG",
                        ServiceUniqueId = new Guid("6A476315-04DB-4D25-A25C-E6917A1BCAD9"),
                        ServiceProperty = new List<ServiceProperty>() {
                            new ServiceProperty()
                            {
                                PropertyName = "LABELTAG",
                                PropertyValue="Next Day"
                            }
                        },
                        ConfigItems = new List<ConfigItem>() {
                            new ConfigItem() {
                                ConfigItemId="InsuranceConver",
                                Description="Additional Insurance conver",
                                GroupName = "Insurance",
                                MustBeSpecified = false,
                                Name="Insurance Cover",
                                ReadOnly = false,
                                SortOrder=1,
                                SelectedValue = "500",
                                ValueType = ConfigValueType.LIST,
                                ListValues = new List<ConfigItemListItem>() {
                                    new ConfigItemListItem() {
                                        Display = "Up to 500",
                                        Value = "500"
                                    },
                                    new ConfigItemListItem() {
                                        Display = "Up to 1000",
                                        Value = "1000"
                                    },
                                    new ConfigItemListItem() {
                                        Display = "Up to 2000",
                                        Value = "2000"
                                    },
                                }
                            }
                        }
                    },
                    new CourierService() {
                        ServiceCode="CODE_WED",
                        ServiceGroup="Fast2Door",
                        ServiceName="Fast2Door By Wednesday",
                        ServiceTag = "WEDNESDAY",
                        ServiceUniqueId = new Guid("58F4DF9F-0245-4D18-B04C-334CDCAC4186"),
                        ServiceProperty = new List<ServiceProperty>() {
                            new ServiceProperty()
                            {
                                PropertyName = "LABELTAG",
                                PropertyValue="By Wednesday"
                            }
                        },
                        ConfigItems = new List<ConfigItem>() {
                            new ConfigItem() {
                                ConfigItemId="SMS",
                                Description="Send delivery SMS to the customer",
                                GroupName = "Notifications",
                                MustBeSpecified = false,
                                Name="SMS",
                                ReadOnly = false,
                                SortOrder=1,
                                SelectedValue = "true",
                                ValueType = ConfigValueType.BOOLEAN
                            }
                        }
                    }
                };
            }
        }
    }
}
