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
                        ServiceCode="V01PAK",
                        ServiceGroup="Fast2Door",
                        ServiceName="DHL Paket V01PAK",
                        ServiceTag = "DHL Paket V01PAK",
                        ServiceUniqueId = new Guid("6A476315-04DB-4D25-A25C-E6917A1BCAD9"),
                        ServiceProperty = new List<ServiceProperty>() {
                            new ServiceProperty()
                            {
                                PropertyName = "StreetName",
                                PropertyValue=""
                            },
                            new ServiceProperty()
                            {
                                PropertyName = "StreetNumber",
                                PropertyValue=""
                            }
                        },
                        ConfigItems = new List<ConfigItem>() {
                            new ConfigItem() {
                                ConfigItemId="AccountNumber",
                                Description="Account Number for this service",
                                GroupName = "Notifications",
                                MustBeSpecified = true,
                                Name="AccountNumber",
                                ReadOnly = false,
                                SortOrder=1,
                                SelectedValue = "",
                                ValueType = ConfigValueType.STRING
                            }
                            ,
                             new ConfigItem() {
                                ConfigItemId="DefaultWeight",
                                Description="Default Weight (in grams) for this service",
                                GroupName = "Notifications",
                                MustBeSpecified = true,
                                Name="DefaultWeight",
                                ReadOnly = false,
                                SortOrder=1,
                                SelectedValue = "",
                                ValueType = ConfigValueType.STRING
                            }
                        }
                    },
                    new CourierService() {
                        ServiceCode="V06PAK",
                        ServiceGroup="Fast2Door",
                        ServiceName="DHL Paket Taggleich V06PAK",
                        ServiceTag = "DHL Paket Taggleich V06PAK",
                        ServiceUniqueId = new Guid("6A476315-04DB-4D25-A25C-E6917A1BCAD8"),
                        ServiceProperty = new List<ServiceProperty>() {
                            new ServiceProperty()
                            {
                                PropertyName = "StreetName",
                                PropertyValue=""
                            },
                            new ServiceProperty()
                            {
                                PropertyName = "StreetNumber",
                                PropertyValue=""
                            }
                        },
                        ConfigItems = new List<ConfigItem>() {
                            new ConfigItem() {
                                ConfigItemId="AccountNumber",
                                Description="Account Number for this service",
                                GroupName = "Notifications",
                                MustBeSpecified = true,
                                Name="AccountNumber",
                                ReadOnly = false,
                                SortOrder=1,
                                SelectedValue = "",
                                ValueType = ConfigValueType.STRING
                            }
                            ,
                             new ConfigItem() {
                                ConfigItemId="DefaultWeight",
                                Description="Default Weight (in grams) for this service",
                                GroupName = "Notifications",
                                MustBeSpecified = true,
                                Name="DefaultWeight",
                                ReadOnly = false,
                                SortOrder=1,
                                SelectedValue = "",
                                ValueType = ConfigValueType.STRING
                            }
                        }
                    },
                    new CourierService() {
                        ServiceCode="V54EPAK",
                        ServiceGroup="Fast2Door",
                        ServiceName="DHL Europaket V54EPAK",
                        ServiceTag = "DHL Europaket V54EPAK",
                        ServiceUniqueId = new Guid("6A476315-04DB-4D25-A25C-E6917A1BCAD7"),
                       ServiceProperty = new List<ServiceProperty>() {
                            new ServiceProperty()
                            {
                                PropertyName = "StreetName",
                                PropertyValue=""
                            },
                            new ServiceProperty()
                            {
                                PropertyName = "StreetNumber",
                                PropertyValue=""
                            }
                        },
                        ConfigItems = new List<ConfigItem>() {
                            new ConfigItem() {
                                ConfigItemId="AccountNumber",
                                Description="Account Number for this service",
                                GroupName = "Notifications",
                                MustBeSpecified = true,
                                Name="AccountNumber",
                                ReadOnly = false,
                                SortOrder=1,
                                SelectedValue = "",
                                ValueType = ConfigValueType.STRING
                            }
                            ,
                             new ConfigItem() {
                                ConfigItemId="DefaultWeight",
                                Description="Default Weight (in grams) for this service",
                                GroupName = "Notifications",
                                MustBeSpecified = true,
                                Name="DefaultWeight",
                                ReadOnly = false,
                                SortOrder=1,
                                SelectedValue = "",
                                ValueType = ConfigValueType.STRING
                            }
                        }
                    },
                    new CourierService() {
                        ServiceCode="V53WPAK",
                        ServiceGroup="Fast2Door",
                        ServiceName="DHL Paket International V53WPAK",
                        ServiceTag = "DHL Paket International V53WPAK",
                        ServiceUniqueId = new Guid("6A476315-04DB-4D25-A25C-E6917A1BCAD6"),
                        ServiceProperty = new List<ServiceProperty>() {
                            new ServiceProperty()
                            {
                                PropertyName = "StreetName",
                                PropertyValue=""
                            },
                            new ServiceProperty()
                            {
                                PropertyName = "StreetNumber",
                                PropertyValue=""
                            }
                        },
                        ConfigItems = new List<ConfigItem>() {
                            new ConfigItem() {
                                ConfigItemId="AccountNumber",
                                Description="Account Number for this service",
                                GroupName = "Notifications",
                                MustBeSpecified = true,
                                Name="AccountNumber",
                                ReadOnly = false,
                                SortOrder=1,
                                SelectedValue = "",
                                ValueType = ConfigValueType.STRING
                            }
                            ,
                             new ConfigItem() {
                                ConfigItemId="DefaultWeight",
                                Description="Default Weight (in grams) for this service",
                                GroupName = "Notifications",
                                MustBeSpecified = true,
                                Name="DefaultWeight",
                                ReadOnly = false,
                                SortOrder=1,
                                SelectedValue = "",
                                ValueType = ConfigValueType.STRING
                            }
                        }
                    },
                    new CourierService() {
                        ServiceCode="V06TG",
                        ServiceGroup="Fast2Door",
                        ServiceName="DHL Kurier Taggleich V06TG",
                        ServiceTag = "DHL Kurier Taggleich V06TG",
                        ServiceUniqueId = new Guid("6A476315-04DB-4D25-A25C-E6917A1BCAD5"),
                        ServiceProperty = new List<ServiceProperty>() {
                            new ServiceProperty()
                            {
                                PropertyName = "StreetName",
                                PropertyValue=""
                            },
                            new ServiceProperty()
                            {
                                PropertyName = "StreetNumber",
                                PropertyValue=""
                            }
                        },
                        ConfigItems = new List<ConfigItem>() {
                            new ConfigItem() {
                                ConfigItemId="AccountNumber",
                                Description="Account Number for this service",
                                GroupName = "Notifications",
                                MustBeSpecified = true,
                                Name="AccountNumber",
                                ReadOnly = false,
                                SortOrder=1,
                                SelectedValue = "",
                                ValueType = ConfigValueType.STRING
                            }
                            ,
                             new ConfigItem() {
                                ConfigItemId="DefaultWeight",
                                Description="Default Weight (in grams) for this service",
                                GroupName = "Notifications",
                                MustBeSpecified = true,
                                Name="DefaultWeight",
                                ReadOnly = false,
                                SortOrder=1,
                                SelectedValue = "",
                                ValueType = ConfigValueType.STRING
                            }
                        }
                    },
                    new CourierService() {
                        ServiceCode="V06WZ",
                        ServiceGroup="Fast2Door",
                        ServiceName="DHL Kurier Wunschzeit V06WZ",
                        ServiceTag = "DHL Kurier Wunschzeit V06WZ",
                        ServiceUniqueId = new Guid("6A476315-04DB-4D25-A25C-E6917A1BCAD4"),
                        ServiceProperty = new List<ServiceProperty>() {
                            new ServiceProperty()
                            {
                                PropertyName = "StreetName",
                                PropertyValue=""
                            },
                            new ServiceProperty()
                            {
                                PropertyName = "StreetNumber",
                                PropertyValue=""
                            }
                        },
                        ConfigItems = new List<ConfigItem>() {
                            new ConfigItem() {
                                ConfigItemId="AccountNumber",
                                Description="Account Number for this service",
                                GroupName = "Notifications",
                                MustBeSpecified = true,
                                Name="AccountNumber",
                                ReadOnly = false,
                                SortOrder=1,
                                SelectedValue = "",
                                ValueType = ConfigValueType.STRING
                            }
                            ,
                             new ConfigItem() {
                                ConfigItemId="DefaultWeight",
                                Description="Default Weight (in grams) for this service",
                                GroupName = "Notifications",
                                MustBeSpecified = true,
                                Name="DefaultWeight",
                                ReadOnly = false,
                                SortOrder=1,
                                SelectedValue = "",
                                ValueType = ConfigValueType.STRING
                            }
                        }
                    },
                    new CourierService() {
                        ServiceCode="V86PARCEL",
                        ServiceGroup="Fast2Door",
                        ServiceName="DHL Paket Austria V86PARCEL",
                        ServiceTag = "DHL Paket Austria V86PARCEL",
                        ServiceUniqueId = new Guid("6A476315-04DB-4D25-A25C-E6917A1BCAD3"),
                        ServiceProperty = new List<ServiceProperty>(),
                         ConfigItems = new List<ConfigItem>() {
                            new ConfigItem() {
                                ConfigItemId="AccountNumber",
                                Description="Account Number for this service",
                                GroupName = "Notifications",
                                MustBeSpecified = true,
                                Name="AccountNumber",
                                ReadOnly = false,
                                SortOrder=1,
                                SelectedValue = "",
                                ValueType = ConfigValueType.STRING
                            }
                            ,
                             new ConfigItem() {
                                ConfigItemId="DefaultWeight",
                                Description="Default Weight (in grams) for this service",
                                GroupName = "Notifications",
                                MustBeSpecified = true,
                                Name="DefaultWeight",
                                ReadOnly = false,
                                SortOrder=1,
                                SelectedValue = "",
                                ValueType = ConfigValueType.STRING
                            }
                        }
                    },
                    new CourierService() {
                        ServiceCode="V87PARCEL",
                        ServiceGroup="Fast2Door",
                        ServiceName="DHL Paket Connect V87PARCEL",
                        ServiceTag = "DHL Paket Connect V87PARCEL",
                        ServiceUniqueId = new Guid("6A476315-04DB-4D25-A25C-E6917A1BCAD2"),
                        ServiceProperty = new List<ServiceProperty>() {
                            new ServiceProperty()
                            {
                                PropertyName = "StreetName",
                                PropertyValue=""
                            },
                            new ServiceProperty()
                            {
                                PropertyName = "StreetNumber",
                                PropertyValue=""
                            }
                        },
                        ConfigItems = new List<ConfigItem>() {
                            new ConfigItem() {
                                ConfigItemId="AccountNumber",
                                Description="Account Number for this service",
                                GroupName = "Notifications",
                                MustBeSpecified = true,
                                Name="AccountNumber",
                                ReadOnly = false,
                                SortOrder=1,
                                SelectedValue = "",
                                ValueType = ConfigValueType.STRING
                            }
                            ,
                             new ConfigItem() {
                                ConfigItemId="DefaultWeight",
                                Description="Default Weight (in grams) for this service",
                                GroupName = "Notifications",
                                MustBeSpecified = true,
                                Name="DefaultWeight",
                                ReadOnly = false,
                                SortOrder=1,
                                SelectedValue = "",
                                ValueType = ConfigValueType.STRING
                            }
                        }
                    },
                    new CourierService() {
                        ServiceCode="V82PARCEL",
                        ServiceGroup="Fast2Door",
                        ServiceName="DHL Paket International V82PARCEL",
                        ServiceTag = "DHL Paket International V82PARCEL",
                        ServiceUniqueId = new Guid("6A476315-04DB-4D25-A25C-E6917A1BCAD1"),
                        ServiceProperty = new List<ServiceProperty>() {
                            new ServiceProperty()
                            {
                                PropertyName = "StreetName",
                                PropertyValue=""
                            },
                            new ServiceProperty()
                            {
                                PropertyName = "StreetNumber",
                                PropertyValue=""
                            }
                        },
                        ConfigItems = new List<ConfigItem>() {
                            new ConfigItem() {
                                ConfigItemId="AccountNumber",
                                Description="Account Number for this service",
                                GroupName = "Notifications",
                                MustBeSpecified = true,
                                Name="AccountNumber",
                                ReadOnly = false,
                                SortOrder=1,
                                SelectedValue = "",
                                ValueType = ConfigValueType.STRING
                            },
                             new ConfigItem() {
                                ConfigItemId="DefaultWeight",
                                Description="Default Weight (in grams) for this service",
                                GroupName = "Notifications",
                                MustBeSpecified = true,
                                Name="DefaultWeight",
                                ReadOnly = false,
                                SortOrder=1,
                                SelectedValue = "",
                                ValueType = ConfigValueType.STRING
                            }
                        }
                    }
                };
            }
        }
    }
}
